using System;
using System.Collections.Generic;
using PhoneBookApp.Entities;

namespace PhoneBookApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string menu = null;
            var phoneBook = new Dictionary<Contact, List<Call>>();
            phoneBook.Add(new Contact("ivan bakotin", "12345"), new List<Call>());

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

        static void ConfirmContinue()
        {
            Console.WriteLine("Za povratak na glavni izbornik pritisnite bilo koju tipku!");
            Console.ReadKey();
            Console.Clear();
        }

        static void PrintContacts(Dictionary<Contact, List<Call>> phoneBook)
        {
            Console.WriteLine("Ispis kontakata:\n");
            foreach(var contact in phoneBook)
            {
                Console.WriteLine($"Ime i prezime: {contact.Key._nameSurname}\n" +
                    $"Broj mobitela: {contact.Key._phoneNumber}\n\n");
            }
            ConfirmContinue();
            return;
        }

        static void AddContact(Dictionary<Contact, List<Call>> phoneBook)
        {
            string menu = null;

            do
            {
                Console.WriteLine("Upisite ime i prezime novog kontakta:\n");
                var nameSurname = Console.ReadLine();
                Console.WriteLine("Upisite broj mobitela novog kontakta:\n");
                var phoneNum = Console.ReadLine();

                foreach (var contact in phoneBook)
                {
                    if (contact.Key._phoneNumber == phoneNum)
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
                        Console.WriteLine("\nKontakt uspjesno dodan!\n");
                        ConfirmContinue();
                        menu = "0";
                        break;
                }

            } while (menu is not "0");
        }

        static void DeleteContact(Dictionary<Contact, List<Call>> phoneBook)
        {
            Console.WriteLine("Upisite broj mobitela kontakta kojeg zelite izbrisati:\n");
            var phoneNumber = Console.ReadLine();
            foreach(var contact in phoneBook)
            {
                if (contact.Key._phoneNumber == phoneNumber)
                {
                    phoneBook.Remove(contact.Key);
                    Console.WriteLine("Kontakt izbrisan!\n");
                    ConfirmContinue();
                    return;
                }
            }
            Console.WriteLine("Kontakt ne postoji!\n");
            ConfirmContinue();
        }

        static void EditContactPreference(Dictionary<Contact, List<Call>> phoneBook)
        {

        }

        static void ManageContacts(Dictionary<Contact, List<Call>> phoneBook)
        {

        }

        static void PrintCalls(Dictionary<Contact, List<Call>> phoneBook)
        {

        }
    }
}
