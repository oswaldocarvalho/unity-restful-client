using System;
using RESTfull;
using Newtonsoft.Json;

namespace RESTClient.Requests
{
	[Serializable]
	public class RankingRequest : BaseRequestResponse
	{
		[JsonProperty("score")] public int Score { get; set; }
	}
}