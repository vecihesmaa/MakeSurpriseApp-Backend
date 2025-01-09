using Microsoft.AspNetCore.SignalR;

namespace MakeSurpriseProject.Hubs
{
    public class SpecialDaysHub : Hub
    {
        public async Task NotifyFrontend(string message)
        {
            await Clients.All.SendAsync("receiveNotification", message);
        }
        //public async Task NotifyUser(string userId, string message)
        //{
        //    await Clients.User(userId).SendAsync("receiveNotification", message);
        //}
    }
}
