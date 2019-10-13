using System;
using System.IO;
using System.Net;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Newtonsoft.Json.Linq;

namespace laboratorul2
{
    class Program
    {
        private static DriveService _service;
        private static string _token;
        static void Main(string[] args)
        {
            init();
        }
        static void init()
        {
            string[] scopes=new string[]{
                DriveService.Scope.Drive,
                DriveService.Scope.DriveFile

            };
            var clientId="443094691967-fepi46eejfefp3f4f4u7kr7qdndvt4r4.apps.googleusercontent.com";
            var clientSecret="O4K_PJwdsd_3ndCULNCVV3NX";
            var credentials=GoogleWebAuthorizationBroker.AuthorizeAsync(
            new ClientSecrets
            {
                    ClientId=clientId,
                    ClientSecret=clientSecret
            }, 
            scopes,
            Environment.UserName,
            CancellationToken.None,
            null   
            ).Result;
            Console.Write("Token :"+credentials.Token.AccessToken);
            //first
            _service=new DriveService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                    HttpClientInitializer=credentials
            });
            _token=credentials.Token.AccessToken;
            GetMyFiles();
        }
        static void GetMyFiles()
        {
            var request=(HttpWebRequest)WebRequest.Create("https://www.googleapis.com/drive/v3/files");
            request.Headers.Add(HttpRequestHeader.Authorization,"Bearer " +_token );
            using (var response=request.GetResponse())
            {
                using(Stream data=response.GetResponseStream())
                using(var reader=new StreamReader(data))
                {
                    string text=reader.ReadToEnd();
                    var myData=JObject.Parse(text);
                    foreach(var file in myData["files"])
                    {
                        if(file["mimeType"].ToString()!="application/vnd.google-apps.folder")
                        {    
                            Console.WriteLine("File name : " +file["name"]);
                        }
                    }
                }
            }
        }
    }
}
