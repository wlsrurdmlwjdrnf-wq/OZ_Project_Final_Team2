namespace TeamProjectServer.Models.DTO
{
    public class LoginResponse
    {
        public bool Success { get; set; } // 로그인 성공여부
        public string Message { get; set; } // 메세지
        public PlayerAccountData PlayerData { get; set; } // 해당 플레이어의 데이터
        public float OfflineRewardGold { get; set; } // 오프라인 보상 골드
        public float OfflineRewardEXP { get; set; } // 오프라인 보상 경험치
        public float OfflineMinutes { get; set; } // 부재시간
    }
}
