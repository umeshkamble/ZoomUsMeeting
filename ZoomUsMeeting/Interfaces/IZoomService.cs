using System;
using System.Threading.Tasks;

namespace ZoomUsMeeting.Interfaces
{
    public interface IZoomService
    {
        Task<bool> InitZoomLib(string appKey, string appSecret);
        Task JoinMeeting(string meetingID, string meetingPassword, string displayName = "Zoom Demo");
        bool IsInMeeting();
    }
}
