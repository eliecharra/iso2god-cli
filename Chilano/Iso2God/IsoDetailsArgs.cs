namespace Chilano.Iso2God
{
    using System;

    internal class IsoDetailsArgs
    {
        public string PathISO;
        public string PathTemp;
        public string PathXexTool;

        public IsoDetailsArgs(string ISO, string Temp, string XT)
        {
            this.PathISO = ISO;
            this.PathTemp = Temp;
            this.PathXexTool = XT;
        }
    }
}

