﻿using System.Threading.Tasks;
using Android.Content;
using Microsoft.WindowsAzure.MobileServices;
using HappyBodyApp.Abstractions;
using HappyBodyApp.Droid.Services;

[assembly: Xamarin.Forms.Dependency(typeof(DroidLoginProvider))]
namespace HappyBodyApp.Droid.Services
{
    public class DroidLoginProvider : ILoginProvider
    {
        Context context;

        public void Init(Context context)
        {
            this.context = context;
        }

        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client)
        {
            return await client.LoginAsync(context, MobileServiceAuthenticationProvider.Google, "happybody");
        }
    }
}