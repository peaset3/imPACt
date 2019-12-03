using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

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
        public string PhotoUrl { get; set; }

        public ImageSource PhotoSource()
        {

            ImageSource i = new Uri(PhotoUrl);
            return i;
            
        }
        public string Fullname
        {
            get { return Surname + " " + Lastname; }
        }
    }
}
