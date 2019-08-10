using System;
using RESTfull;

namespace RESTClient.Requests
{
	[Serializable]
	public class RankingRequest : BaseRequestResponse
	{
        public int score;
	}
}