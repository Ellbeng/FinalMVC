using FinalMVC.DTO;
using FinalMVC.Models;
using FinalMVC.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalMVC.Controllers
{
    public class TokenController : Controller
    {


        private readonly ITokenService _tokenService;
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;



        public TokenController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ITokenService tokenService, IConfiguration configuration)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
           

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertToken()

        {

            var token = new Token();
            token.UserId = _userManager.GetUserId(User);
            token.PublicToken = Guid.NewGuid().ToString();
            token.PrivateToken = Guid.NewGuid().ToString();
            token.PrivateTokenStatus = 0;
            token.PublicTokenStatus = 0;
            token.CreateDate = DateTime.Now;
         

            _tokenService.GenerateToken(token); 

            return new EmptyResult();
        }
    }
}
