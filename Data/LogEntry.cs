namespace MagickUtils.Data
{
    public struct LogEntry
    {
        public string logMessage;
        public bool hidden;
        public bool replaceLastLine;
        public string filename;

        public LogEntry(string logMessageArg, bool hiddenArg = false, bool replaceLastLineArg = false, string filenameArg = "")
        {
            logMessage = logMessageArg;
            hidden = hiddenArg;
            replaceLastLine = replaceLastLineArg;
            filename = filenameArg;
        }
    }
}
