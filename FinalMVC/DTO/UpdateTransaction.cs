using FinalMVC.Areas.Identity.Data;

namespace FinalMVC.DTO
{
    public class UpdateTransaction
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public int Status { get; set; }

    }
}
