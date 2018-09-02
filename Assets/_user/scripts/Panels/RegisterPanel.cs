using System.Threading.Tasks;
using Forms;
using RESTClient.Requests;
using RESTClient.Responses;
using RESTfull;
using UnityEngine;

namespace Account
{
    public class RegisterPanel : Form
    {
        public GameObject SignInPanel;

        private FormField _title;

        private FormField _fullName;
        private FormField _nickname;
        private FormField _email;
        private FormField _password;
        private FormField _retypedPassword;

        // Use this for initialization
        new void Start()
        {
            base.Start();
            
            _title = AddField("Title");
            _fullName = AddField("FullName");
            _nickname = AddField("Nickname");
            _email = AddField("Email");
            _password = AddField("Password");
            _retypedPassword = AddField("RetypePassword");

            ResetForm();
        }

        private bool ValidateForm()
        {
            ResetErrors();
            
            if (_password.GetValue() != _retypedPassword.GetValue())
            {
                _retypedPassword.SetMessage("Passwords do not match", MessageType.Error);
                return false;
            }

            return true;
        }

        public async void OnRegisterClick()
        {
            if (!ValidateForm())
            {
                return;
            }

            // try to save te new user
            RegisterRequest request = new RegisterRequest() {
                FullName = _fullName.GetValue(),
                Nickname = _nickname.GetValue(),
                Email = _email.GetValue(),
                Password = _password.GetValue(),
            };

            RegisterResponse response;

            try
            {
                response = await Global.RestClient.Post<RegisterResponse>("/account/register", request);
            }
            catch(ResponseException e)
            {
                _title.SetMessage(e.Message, MessageType.Error);
                return;
            }

            ResetForm();

            _title.SetMessage($"Thanks {response.Nickname}!");
                
            //
            await Task.Delay(3000);
            
            ResetForm();
            
            OnSignInClick();
        }

        public void OnSignInClick()
        {
            gameObject.SetActive(false);
            SignInPanel.SetActive(true);
        }
    }   
}