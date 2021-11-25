using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApp.Entities
{
    class Contact
    {
        public string _nameSurname;
        public string _phoneNumber;

        public enum preference
        {
            Favorit,
            Normal,
            Blocked
        };

        public Contact(string nameSurname, string phoneNumber)
        {
            _nameSurname = nameSurname;
            _phoneNumber = phoneNumber;
        }

    }
}
