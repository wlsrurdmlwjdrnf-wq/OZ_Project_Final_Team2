using Newtonsoft.Json;
using SQLite4Unity3d;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;


public class ImportJson : EditorWindow
{
    private string sheetURL = "https://script.google.com/macros/s/AKfycbxbx3aAGHfHrDj4FRME-VRNAhOOMVELDPLyXQ_HrnR98MExlxmwdvfJBmQ8SXPs_9fIPg/exec";
    private bool isDownloading = false;

    [MenuItem("Tools/데이터 동기화")]
    public static void ShowWindow() => GetWindow<ImportJson>("Data Importer");

    private void OnGUI()
    {
        GUILayout.Label("구글 시트 -> SQLite 동기화", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        sheetURL = EditorGUILayout.TextField("Sheet URL", sheetURL);

        EditorGUILayout.Space();

        if (GUILayout.Button("데이터 동기화", GUILayout.Height(40)))
        {
            DownloadAndSave();
        }

        if (isDownloading)
        {
            EditorGUILayout.HelpBox("데이터 가져오는중..", MessageType.Info);
        }
    }

    private void DownloadAndSave()
    {
        if (isDownloading) return;
        isDownloading = true;

        EditorApplication.CallbackFunction updateAction = null;

        var www = UnityWebRequest.Get(sheetURL);
        var operation = www.SendWebRequest();

        updateAction = () => {
            if (operation.isDone)
            {
                EditorApplication.update -= updateAction;
                if (www.result == UnityWebRequest.Result.Success)
                {
                    string json = www.downloadHandler.text;
                    Debug.Log($"데이터 수신 완료");

                    SaveToLocalDatabase(json);

                    EditorUtility.DisplayDialog("성공", "데이터가 성공적으로 로컬 DB에 반영되었습니다.", "확인");
                }
                else
                {
                    Debug.LogError($"[Error] {www.error}");
                    EditorUtility.DisplayDialog("실패", "데이터 다운로드 중 오류가 발생했습니다.", "확인");
                }

                isDownloading = false;
                www.Dispose();
            }
        };
        EditorApplication.update += updateAction;
    }

    private void SaveToLocalDatabase(string json)
    {
        try
        {
            var response = JsonConvert.DeserializeObject<SheetData>(json);
            string dbPath = Path.Combine(Application.persistentDataPath, "LocalGameData.db");

            string directory = Path.GetDirectoryName(dbPath);
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

            using (var db = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create))
            {
                // 트랜잭션 시작 (성능 및 데이터 무결성)
                db.BeginTransaction();

                SaveTable<WeaponData>(db, response.data, "Weapon");
                SaveTable<AccessoryData>(db, response.data, "Accessory");
                SaveTable<ArtifactData>(db, response.data, "Artifact");
                SaveTable<PlayerInitData>(db, response.data, "PlayerInit");
                SaveTable<SkillData>(db, response.data, "Skill");
                SaveTable<StageData>(db, response.data, "Stage");

                db.Commit();

                PlayerPrefs.SetString("GameDataVersion", response.version);
                EditorUtility.DisplayDialog("성공", $"버전 {response.version} 저장 완료!", "확인");
            }
        }
        catch (System.Exception e)
        {
            Debug.Log("저장 시도 데이터: " + json);
            Debug.LogError($"데이터 처리 에러: {e.Message}");
        }
    }

    private void SaveTable<T>(SQLiteConnection db, Dictionary<string, List<Dictionary<string, object>>> allData, string sheetName) where T : new()
    {
        if (allData.ContainsKey(sheetName))
        {
            Debug.Log($"{sheetName} 데이터 발견! 개수: {allData[sheetName].Count}");
            db.DropTable<T>(); // 기존 데이터 삭제
            db.CreateTable<T>();

            // 객체 변환
            string tableJson = JsonConvert.SerializeObject(allData[sheetName]);
            var list = JsonConvert.DeserializeObject<List<T>>(tableJson);

            db.InsertAll(list);
            Debug.Log($"[LocalDB] {sheetName} 테이블 : {list.Count}개 데이터 저장");
        }
        else
        {
            Debug.LogWarning($"{sheetName} 시트가 JSON 데이터에 존재하지 않습니다.");
        }
    }
}
