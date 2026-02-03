using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamProjectServer.Data;
using TeamProjectServer.Models;
using TeamProjectServer.Models.DTO;
using TeamProjectServer.Services;

namespace TeamProjectServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context) => _context = context;

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] AuthRequest request)
        {
            Console.WriteLine("가입요청");
            if (await _context.playerAccountData.AnyAsync(x => x.Email == request.Email))
            {
                return BadRequest(new { message = "중복 ID" });
            }

            var baseData = DataManager.Get<PlayerInit>(1);

            if (baseData == null) Console.WriteLine("데이터베이스 null");

            if (baseData == null)
            {
                return StatusCode(500, "시트 데이터 에러");
            }

            var newAccount = new PlayerAccountData
            {
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),

                Name = baseData.Name,
                Level = baseData.Level,
                Tier = baseData.Tier,
                ATKPower = baseData.ATKPower,
                MaxHP = baseData.MaxHP,
                HPRegenPerSec = baseData.HPRegenPerSec,
                MaxMP = baseData.MaxMP,
                CriticalRate = baseData.CriticalRate,
                CriticalDamage = baseData.CriticalDamage,
                MPRegenPerSec = baseData.MPRegenPerSec,
                GoldMultiplier = baseData.GoldMultiplier,
                CurGold = baseData.CurGold,
                EXPMultiplier = baseData.EXPMultiplier,
                ATKSpeed = baseData.ATKSpeed,
                MoveSpeed = baseData.MoveSpeed,

                LastLoginTime = DateTime.UtcNow
            };

            _context.playerAccountData.Add(newAccount);
            await _context.SaveChangesAsync();

            return Ok(new { message = "회원가입 성공" });
        }
    }
}
