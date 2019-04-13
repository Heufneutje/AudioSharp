namespace AudioSharp.Utils
{
    public class UpdateCheckResult
    {
        public UpdateResultType ResultType { get; private set; }
        public string Message { get; private set; }
        public string MessageTitle { get; private set; }
        public string DownloadUrl { get; private set; }

        public UpdateCheckResult(UpdateResultType resultType, string message, string messageTitle, string downloadUrl)
        {
            ResultType = resultType;
            Message = message;
            MessageTitle = messageTitle;
            DownloadUrl = downloadUrl;
        }
    }
}
