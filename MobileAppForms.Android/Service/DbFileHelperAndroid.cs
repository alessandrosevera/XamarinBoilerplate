using System;
using System.IO;
using Android.App;
using MobileAppForms.Model;
using MobileAppForms.Service.Core;

namespace MobileAppForms.Service
{
    public class DbFileHelperAndroid : DbFileHelper
    {
        #region auto-properties

        private Activity Activity { get; }

        #endregion


        #region ctor(s)

        public DbFileHelperAndroid(Activity activity)
        {
            Activity = activity;
        }

        #endregion


        #region DbFileHelper implementation

        public string GetDbPath()
        {
            string dbPath = null;
            try
            {
                // string folderName = Activity.PackageName;
                string folderName = AppConstants.DbFolderName;

                string folderPath = Path.Combine(Activity.ApplicationContext.GetExternalFilesDir(null).AbsolutePath, folderName);
                bool folderCreated = Directory.Exists(folderPath);
                if (!folderCreated)
                {
                    var dirInfo = Directory.CreateDirectory(folderPath);
                    folderCreated = dirInfo.Exists;
                }

                if (folderCreated)
                {
                    string dbFullName = AppConstants.DbName + ".db3";
                    string dbFilePath = Path.Combine(folderPath, dbFullName);
                    bool dbFileExist = File.Exists(dbFilePath);
                    if (!dbFileExist)
                    {
                        var fileInfo = File.Create(dbFilePath);
                        dbFileExist = fileInfo.CanRead && fileInfo.CanWrite;
                    }

                    if (dbFileExist)
                        dbPath = dbFilePath;
                }

                return dbPath;
            }
            catch (Exception)
            {
                return null;
            }

        }

        #endregion
    }
}
