using EtherscanTest.Dtos;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace EtherscanTest
{
	public static class ETHApi
	{
		private const string ApiUrl = "https://eth-mainnet.g.alchemy.com/v2/M0cF-BPTG9l5vkiIBcfzoezQePrAunIm";
		private static HttpClient _httpClient = new HttpClient();

		public static GetBlockByNumberResultDto? GetBlock(int blockNumber)
		{
			using (var response = _httpClient.PostAsJsonAsync<ETHRequestDto>(ApiUrl, new ETHRequestDto()
			{
				JsonRPC = "2.0",
				Method = "eth_getBlockByNumber",
				Params = new List<object> { blockNumber.ToHex(), true },
				Id = 0
			}).Result) 
				if (response != null)
				{
					var result = response.Content.ReadAsStringAsync().Result;
					if (!string.IsNullOrEmpty(result))
					{
						var data = JsonConvert.DeserializeObject<ETHResponseDto>(result);
						if (data != null && data.Result != null)
						{
							var blockResult = data.Result.ToString() ?? string.Empty;
							if (!string.IsNullOrEmpty(blockResult))
								return JsonConvert.DeserializeObject<GetBlockByNumberResultDto>(blockResult);
						}
					}
				}

			return null;
		}

		public static int GetBlockTransactionCount(string blockNumber)
		{
			using (var response = _httpClient.PostAsJsonAsync<ETHRequestDto>(ApiUrl, new ETHRequestDto()
			{
				JsonRPC = "2.0",
				Method = "eth_getBlockTransactionCountByNumber",
				Params = new List<object> { blockNumber },
				Id = 1
			}).Result)
				if (response != null)
				{
					var result = response.Content.ReadAsStringAsync().Result;
					if (!string.IsNullOrEmpty(result))
					{
						var data = JsonConvert.DeserializeObject<ETHResponseDto>(result);
						if (data != null && data.Result != null)
						{
							var countResult = data.Result.ToString() ?? string.Empty;
							if (!string.IsNullOrEmpty(countResult))
								return Convert.ToInt32(countResult, 16);
						}
					}
				}

			return 0;
		}

		public static GetTransactionResultDto? GetTransaction(string blockNumber, int trxIndex)
		{
			using (var response = _httpClient.PostAsJsonAsync<ETHRequestDto>(ApiUrl, new ETHRequestDto()
			{
				JsonRPC = "2.0",
				Method = "eth_getTransactionByBlockNumberAndIndex",
				Params = new List<object> { blockNumber, trxIndex.ToHex() },
				Id = 1
			}).Result)
				if (response != null)
				{
					var result = response.Content.ReadAsStringAsync().Result;
					if (!string.IsNullOrEmpty(result))
					{
						var data = JsonConvert.DeserializeObject<ETHResponseDto>(result);
						if (data != null && data.Result != null)
						{
							var transactionResult = data.Result.ToString() ?? string.Empty;
							if (!string.IsNullOrEmpty(transactionResult))
								return JsonConvert.DeserializeObject<GetTransactionResultDto>(transactionResult);
						}
					}
				}

			return null;
		}

		public static string ToHex(this int value)
		{
			return string.Format("0x{0:X}", value);
		}
	}
}
