using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApp.Entities
{
    class Contact
    {
        private string _nameSurname;
        private string _phoneNumber;
        private Preference _preference;

        public string nameSurname { get => _nameSurname; set => _nameSurname = value; }
        public string phoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public Preference preference { get => _preference; set => _preference = value; }

        public enum Preference
        {
            Normal = 1,
            Favorit = 2,
            Blocked = 3
        };

        public Contact(string nameSurname, string phoneNumber)
        {
            _nameSurname = nameSurname;
            _phoneNumber = phoneNumber;
            _preference = Preference.Normal;
        }

        public Contact(string nameSurname, string phoneNumber, int pref)
        {
            _nameSurname = nameSurname;
            _phoneNumber = phoneNumber;

            switch (pref)
            {
                case (int)Preference.Normal:
                    _preference = Preference.Normal;
                    break;
                case (int)Preference.Favorit:
                    _preference = Preference.Favorit;
                    break;
                case (int)Preference.Blocked:
                    _preference = Preference.Blocked;
                    break;
                default:
                    Console.WriteLine("Preferenca krivo odabrana!\n");
                    return;
            }
        }
    }
}
