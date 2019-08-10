using System.Threading.Tasks;
using Forms;
using RESTClient.Requests;
using RESTClient.Responses;
using RESTfull;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : Form 
{
	public GameObject SignInPanel;
	public Text LeaderBoardText;
	public Text ResultText;
	public Button PlayButton;

	private System.Random _random;
	private FormField _title;

	// Use this for initialization
	new void Start()
	{
		base.Start();
            
		_title = AddField("Title");
		
		_random = new System.Random();
		
		ResetForm();
	}
	
	public async void OnPlayClick()
	{
		if (!PlayButton.enabled)
		{
			return;
		}
		
		PlayButton.enabled = false;
		
		_title.SetMessage("Playng...");

		int score = 0;
		for (int i = 0; i < 20; i++)
		{
			score += _random.Next(0, 10000);
			ResultText.text = score.ToString();
			await Task.Delay(i * 10);
		}
		
		// plus 5k
		score += _random.Next(0, 5000);
		ResultText.text = score.ToString();
		
		_title.SetMessage("Sending", MessageType.Warning);
		await SendRanking(score);
		
		PlayButton.enabled = true;
		
		await GetRanking();
		
		_title.SetMessage("Complete", MessageType.Success);
	}

	public async void OnSignoutClick()
	{
		try
		{
			await Global.RestClient.Delete<BaseRequestResponse>("/account/sign-out");
			gameObject.SetActive(false);
			SignInPanel.SetActive(true);
		}
		catch(ResponseException e)
		{
			_title.SetMessage(e.Message, MessageType.Error);
		}
	}

	private void AddRecord(int Id, string Nickname, int Score)
	{
		LeaderBoardText.text += $"#{Id.ToString().PadLeft(5, ' ')} {Nickname.PadRight(24, '.')}{Score.ToString().PadLeft(8, '.')} \n";
		LeaderBoardText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, LeaderBoardText.rectTransform.rect.height+30);
	}

	private async Task SendRanking(int score)
	{
		try
		{
			await Global.RestClient.Patch<BaseRequestResponse>("/ranking", new RankingRequest() { score = score });
		}
		catch(ResponseException e)
		{
			_title.SetMessage(e.Message, MessageType.Error);
		}
	}

	private async Task GetRanking()
	{
		LeaderBoardText.text = "";
		RankingResponse response;

		try
		{
			response = await Global.RestClient.Get<RankingResponse>("/ranking");
		}
		catch(ResponseException e)
		{
			_title.SetMessage(e.Message, MessageType.Error);
			return;
		}

		foreach (var item in response.ranking)
		{
			if (item.nickname == Global.Me.nickname)
			{
				LeaderBoardText.text += " Me ".PadLeft(22, '~').PadRight(39, '~') + "\n";
			}
			
			AddRecord(item.rank, item.nickname, item.score);
		}
	}
}
