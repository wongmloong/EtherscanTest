using System.ComponentModel.DataAnnotations.Schema;

namespace EtherscanTest.Models
{
	[Table("transactions")]
	public class Transaction
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int transactionID { get; set; }

		public int blockID { get; set; }

		public string? hash { get; set; }

		public string? from { get; set; }

		public string? to { get; set; }

		public decimal? value { get; set; }

		public decimal? gas { get; set; }

		public decimal? gasPrice { get; set; }

		public int transactionIndex { get; set; }
	}
}
