using Newtonsoft.Json;

namespace EtherscanTest.Dtos
{
	public class ETHRequestDto
	{
		[JsonProperty(PropertyName = "jsonrpc")]
		public string? JsonRPC { get; set; }

		[JsonProperty(PropertyName = "method")]
		public string? Method { get; set; }

		[JsonProperty(PropertyName = "params")]
		public object? Params { get; set; }

		[JsonProperty(PropertyName = "id")]
		public int Id { get; set; }
    }

	public class ETHResponseDto
	{
		[JsonProperty(PropertyName = "jsonrpc")]
		public string? JsonRPC { get; set; }

		[JsonProperty(PropertyName = "id")]
		public int Id { get; set; }

		[JsonProperty(PropertyName = "result")]
		public object? Result { get; set; }
	}

	public class GetBlockByNumberResultDto
	{
		public string? number { get; set; }
		public string? difficulty { get; set; }
		public string? extraData { get; set; }
		public string? gasLimit { get; set; }
		public string? gasUsed { get; set; }
		public string? hash { get; set; }
		public string? logsBloom { get; set; }
		public string? miner { get; set; }
		public string? mixHash { get; set; }
		public string? nonce { get; set; }
		public string? parentHash { get; set; }
		public string? receiptsRoot { get; set; }
		public string? sha3Uncles { get; set; }
		public string? size { get; set; }
		public string? stateRoot { get; set; }
		public string? timestamp { get; set; }
		public string? totalDifficulty { get; set; }
		public IList<object>? transactions { get; set; }
		public string? transactionsRoot { get; set; }
		public IList<object>? uncles { get; set; }
	}

	public class GetTransactionResultDto
	{
		public string? blockHash { get; set; }
		public string? blockNumber { get; set; }
		public string? from { get; set; }
		public string? gas { get; set; }
		public string? gasPrice { get; set; }
		public string? hash { get; set; }
		public string? input { get; set; }
		public string? nonce { get; set; }
		public string? to { get; set; }
		public string? transactionIndex { get; set; }
		public string? value { get; set; }
		public string? type { get; set; }
		public string? chainId { get; set; }
		public string? v { get; set; }
		public string? r { get; set; }
		public string? s { get; set; }
	}
}
