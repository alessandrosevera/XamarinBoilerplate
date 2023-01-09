using System;
using System.IO;
using MobileAppForms.Model;
using MobileAppForms.Service.Core;

namespace MobileAppForms.Service
{
    public class DbFileHelperIos : DbFileHelper
    {
        #region ctor(s)

        public DbFileHelperIos()
        {
        }

        #endregion


        #region DbFileHelper implementation

        public string GetDbPath()
        {
            string dbPath = null;
            try
            {
                string folderName = AppConstants.DbFolderName;

                string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), folderName);
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
