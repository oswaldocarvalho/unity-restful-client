using System.Threading.Tasks;
using Forms;
using RESTClient;
using RESTClient.Requests;
using RESTClient.Responses;
using RESTfull;
using UnityEngine;

namespace Account
{
    public class SignInPanel : Form
    {
        public GameObject RegisterPanel;
        public GameObject GamePanel;

        private FormField _title;

        private FormField _email;
        private FormField _password;

        // Use this for initialization
        new void Start()
        {
            base.Start();
            
            _title = AddField("Title");
            _email = AddField("Email");
            _password = AddField("Password");

            Global.RestClient = new TokenAuthRestClient(Global.API_URL);
            
            ResetForm();
        }

        public async void OnSignInClick()
        {
            ResetErrors();

            SignInRequest request = new SignInRequest()
            {
                Email = _email.GetValue(),
                Password = _password.GetValue()
            };

            SignInResponse response;

            try
            {
                response = await Global.RestClient.Post<SignInResponse>("/account/sign-in", request);
                Global.Me = response;
            }
            catch(ResponseException e)
            {
                _title.SetMessage(e.Message, MessageType.Error);
                return;
            }

            ResetForm();

            _title.SetMessage($"Wellcome {response.Nickname}!");

            await Task.Delay(1000);
            
            ResetForm();
            
            gameObject.SetActive(false);
            GamePanel.SetActive(true);

        }

        public void OnRegisterClick()
        {
            gameObject.SetActive(false);
            RegisterPanel.SetActive(true);
        }
    }
}

