using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using AudioSharp.Translations;
using Newtonsoft.Json.Linq;

namespace AudioSharp.Utils
{
    public static class UpdateUtils
    {
        private const string _GITHUB_BASE_URL = "https://api.github.com";

        public static UpdateCheckResult CheckForUpdate()
        {
            try
            {
                string json;
                using (WebClient client = new WebClient())
                {
                    SetHeaders(client);
                    json = client.DownloadString($"{_GITHUB_BASE_URL}/repos/heufneutje/audiosharp/tags");
                }

                JArray tags = (JArray)JsonUtils.DeserializeObject(json);
                if (!tags.Any())
                    return null;

                string latestVersion = tags.First["name"].ToString().Substring(1);
                FileVersionInfo fvi = GetCurrentVersion();
                string currentVersion = string.Join(".", new int[3] { fvi.FileMajorPart, fvi.FileMinorPart, fvi.FileBuildPart });
                if (VersionsEqual(fvi.FileMajorPart, fvi.FileMinorPart, fvi.FileBuildPart, latestVersion))
                    return new UpdateCheckResult(UpdateResultType.NoUpdateAvailable, string.Format(Messages.GUINoUpdateAvailable, currentVersion, Environment.NewLine), Messages.GUINoUpdateAvailableTitle);

                return new UpdateCheckResult(UpdateResultType.UpdateAvailable, string.Format(Messages.GUIUpdateAvailable, currentVersion, Environment.NewLine, latestVersion), Messages.GUIUpdateAvailableTitle);
            }
            catch (WebException webEx)
            {
                return new UpdateCheckResult(UpdateResultType.Error, string.Format(Messages.GUIErrorWeb, Environment.NewLine, webEx.Message), Messages.GUIErrorCommon);
            }
            catch (Exception ex)
            {
                return new UpdateCheckResult(UpdateResultType.Error, string.Format(Messages.GUIErrorUnknown, Environment.NewLine, ex.Message), Messages.GUIErrorCommon);
            }
        }

        public static FileVersionInfo GetCurrentVersion()
        {
            return FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);
        }

        private static bool VersionsEqual(int curMajor, int curMinor, int curPatch, string latest)
        {
            string[] parts = latest.Split('.');
            if (Convert.ToInt32(parts[0]) > curMajor)
                return false;

            if (Convert.ToInt32(parts[1]) > curMinor)
                return false;

            if (Convert.ToInt32(parts[2]) > curPatch)
                return false;

            return true;
        }

        private static void SetHeaders(WebClient client)
        {
            client.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0");
        }
    }
}
