using System;
using System.Reflection;
using UnityEngine.Networking;

namespace RESTfull
{
    [Serializable]
    public class BaseRequestResponse
    {
        public string GetQueryString()
        {
            FieldInfo[] fields = this.GetType().GetFields();
            string queryString = "?";

            foreach(FieldInfo field in fields)
            {
                queryString += (queryString.Length==1?"":"&") + $"{UnityWebRequest.EscapeURL(field.Name)}={UnityWebRequest.EscapeURL(field.GetValue(this).ToString())}";
            }

            return queryString;
        }
    }
}
