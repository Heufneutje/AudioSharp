using System;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;

namespace AudioSharp.Utils
{
    public static class UpdateUtils
    {
        private const string _GITHUB_BASE_URL = "https://api.github.com";

        public static string CheckForUpdate(int curMajor, int curMinor, int curPatch)
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
                return VersionsEqual(curMajor, curMinor, curPatch, latestVersion) ? null : latestVersion;
            }
            catch (Exception)
            {
                // TODO: Error logging.
                return null;
            }
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
