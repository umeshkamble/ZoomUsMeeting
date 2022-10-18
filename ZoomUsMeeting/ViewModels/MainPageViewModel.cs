using System;
using System.Windows.Input;
using Xamarin.Forms;
using ZoomUsMeeting.Interfaces;

namespace ZoomUsMeeting.ViewModels
{
    public class MainPageViewModel
    {
        public ICommand JoinMeetingCommand { get; set; }
        public string MeetingId { get; set; }
        public string MeetingPassword { get; set; }
        IZoomService zoomService;

        public MainPageViewModel()
        {
            MeetingId = "76188588355";
            MeetingPassword = "FAT7WmN62H63UgBUaesXwIaAqxYgS8.1";
            zoomService = DependencyService.Get<IZoomService>();
            zoomService.InitZoomLib("", "");
            JoinMeetingCommand = new Command(JoinMeeting);
        }

        private async void JoinMeeting()
        {
            await zoomService.JoinMeeting(MeetingId, "ZoomSample", MeetingPassword);
        }

    }
}
