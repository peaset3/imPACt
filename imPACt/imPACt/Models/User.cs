using System;
using System.Collections.Generic;
using System.Text;

namespace imPACt.Models
{
   public class User
    {
        public string Email { get; set; }
        public string School { get; set; }
        public string Degree { get; set; }
        public string Surname { get; set; }
        public string Lastname { get; set; }
        public string Uid { get; set; }
        public byte AccountType { get; set; }

        public string Fullname
        {
            get { return Surname + " " + Lastname; }
        }
    }
}
