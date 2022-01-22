using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using File = Google.Apis.Drive.v3.Data.File;

namespace IOTProjectt
{
    internal class Class1
    {
        private string[] Scopes = { DriveService.Scope.Drive };
        private string ApplicationName = "GoogleDriveAPIStart";

        static void Main()
        {
            UserCredential credential = GetUserCredential();

            DriveService service = GetDriveService(credential);
        }

        [Obsolete]
        private UserCredential GetUserCredential()
        {
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string creadPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                creadPath = Path.Combine(creadPath, "driveCredentials", "drive.credentials.json");

                return GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "User",
                    CancellationToken.None,
                    new FileDataStore(creadPath, true)).Result;


            }
        }

        private static DriveService GetDriveService(UserCredential credential)
        {
            return new DriveService(
                new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                });
        }

    }
}