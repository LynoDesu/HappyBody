using System;
using System.IO;
using HappyBody.Droid.Services;
using HappyBody.Models;
using HappyBody.Services;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseAccess))]
namespace HappyBody.Droid.Services
{
    public class DatabaseAccess : IDatabaseAccess
    {
        public string DatabasePath()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Constants.LocalDbName);

            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }

            return path;
        }
    }
}
