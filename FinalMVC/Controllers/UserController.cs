using FinalMVC.Areas.Identity.Data;
using FinalMVC.DTO;
using FinalMVC.Models;
using FinalMVC.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCWithDapper.DBContext;
using Newtonsoft.Json;
using System.Net.Http;

using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;

namespace FinalMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IWalletService _walletService;
        private readonly ITransactionService _transactionService;
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
       


        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IWalletService walletService, IConfiguration configuration, ITransactionService transactionService)
        {
            _walletService = walletService;
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _transactionService = transactionService;
            
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetUser()
        {

            var userId = _userManager.GetUserId(User);
            var user = _walletService.GetUserById(userId);
            var model = new UserModel
            {
                // Assign properties from Identity user and other data as needed
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Id = userId,
                UserName=user.UserName,
                WalletModel=user.WalletModel
            };
            return View(model);
        }




        [HttpGet]
        public JsonResult GetBalance()
        {
       
            var userId = _userManager.GetUserId(User);
            var user = _walletService.GetUserWithWallet(userId);

            return Json(user.CurrentBalance);
        }

        public IActionResult Deposit()
        {
            return View();
        }

        public IActionResult WithDraw()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> InsertTransaction(TransactionDto transactionDto)
        {
            var transaction = new Transaction();
            transaction.UserId = _userManager.GetUserId(User);
            transaction.PaymentType = transactionDto.PaymentType;
            transaction.Status = 1;
            transaction.Currency = transactionDto.Currency;
            if(transaction.PaymentType=="Deposit") transaction.Amount = 0;
            else if(transaction.PaymentType == "Withdraw") transaction.Amount = 0;
            transaction.CreateDate= DateTime.Now;
            transaction.Status = 1;
           Transaction newTransaction=_transactionService.InsertTransaction(transaction);
            
            var callbackUrl = "https://localhost:44339/User/CallBack";
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"Id", newTransaction.Id.ToString() },
                    { "Amount", transactionDto.Amount.ToString() },
                    { "PaymentType", transaction.PaymentType.ToString() },
                    { "Currency", transaction.Currency.ToString() },
                    { "CreateDate", transaction.CreateDate.ToString() },
                    { "Status", transaction.Status.ToString() },
                    { "CallbackUrl",callbackUrl.ToString()}


                });
            using (var client = new HttpClient())
            {
                
                var uri = "https://localhost:44365/Card/RedirectToBank";

                var response = await client.PostAsync(uri, content);

                string textResult = await response.Content.ReadAsStringAsync();

                // Return JSON response
                return Json(new { bankFormFillLink= textResult });
            }


        }


        public IActionResult CallBack(TransactionInfo transaction)
        {

            string transactionLink = _transactionService.GeneratetransactionFormFillLink(transaction);
            return Ok(transactionLink);


         
        }


        public JsonResult UpdateTransaction(TransactionInfo transactionDto)
        {
            var transaction = new UpdateTransaction();
            transaction.Id =Convert.ToInt16(transactionDto.Id);
            transaction.UserId = _userManager.GetUserId(User);
            transaction.Status =Convert.ToInt16(transactionDto.Status);
            
            if (transactionDto.PaymentType == "Deposit") transaction.Amount =Convert.ToDecimal(transactionDto.Amount);
            else if (transactionDto.PaymentType == "Withdraw") transaction.Amount = Convert.ToDecimal(transactionDto.Amount) * -1;
           _transactionService.UpdateTransaction(transaction);
            

            return Json(new { success = true });


        }




    }
}
