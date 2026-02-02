using System.ComponentModel.DataAnnotations;
namespace TeamProjectServer.Models
{
    public class UserData
    {
        //DB 유저 식별용 고유번호 
        [Key]
        public int ID { get; set; }

        //로그인 이메일
        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }


        //스프레드 시트 매핑 데이터

        public string Name { get; set; }
        public int Level { get; set; }
        public Tier Tier { get; set; }
        public float ATKPower { get; set; }
        public float MaxHP { get; set; }
        public float HPRegenPerSec { get; set; }
        public float MaxMP { get; set; }
        public float CriticalRate { get; set; }
        public float CriticalDamage { get; set; }
        public float MPRegenPerSec { get; set; }
        public float GoldMultiplier { get; set; }
        public float EXPMultiplier { get; set; }
        public float ATKSpeed { get; set; }
        public float MoveSpeed { get; set; }
    }
}
