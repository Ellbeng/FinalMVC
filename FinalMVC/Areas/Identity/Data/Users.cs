using FinalMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FinalMVC.Areas.Identity.Data
{
    
    public class Users:IdentityUser
    {
       
        public string FirstName { get; set; }
      
        public string LastName { get; set; }

        public virtual WalletModel WalletModel { get; set; }


    }


}
