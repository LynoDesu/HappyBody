using System.Threading.Tasks;
using HappyBodyApp.Abstractions;
using Microsoft.WindowsAzure.MobileServices;
using HappyBodyApp.iOS.Services;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(iOSLoginProvider))]
namespace HappyBodyApp.iOS.Services
{
    public class iOSLoginProvider : ILoginProvider
    {
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client)
        {
            return await client.LoginAsync(RootView, "google", "happybody");
        }

        public UIViewController RootView => UIApplication.SharedApplication.KeyWindow.RootViewController;
    }
}