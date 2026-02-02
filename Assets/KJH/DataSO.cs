using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataSO", menuName = "ItemDataSO")]
public class DataSO : ScriptableObject
{
    public Sprite icon;
    public GameObject effect;
    public AudioClip sfx;
}
