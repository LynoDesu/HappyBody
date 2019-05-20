using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

namespace HappyBodyApp.Abstractions
{
    public interface ILoginProvider
    {
        Task<MobileServiceUser> LoginAsync(MobileServiceClient client);
    }
}