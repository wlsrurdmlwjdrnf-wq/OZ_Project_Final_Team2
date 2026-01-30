using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Extensions;
/*
 * 싱글톤입니다
 * UI에서 버튼이벤트 발생시 해당 함수 연결해주시면 됩니다
*/



public class LoginManager : Singleton<LoginManager>
{
    FirebaseAuth auth;


    //기본값 세팅
    protected override void Init()
    {
        base.Init();
        auth = FirebaseAuth.DefaultInstance;
    }


    //회원가입
    // UI에서 email, password에서 input받은 값을 매개변수로 넘겨주면 됩니다

    public void SIgnUp(string email, string password)
    {
        //(비동기) 작업이 끝났을 때 처리할 작업
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(x =>
        {
            // 네트워크 끊김, 강제 중단
            if (x.IsCanceled)
            {
                //UI연동 openUI 및 해당 UI Text = 네트워크 오류
                return;
            }
            //서버로부터 에러를 받은경우(중복가입, 이메일,비밀번호 형식)
            else if (x.IsFaulted)
            {
                //UI연동 openUI 및 해당 UI Text = 중복된 ID거나 올바르지 않은 형식입니다
                //Debug.LogError(x.Exception); <- 상세에러 디버그용
                return;
            }
            //가입완료
            else
            {
                FirebaseUser newUser = x.Result.User;
                //UI연동 openUI 및 해당 UI Text = 가입 성공!
                //SQLite 데이터 베이스에서 Defalut UserData 동기화
            }
        });
    }

    //로그인

    //로그인도 회원가입과 똑같음

    public void SighIn(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(x =>
        {
            // 네트워크 끊김, 강제 중단
            if (x.IsCanceled)
            {
                //UI연동 openUI 및 해당 UI Text = 네트워크 오류
                return;
            }
            //서버로부터 에러를 받은경우(ID, Password 불일치)
            else if (x.IsFaulted)
            {
                //UI연동 openUI 및 해당 UI Text = ID 혹은 Password가 일치하지 않습니다
                //Debug.LogError(x.Exception); <- 상세에러 디버그용
                return;
            }
            //로그인 성공
            else
            {
                FirebaseUser newUser = x.Result.User;
                //UI연동 openUI 및 해당 UI Text = 로그인 성공
                //이 시점에서 마지막 접속시간(서버시간기준)과 현재 접속시간 비교후 오프라인 재화 획득 추가
                //유저 재화량은 실시간으로 서버에 저장하지않고 로컬 데이터베이스에 저장해둔 뒤 서버시간 기준으로 비교하여 동기화
            }
        });
    }

}
