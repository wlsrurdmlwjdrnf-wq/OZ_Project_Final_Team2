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

    }
}
