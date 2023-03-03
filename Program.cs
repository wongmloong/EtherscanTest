using EtherscanTest.Models;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System.Globalization;
using System.Numerics;

namespace EtherscanTest
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var logger = LoggerFactory.Create(builder => builder.AddNLog()).CreateLogger<Program>();

			logger.LogInformation("Program has started.");

			for (int blockNumber = 12100001; blockNumber <= 12100500; blockNumber++)
			{
				using (var etherscanEntity = new EtherscanEntities())
				{
					logger.LogInformation($"Processing block #{blockNumber}...");
					var block = ETHApi.GetBlock(blockNumber);
					if (block != null && !string.IsNullOrEmpty(block.number))
					{
						logger.LogInformation($"Block #{blockNumber} found.");

						// Insert block
						var ethBlock = new Block()
						{
							blockNumber = Convert.ToInt32(block.number, 16),
							hash = block.hash,
							parentHash = block.parentHash,
							miner = block.miner,
							blockReward = 0,
							gasLimit = HexToDecimal(block.gasLimit),
							gasUsed = HexToDecimal(block.gasUsed)
						};
						etherscanEntity.Add<Block>(ethBlock);
						etherscanEntity.SaveChanges();
						logger.LogInformation($"Block #{blockNumber} inserted into database.");

						var blockTransactionCount = ETHApi.GetBlockTransactionCount(block.number);
						if (blockTransactionCount != 0)
						{
							for (int trxIndex = 0; trxIndex < blockTransactionCount; trxIndex++)
							{
								logger.LogInformation($"Retrieving transaction (Block: {blockNumber}, Index: {trxIndex})");
								var transaction = ETHApi.GetTransaction(block.number, trxIndex);
								if (transaction != null)
								{
									logger.LogInformation($"Transaction (Block: {blockNumber}, Index: {trxIndex}) found.");

									var ethTransaction = new Transaction()
									{
										blockID = ethBlock.blockID,
										hash = transaction.hash,
										from = transaction.from,
										to = transaction.to,
										value = HexToDecimal(transaction.value),
										gas = HexToDecimal(transaction.gas),
										gasPrice = HexToDecimal(transaction.gasPrice),
										transactionIndex = Convert.ToInt32(transaction.transactionIndex, 16)
									};
									etherscanEntity.Add<Transaction>(ethTransaction);
									etherscanEntity.SaveChanges();
									logger.LogInformation($"Transaction (Block: {blockNumber}, Index: {trxIndex}) inserted into database.");
								}
								else
								{
									logger.LogError($"Transaction (Block: {blockNumber}, Index: {trxIndex}) not found.");
								}
							}
						}
					}
					else
					{
						logger.LogError($"Block #{blockNumber} not found.");
					}
				}

				Thread.Sleep(1000);
			}
		}

		static decimal HexToDecimal(string? hex)
		{
			if (!string.IsNullOrEmpty(hex) && hex.IndexOf("0x") >= 0)
			{
				var number = BigInteger.Parse("0" + hex.Substring(2), NumberStyles.AllowHexSpecifier);
				return decimal.Parse(number.ToString());
			}
			else
				return 0;
		}
	}
}