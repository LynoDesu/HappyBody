using System;
using System.IO;
using HappyBody.iOS.Services;
using HappyBody.Models;
using HappyBody.Services;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseAccess))]
namespace HappyBody.iOS.Services
{
    public class DatabaseAccess : IDatabaseAccess
    {
        public string DatabasePath()
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, Constants.LocalDbName);
        }
    }
}
