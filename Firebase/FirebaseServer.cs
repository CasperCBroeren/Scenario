using System;
using System.Net;
using RestSharp;

namespace Firebase
{
    public class FirebaseServer
    {
        public string SeverKey { get; }
        private RestClient client { get; } 

        public FirebaseServer(string serverKey)
        {
            this.SeverKey = serverKey;
            this.client = new RestSharp.RestClient("https://fcm.googleapis.com/");
        } 

        public bool SendMessage(FireBaseMessage message)
        {
            var request = new RestSharp.RestRequest($"/fcm/send", Method.POST);
            request.AddHeader("ContentType", "application/json");
            request.AddHeader("Authorization", $"key={SeverKey}"); 
            request.AddObject(message);
            var response = client.Execute(request);

            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
