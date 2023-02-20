using System;
using System.Collections.Generic;
using System.Text;

namespace LegionSociety.Contacts.Models
{
    public class QrResponseModel
    {
        public byte[] QrImage { get; set; }
        public string TotpCode { get; set; }
    }
}
