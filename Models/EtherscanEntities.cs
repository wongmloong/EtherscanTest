using Microsoft.EntityFrameworkCore;

namespace EtherscanTest.Models
{
	public class EtherscanEntities: DbContext
	{
		public DbSet<Block> Blocks { get; set; }
		public DbSet<Transaction> Transactions { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseMySQL("server=localhost;port=3306;database=etherscan_db;uid=root;password=123456");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
