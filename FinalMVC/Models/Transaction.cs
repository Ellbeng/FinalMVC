using FinalMVC.Areas.Identity.Data;
using MessagePack;

namespace FinalMVC.Models
{
    public class Transaction
    {
        
        public int Id { get; set; }
        public string UserId { get; set; }
        public string PaymentType { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime CreateDate { get; set; }
        public int Status { get; set; }
        public virtual Users Users { get; set; }
    }
}
