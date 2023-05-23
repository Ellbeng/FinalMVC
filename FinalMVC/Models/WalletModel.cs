using FinalMVC.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace FinalMVC.Models
{
    public class WalletModel
    {
        [Key]
        public int WalletId { get; set; }
        public string UserId { get; set; }
        public int CurrentBalance { get; set; }
        public virtual Users Users { get; set; }
    }
}
