using System;
using System.Collections.Generic;
using System.Linq;
using PhoneBookApp.Entities;

namespace PhoneBookApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string menu = null;
            var phoneBook = new Dictionary<Contact, List<Call>>();
            phoneBook.Add(new Contact("ivan bakotin", "1234567890"), new List<Call>());

            do
            {
                Console.WriteLine("==MENU==\n" +
                    "1 - Ispis svih kontakata\n" +
                    "2 - Dodavanje novih kontakata u Imenik\n" +
                    "3 - Brisanje kontakata iz Imenika\n" +
                    "4 - Uredivanje preference kontakta\n" +
                    "5 - Upravljanje kontaktima\n" +
                    "6 - Ispis svih poziva\n" +
                    "7 - Izlaz iz aplikacije\n");
                menu = Console.ReadLine();

                switch (menu)
                {
                    case "1":
                        Console.Clear();
                        PrintContacts(phoneBook);
                        break;
                    case "2":
                        Console.Clear();
                        AddContact(phoneBook);
                        break;
                    case "3":
                        Console.Clear();
                        DeleteContact(phoneBook);
                        break;
                    case "4":
                        Console.Clear();
                        EditContactPreference(phoneBook);
                        break;
                    case "5":
                        Console.Clear();
                        ManageContacts(phoneBook);
                        break;
                    case "6":
                        Console.Clear();
                        PrintCalls(phoneBook);
                        break;
                    case "7":
                        Console.Clear();
                        Console.WriteLine("Dovidenja!");
                        break;
                    default:
                        //ManageError();
                        break;
                }
            } while (menu is not "7");
        }

        static bool ValidPhoneNumber(string phoneNumber)
        {
            return long.TryParse(phoneNumber, out long result) && phoneNumber.Length is 10 ? true : false;
        }

        static bool ValidNameSurname(string nameSurname)
        {
            return nameSurname.Any(char.IsDigit) ? false : true;
        }

        static void ConfirmContinue(string message)
        {
            Console.WriteLine($"\n{message}, za povratak na glavni izbornik pritisnite bilo koju tipku!");
            Console.ReadKey();
            Console.Clear();
        }

        static Contact FindKeyWithNumber(Dictionary<Contact, List<Call>> phoneBook, string phoneNumber)
        {
            foreach(var contact in phoneBook)
            {
                if (contact.Key.phoneNumber == phoneNumber)
                {
                    return contact.Key;
                }
            }

            Console.WriteLine("\nKontakt ne postoji!\n");
            return null;
        }

        static void PrintContacts(Dictionary<Contact, List<Call>> phoneBook)
        {
            Console.WriteLine("Ispis kontakata:\n");
            foreach(var contact in phoneBook)
            {
                Console.WriteLine($"Ime i prezime: {contact.Key.nameSurname}\n" +
                    $"Broj mobitela: {contact.Key.phoneNumber}\n" +
                    $"Preferenca: {contact.Key.preference}\n\n");
            }
            ConfirmContinue("Kontakti ispisani");
            return;
        }

        static void AddContact(Dictionary<Contact, List<Call>> phoneBook)
        {
            string menu = null;

            do
            {
                Console.WriteLine("Upisite ime i prezime novog kontakta:\n");
                var nameSurname = Console.ReadLine();
                if (!ValidNameSurname(nameSurname))
                {
                    ConfirmContinue("Ime i prezime krivo uneseno");
                    return;
                }

                Console.WriteLine("\nUpisite broj mobitela novog kontakta:\n");
                var phoneNum = Console.ReadLine();
                if (!ValidPhoneNumber(phoneNum))
                {
                    ConfirmContinue("Broj mobitela krivo upisan, broj mora imati 10 znamenki");
                    return;
                }


                foreach (var contact in phoneBook)
                {
                    if (contact.Key.phoneNumber == phoneNum)
                    {
                        Console.WriteLine("\nBroj vec postoji u imeniku!\n" +
                            "1 - Pokusajte ponovno\n" +
                            "0 - Nazad za glavni izbornik\n");
                        menu = Console.ReadLine();
                    }
                }

                switch (menu)
                {
                    case "1":
                        Console.Clear();
                        break;
                    case "0":
                        Console.Clear();
                        return;
                    default:
                        Contact contact = new Contact(nameSurname, phoneNum);
                        phoneBook.Add(contact, new List<Call>());
                        ConfirmContinue("Kontakt uspjesno dodan");
                        menu = "0";
                        break;
                }

            } while (menu is not "0");
        }

        static void DeleteContact(Dictionary<Contact, List<Call>> phoneBook)
        {
            Console.WriteLine("Upisite broj mobitela kontakta kojeg zelite izbrisati:\n");
            var phoneNumber = Console.ReadLine();
            if (ValidPhoneNumber(phoneNumber))
            {
                ConfirmContinue("Broj mobitela krivo upisan");
                return;
            }

            foreach(var contact in phoneBook)
            {
                if (contact.Key.phoneNumber == phoneNumber)
                {
                    phoneBook.Remove(contact.Key);
                    ConfirmContinue("Kontakt izbrisan");
                    return;
                }
            }
            ConfirmContinue("Kontakt ne postoji!");
        }

        static void EditContactPreference(Dictionary<Contact, List<Call>> phoneBook)
        {
            Console.WriteLine("Upisite broj mobitela kontakta kojemu zelite promijeniti preferencu:\n");
            var phoneNumber = Console.ReadLine();
            if (!ValidPhoneNumber(phoneNumber)) return;

            var key = FindKeyWithNumber(phoneBook, phoneNumber);

            if (key is null) return;
            else
            {
                Console.WriteLine("Unesite novu preferencu:\n" +
                    "1 - Normal\n2 - Favorit\n3 - Blokiran\n");
                var pref = Console.ReadLine();

                if(int.TryParse(pref, out int result) && int.Parse(pref) > 0 && int.Parse(pref) < 4)
                {
                    var oldContact = key;
                    var listOfCalls = phoneBook[oldContact];
                    phoneBook.Remove(oldContact);

                    var newContact = new Contact(key.nameSurname, phoneNumber, result);
                    phoneBook.Add(newContact, listOfCalls);
                    ConfirmContinue("Preferenca kontakta uspjesno promijenjena");
                }
                else
                {
                    ConfirmContinue("Krivi unos izbornika");
                    return;
                }
            }
            
        }

        static void ManageContacts(Dictionary<Contact, List<Call>> phoneBook)
        {

        }

        static void PrintCalls(Dictionary<Contact, List<Call>> phoneBook)
        {
            foreach (var contact in phoneBook)
            {

                Console.WriteLine($"\nKontakt: {contact.Key.nameSurname}, Broj mobitela: {contact.Key.phoneNumber}");

                foreach (var call in contact.Value)
                {
                    Console.WriteLine($"Vrijeme poziva: {call.timeOfCall}\n" +
                        $"Status poziva: {call.status}\n");
                }
            }
            ConfirmContinue("Pozivi ispisani");
        }
    }
}
