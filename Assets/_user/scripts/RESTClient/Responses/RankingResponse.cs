using System;
using System.Collections.Generic;
using RESTfull;

namespace RESTClient.Responses
{
	[Serializable]
	public class RankingItem
	{
		public int rank;
		public string nickname;
		public int score;
	}

	[Serializable]
	public class RankingResponse : BaseRequestResponse
	{
		public RankingItem me;
		public RankingItem[] ranking;
	}
}