using System;
using System.IO;
using System.Reflection;

namespace AudioSharp.Utils
{
    public static class ShortcutUtils
    {
        public static void AddStartupShortcut()
        {
            FillLocations(out string startUpFolderPath, out string appPath, out string appName, out string shortcutLocation);
            if (FindShortcutPath(shortcutLocation, appPath) != null)
                return;

            if (File.Exists(shortcutLocation))
            {
                int existsCount = 1;
                while (File.Exists(shortcutLocation))
                {
                    existsCount++;
                    shortcutLocation = Path.Combine(startUpFolderPath, $"{appName} ({existsCount}).lnk");
                }
            }

            IWshRuntimeLibrary.WshShell wshShell = new IWshRuntimeLibrary.WshShell();
            IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)wshShell.CreateShortcut(shortcutLocation);
            shortcut.TargetPath = appPath;
            shortcut.Description = appName;
            shortcut.Save();
        }

        public static void RemoveStartupShortcut()
        {
            FillLocations(out string startUpFolderPath, out string appPath, out string appName, out string shortcutLocation);
            shortcutLocation = FindShortcutPath(shortcutLocation, appPath);
            if (shortcutLocation != null && File.Exists(shortcutLocation))
                File.Delete(shortcutLocation);
        }

        private static void FillLocations(out string startUpFolderPath, out string appPath, out string appName, out string shortcutLocation)
        {
            startUpFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            appPath = Assembly.GetEntryAssembly().Location;
            appName = Path.GetFileNameWithoutExtension(appPath);
            shortcutLocation = Path.Combine(startUpFolderPath, $"{appName}.lnk");
        }

        private static string FindShortcutPath(string defaultShortcutPath, string appPath)
        {
            if (File.Exists(defaultShortcutPath) && GetShortcutTargetFile(defaultShortcutPath) == appPath)
                return defaultShortcutPath;

            // Check if it has been renamed for some reason.
            foreach (string shortcut in Directory.EnumerateFiles(Path.GetDirectoryName(defaultShortcutPath), "*.lnk"))
                if (GetShortcutTargetFile(shortcut) == appPath)
                    return shortcut;

            return null;
        }

        private static string GetShortcutTargetFile(string shortcutFilename)
        {
            string pathOnly = Path.GetDirectoryName(shortcutFilename);
            string filenameOnly = Path.GetFileName(shortcutFilename);

            Shell32.Shell shell = new Shell32.Shell();
            Shell32.Folder folder = shell.NameSpace(pathOnly);
            Shell32.FolderItem folderItem = folder.ParseName(filenameOnly);
            if (folderItem != null)
            {
                Shell32.ShellLinkObject link = (Shell32.ShellLinkObject)folderItem.GetLink;
                return link.Path;
            }

            return string.Empty;
        }
    }
}
