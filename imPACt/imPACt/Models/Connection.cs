using System;
using System.Collections.Generic;
using System.Text;

namespace imPACt.Models
{
    class Connection
    {
        public string MenteeUid { get; set; }
        public string MentorUid { get; set; }
        public List<string> Conversation { get; set; }
    }
}
