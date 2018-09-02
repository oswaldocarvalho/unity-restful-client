using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RESTfull;

namespace RESTClient.Responses
{
	[Serializable]
	public class RankingItem
	{
		[JsonProperty("rank")] public int Rank;
		[JsonProperty("nickname")] public string Nickname;
		[JsonProperty("score")] public int Score;
	}

	[Serializable]
	public class RankingResponse : BaseRequestResponse
	{
		[JsonProperty("me")] public RankingItem Me;
		[JsonProperty("ranking")] public IEnumerable<RankingItem> ranking;
	}
}