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
using Android.Util;
using IOTProjectt;
using System.Threading.Tasks;
using Android;
using IOTProjectt.Droid;

[Activity(Label = "DriveOpen", MainLauncher = true, Icon = "@mipmap/icon")]
public class MainActivity : Activity, GoogleApiClient.IConnectionCallbacks, IResultCallback, IDriveApiDriveContentsResult
{

    const string TAG = "GDriveExample";
    const int REQUEST_CODE_RESOLUTION = 3;

    GoogleApiClient _googleApiClient;

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        if(GlobalVariables.IsEnd == true)
        {
            if (_googleApiClient == null)
            {
                _googleApiClient = new GoogleApiClient.Builder(this)
                  .AddApi(DriveClass.API)
                  .AddScope(DriveClass.ScopeFile)
                  .AddConnectionCallbacks(this)
                  .AddOnConnectionFailedListener(onConnectionFailed)
                  .Build();
            }
            if (!_googleApiClient.IsConnected)
                _googleApiClient.Connect();
        };
    }

    protected void onConnectionFailed(ConnectionResult result)
    {
        Log.Info(TAG, "GoogleApiClient connection failed: " + result);
        if (!result.HasResolution)
        {
            GoogleApiAvailability.Instance.GetErrorDialog(this, result.ErrorCode, 0).Show();
            return;
        }
        try
        {
            result.StartResolutionForResult(this, REQUEST_CODE_RESOLUTION);
        }
        catch (IntentSender.SendIntentException e)
        {
            Log.Error(TAG, "Exception while starting resolution activity", e);
        }
    }

    public void OnConnected(Bundle connectionHint)
    {
        Log.Info(TAG, "Connected.");
        DriveClass.DriveApi.NewDriveContents(_googleApiClient).SetResultCallback(this);
    }

    protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
    {
        base.OnActivityResult(requestCode, resultCode, data);
        if (requestCode == REQUEST_CODE_RESOLUTION)
        {
            switch (resultCode)
            {
                case Result.Ok:
                    _googleApiClient.Connect();
                    break;
                case Result.Canceled:
                    Log.Error(TAG, "Unable to sign in");
                    break;
                case Result.FirstUser:
                    Log.Error(TAG, "Unable to sign in");
                    break;
                default:
                    Log.Error(TAG, resultCode);
                    return;
            }
        }
    }

    void IResultCallback.OnResult(Java.Lang.Object result)
    {
        var contentResults = (result).JavaCast<IDriveApiDriveContentsResult>();
        if (!contentResults.Status.IsSuccess)
            return;
        Task.Run(() =>
        {
            var writer = new OutputStreamWriter(contentResults.DriveContents.OutputStream);
            writer.Close();
            MetadataChangeSet changeSet = new MetadataChangeSet.Builder()
                   .SetTitle("New Text File")
                   .SetMimeType("text/plain")
                   .Build();
            DriveClass.DriveApi
                      .GetRootFolder(_googleApiClient)
                      .CreateFile(_googleApiClient, changeSet, contentResults.DriveContents);
        });
    }

    public void OnConnectionSuspended(int cause)
    {
        throw new NotImplementedException();
    }

    public IDriveContents DriveContents
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public Statuses Status
    {
        get
        {
            throw new NotImplementedException();
        }
    }
}