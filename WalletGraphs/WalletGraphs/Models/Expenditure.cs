using System.ComponentModel.DataAnnotations;

namespace WalletGraphs.Models
{
    public class Expenditure
    {
		
		public int ExpenditureId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public decimal Amount { get; set; }
		[Required]
		public DateTime Date { get; set; }
		[Required]
		public string Category { get; set; }
		[Required]
		public string Reciever { get; set; }

        public Expenditure()
        {
                
        }
    }
}
