﻿using RESTClient;
using RESTClient.Responses;

public static class Global
{
    public const string API_URL = "http://basic-account-api.reptilaw.com";
    public static SignInResponse Me;
    public static TokenAuthRestClient RestClient;
}