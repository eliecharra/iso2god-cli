namespace Chilano.Iso2God
{
   internal class IsoDetailsResults
    {
        public IsoDetailsPlatform ConsolePlatform;
        public string DiscCount;
        public string DiscNumber;
        public string ErrorMessage;
        public string ExType;
        public string MediaID;
        public string Name;
        public string Platform;
        public string ProgressMessage;
        public IsoDetailsResultsType Results;
        public string TitleID;

        public IsoDetailsResults()
        {
        }

        public IsoDetailsResults(IsoDetailsResultsType Type, string Message)
        {
            Results = Type;
            if (Type == IsoDetailsResultsType.Error)
            {
                ErrorMessage = Message;
            }
            if (Type == IsoDetailsResultsType.Progress)
            {
                ProgressMessage = Message;
            }
        }

        public IsoDetailsResults(string Name, string TitleID, string DiscNumber)
        {
            Results = IsoDetailsResultsType.Completed;
            ConsolePlatform = IsoDetailsPlatform.Xbox;
            this.Name = Name;
            this.TitleID = TitleID;
            MediaID = "00000000";
            Platform = "0";
            ExType = "0";
            this.DiscNumber = DiscNumber;
        }

        public IsoDetailsResults(string Name, string TitleID, string MediaID, string Platform, string ExType, string DiscNumber, string DiscCount)
        {
            Results = IsoDetailsResultsType.Completed;
            ConsolePlatform = IsoDetailsPlatform.Xbox360;
            this.Name = Name;
            this.TitleID = TitleID;
            this.MediaID = MediaID;
            this.Platform = Platform;
            this.ExType = ExType;
            this.DiscNumber = DiscNumber;
            this.DiscCount = DiscCount;
        }
    }
}

