using System.ComponentModel.DataAnnotations.Schema;

namespace EtherscanTest.Models
{
	[Table("blocks")]
	public class Block
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int blockID { get; set; }

		public int blockNumber { get; set; }

		public string? hash { get; set; }

		public string? parentHash { get; set; }

		public string? miner { get; set; }

		public decimal? blockReward { get; set; }

		public decimal? gasLimit { get; set; }

		public decimal? gasUsed { get; set; }
	}
}
