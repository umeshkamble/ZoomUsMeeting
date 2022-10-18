using System;
using System.Threading.Tasks;
using US.Zoom.Sdk;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZoomUsMeeting.Droid.Platform;
using ZoomUsMeeting.Interfaces;

[assembly: Dependency(typeof(ZoomService))]

namespace ZoomUsMeeting.Droid.Platform
{
    public class ZoomService : Java.Lang.Object, IZoomService, IZoomSDKInitializeListener
    {
        ZoomSDK zoomSDK;
        static TaskCompletionSource<object> meetingListSource;
        private bool inMeeting = false;
        public async Task<bool> InitZoomLib(string appKey, string appSecret)
        {
            zoomSDK = ZoomSDK.Instance;
            var zoomInitParams = new ZoomSDKInitParams
            {
                AppKey = appKey,
                AppSecret = appSecret,
                EnableLog = true,
                AppLocal = ZoomAppLocal.ZoomLocaleDef
            };
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                zoomSDK = ZoomSDK.Instance;
                zoomSDK.Initialize(Android.App.Application.Context, this, zoomInitParams);
            }).ConfigureAwait(false);
            return true;
        }

        public bool IsInMeeting()
        {
            return zoomSDK?.IsInitialized ?? false;
            //return inMeeting;
        }
        private bool IsInitialized()
        {
            return zoomSDK?.IsInitialized ?? false;
        }

        public async Task JoinMeeting(string meetingID, string meetingPassword, string displayName = "Zoom Demo")
        {
            try
            {
                if (IsInitialized())
                {
                    var meetingService = zoomSDK.MeetingService;

                    await MainThread.InvokeOnMainThreadAsync(() =>
                    {
                        meetingService.JoinMeetingWithParams(Android.App.Application.Context,
                            new JoinMeetingParams
                            {
                                MeetingNo = meetingID,
                                Password = meetingPassword,
                                DisplayName = "Peter Parker"
                            }, new JoinMeetingOptions());
                    }).ConfigureAwait(false);
                }
            }
            catch (System.Exception ex)
            {

            }
        }

        public void OnZoomAuthIdentityExpired()
        {
            //throw new NotImplementedException();
        }

        public void OnZoomSDKInitializeResult(int p0, int p1)
        {
            if (p0 == ZoomError.ZoomErrorSuccess)
            {
                //await MainThread.InvokeOnMainThreadAsync(() =>
                //{
                //    //perform various setup steps
                //    // only custom ui is tested, which requires adding your own Meeting Activity and video renderers in the android.xamarin project
                //    // default UI may work fine, but it is untested
                //    zoomSDK.MeetingSettingsHelper.CustomizedMeetingUIEnabled = true;  // set this to false to use the zoom provided, Default UI
                //    zoomSDK.MeetingSettingsHelper.EnableForceAutoStartMyVideoWhenJoinMeeting(false);

                //    //Add listeners according to your needs
                //    //zoomSDK.InMeetingService.AddListener(new YourInMeetingServiceListener());
                //    //zoomSDK.MeetingService.AddListener(new YourMeetingServiceListener());
                //});

            }
            else
            {
                // something bad happened
            }
        }
    }
}
