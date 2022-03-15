﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSaveConverter.Xbox
{
    internal class XboxPackageList
    {
        private static readonly NLog.Logger logger = LogHelper.getClassLogger();
        private static Library.GameInfo[] internalList;
        static XboxPackageList()
        {
            logger.Info("Loading UWP Package List...");
            string packageFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Packages");
            List<Library.GameInfo> list = new List<Library.GameInfo>();
            foreach(string package in Directory.GetDirectories(packageFolder))
            {
                string wgsFolder = Path.Combine(package, "SystemAppData", "wgs");
                if(Directory.Exists(wgsFolder) && Directory.GetDirectories(wgsFolder).Length >= 2)
                {
                    Library.GameInfo info = Library.GameLibrary.getGameInfo(Path.GetFileName(package));
                    list.Add(info);
                }
            }
            internalList = list.ToArray();
            logger.Info("UWP Packages loaded!");
        }
        internal static string getWGSFolder(string packageName)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Packages", packageName, "SystemAppData", "wgs") + "\\";
        }

        public static Library.GameInfo[] GetList()
        {
            return internalList.ToArray();
        }
    }
}
