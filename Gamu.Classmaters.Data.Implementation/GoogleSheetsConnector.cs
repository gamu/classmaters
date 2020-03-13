using System;
using System.IO;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;
using Gamu.Classmaters.Config;
using Google.Apis.Services;

namespace Gamu.Classmaters.Data.Implementation
{
    internal class GoogleSheetsConnector
    {
        private static string clientSecretPath;
        private static SheetsService sheetServiceCache = null;
        private static object lockObject = new object();
        private static ClassmatersConfig configuration;

        static GoogleSheetsConnector()
        {
            configuration = CofigurationContext.Current();
            clientSecretPath =
                Environment.GetEnvironmentVariable(configuration.GoogleSheetSecretVar);
        }

        public static SheetsService GetService()
        {
            if(sheetServiceCache == null)
            {
                string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };

                lock(lockObject)
                {
                    using (var stream = new FileStream(clientSecretPath, FileMode.Open, FileAccess.Read))
                    {
                        var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                            GoogleClientSecrets.Load(stream).Secrets,
                            Scopes,
                            "user",
                            CancellationToken.None,
                            new FileDataStore("token.json", true)).Result;
                        if(sheetServiceCache == null)
                        {
                            sheetServiceCache = new SheetsService(new BaseClientService.Initializer()
                            {
                                HttpClientInitializer = credential,
                                ApplicationName = configuration.AppName
                            });
                        }
                    }
                }

                using (var stream = new FileStream(clientSecretPath, FileMode.Open, FileAccess.Read))
                {
                    var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore("token.json", true)).Result;
                    var service = new SheetsService(new BaseClientService.Initializer()
                    {
                        HttpClientInitializer = credential,
                        ApplicationName = "Classmaters",
                    });
                }
            }
            

            return sheetServiceCache;
        }
    }
}
