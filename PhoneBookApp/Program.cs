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
            var lista = new List<Call>();
            lista.Add(new Call());
            phoneBook.Add(new Contact("ivan bakotin", "21412471"), lista);

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
                        printContacts(phoneBook);
                        break;
                    case "2":
                        Console.Clear();
                        addContact(phoneBook);
                        break;
                    case "3":
                        Console.Clear();
                        //deleteContact();
                        break;
                    case "4":
                        Console.Clear();
                        //editContactPreference();
                        break;
                    case "5":
                        Console.Clear();
                        //manageContatcs();
                        break;
                    case "6":
                        Console.Clear();
                        //printCalls();
                        break;
                    case "7":
                        Console.Clear();
                        Console.WriteLine("Dovidenja!");
                        break;
                    default:
                        //manageError();
                        break;
                }
            } while (menu is not "7");
        }

        static void confirmContinue()
        {
            Console.WriteLine("Za povratak na glavni izbornik pritisnite bilo koju tipku!");
            Console.ReadKey();
            Console.Clear();
        }

        static void printContacts(Dictionary<Contact, List<Call>> phoneBook)
        {
            Console.WriteLine("Ispis kontakata:\n");
            foreach(var contact in phoneBook)
            {
                Console.WriteLine($"Ime i prezime: {contact.Key._nameSurname}\n" +
                    $"Broj mobitela: {contact.Key._phoneNumber}\n\n");
            }
            confirmContinue();
            return;
        }

        static void addContact(Dictionary<Contact, List<Call>> phoneBook)
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
                        confirmContinue();
                        menu = "0";
                        break;
                }

            } while (menu is not "0");
        }
    }
}
