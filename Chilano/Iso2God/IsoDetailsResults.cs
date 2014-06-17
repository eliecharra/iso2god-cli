namespace Chilano.Iso2God
{
    using System;
    using System.Drawing;

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
            this.Results = Type;
            if (Type == IsoDetailsResultsType.Error)
            {
                this.ErrorMessage = Message;
            }
            if (Type == IsoDetailsResultsType.Progress)
            {
                this.ProgressMessage = Message;
            }
        }

        public IsoDetailsResults(string Name, string TitleID, string DiscNumber)
        {
            this.Results = IsoDetailsResultsType.Completed;
            this.ConsolePlatform = IsoDetailsPlatform.Xbox;
            this.Name = Name;
            this.TitleID = TitleID;
            this.MediaID = "00000000";
            this.Platform = "0";
            this.ExType = "0";
            this.DiscNumber = DiscNumber;
        }

        public IsoDetailsResults(string Name, string TitleID, string MediaID, string Platform, string ExType, string DiscNumber, string DiscCount)
        {
            this.Results = IsoDetailsResultsType.Completed;
            this.ConsolePlatform = IsoDetailsPlatform.Xbox360;
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

