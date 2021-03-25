using System;

#nullable disable

namespace PTK_PSD_Browser.Core.ELODB
{
    public partial class WtElodbLog
    {
        public int RecId { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateUserDate { get; set; }
        public string UserAction { get; set; }
        public string Val { get; set; }
    }
}
