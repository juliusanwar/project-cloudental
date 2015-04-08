using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudClinic.Models.ViewModel
{
    public class MessageBoxViewModel
    {
        public string TitleMessage { get; set; }
        public string MessageBody { get; set; }
        public string AlertClass { get; set; }
    }
}