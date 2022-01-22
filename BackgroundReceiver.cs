using Android.Content;
using Android.OS;
using Xamarin.Forms;
using IOTProjectt;

namespace BackgroundTasks.Droid
{
    [BroadcastReceiver]
    public class BackgroundReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            PowerManager pm = (PowerManager)context.GetSystemService(Context.PowerService);
            PowerManager.WakeLock wakeLock = pm.NewWakeLock(WakeLockFlags.Partial, "BackgroundReceiver");
            wakeLock.Acquire();
            DetectShakeTest BackGround = new DetectShakeTest();
            BackGround.ToggleAccelerometer();
            wakeLock.Release();
        }
    }
}