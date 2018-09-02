# Unity 3D RESTful client

Functional library to make REST requests.

## Running the project
1. Clone this repository;
2. Open the scene SampleScene;
3. Create a account;
4. Make login;
5. Play the game.

The game is a simple random number generator (who archive the highest number wins). The project is using the api located in http://basic-account-api.reptilaw.com, see the Global.cs. The source code of the server is located [**here**](https://github.com/oswaldocarvalho/basic-account-api).

## Dependencies
* JsonDotNet
* System.Net.Http.dll (see Assets/msc.rsp file)

## Creating a new request

Request object
```c#
using System;
using RESTfull;
using Newtonsoft.Json;

namespace RESTClient.Requests
{
    [Serializable]
    public class SignInRequest : BaseRequestResponse
    {
        [JsonProperty("email")] public string Email { get; set; }
        [JsonProperty("password")] public string Password;
    }
}
```

Response object
```c#
using System;
using Newtonsoft.Json;
using RESTfull;

namespace RESTClient.Responses
{
    [Serializable]
    public class SignInResponse : BaseRequestResponse
    {
        [JsonProperty("id")] public string Id;
        [JsonProperty("full_name")] public string FullName { get; set; }
        [JsonProperty("nickname")] public string Nickname { get; set; }
    }
}
```

The anotations like `[JsonProperty("json_property_here")]` are necessary if the json property name and C# property name are diferent.

Example code (making a request)
```c#
RestClient restClient = new RestClient("http://api.exmple.com");

SignInRequest signInRequest = new SignInRequest()
{
    Email = "email@here.com",
    Password = "secret"
};

try
{
    SignInResponse signInResponse = await restClient.Post<SignInResponse>("/account/sign-in", signInRequest);
}
catch(ResponseException e)
{
    // handle the exception
    return;
}
```

## Verbs currently implemented
* Get
* Post
* Put
* Patch
* Delete

---
Thanks