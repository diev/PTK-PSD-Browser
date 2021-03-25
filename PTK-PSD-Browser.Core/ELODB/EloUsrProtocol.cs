using System;

#nullable disable

namespace PTK_PSD_Browser.Core.ELODB
{
    public partial class EloUsrProtocol
    {
        public decimal Userid { get; set; }
        public DateTime Time { get; set; }
        public string Modul { get; set; }
        public string Object { get; set; }
        public string Job { get; set; }
        public decimal Bik { get; set; }
    }
}
