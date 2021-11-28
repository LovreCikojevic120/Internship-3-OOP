using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                        ManageError();
                        break;
                }
            } while (menu is not "7");
        }

        static void ManageError()
        {
            Console.WriteLine("Opcija za izbornik krivo unesena, pritisnite bilo koju tipku da pokusate ponovno!\n");
            Console.ReadKey();
            Console.Clear();
        }

        static bool IsEmpty(Dictionary<Contact, List<Call>> phoneBook)
        {
            return phoneBook.Count is 0 ? true : false;
        }

        static bool ValidPhoneNumber(string phoneNumber)
        {
            return long.TryParse(phoneNumber, out long result) && phoneNumber.Length is 10 ? true : false;
        }

        static bool ValidNameSurname(string nameSurname)
        {
            return (!nameSurname.Any(char.IsDigit) && nameSurname.Length is not 0) ? true : false;
        }

        static void ConfirmContinue(string message)
        {
            Console.WriteLine($"\n{message}, za povratak na izbornik pritisnite bilo koju tipku!");
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

            ConfirmContinue("Kontakt ne postoji");
            return null;
        }

        static void PrintContacts(Dictionary<Contact, List<Call>> phoneBook)
        {
            if (IsEmpty(phoneBook))
            {
                ConfirmContinue("Kontakti prazni");
                return;
            }
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
            if (IsEmpty(phoneBook))
            {
                ConfirmContinue("Kontakti prazni");
                return;
            }

            Console.WriteLine("Upisite broj mobitela kontakta kojeg zelite izbrisati:\n");
            var phoneNumber = Console.ReadLine();
            if (!ValidPhoneNumber(phoneNumber))
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
            ConfirmContinue("Kontakt ne postoji");
        }

        static void EditContactPreference(Dictionary<Contact, List<Call>> phoneBook)
        {
            if (IsEmpty(phoneBook))
            {
                ConfirmContinue("Kontakti prazni");
                return;
            }

            Console.WriteLine("Upisite broj mobitela kontakta kojemu zelite promijeniti preferencu:\n");
            var phoneNumber = Console.ReadLine();
            if (!ValidPhoneNumber(phoneNumber))
            {
                ConfirmContinue("Broj mobitela krivo upisan");
                return;
            }

            var key = FindKeyWithNumber(phoneBook, phoneNumber);

            if (key is null) return;
            
            Console.WriteLine("\nUnesite novu preferencu:\n" +
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
                return;
            }

            ConfirmContinue("Krivi unos izbornika");
            return;
        }

        static void ManageContacts(Dictionary<Contact, List<Call>> phoneBook)
        {
            string menu = null;
            Contact contact = null;

            if (IsEmpty(phoneBook))
            {
                ConfirmContinue("Kontakti prazni");
                return;
            }

            do
            {
                Console.WriteLine("===Upravljanje kontaktima===\n" +
                    "1 - Ispis poziva\n" +
                    "2 - Uspostava poziva\n" +
                    "3 - Povratak na glavni izbornik\n\n");
                menu = Console.ReadLine();

                switch (menu)
                {
                    case "1":
                        Console.Clear();
                        contact = EnterUser(phoneBook);
                        if (contact is null) break;
                        PrintCallsByContact(phoneBook, contact);
                        break;
                    case "2":
                        Console.Clear();
                        contact = EnterUser(phoneBook);
                        if (contact is null) break;
                        MakeCall(phoneBook, contact);
                        break;
                    case "3":
                        Console.Clear();
                        return;
                    default:
                        ManageError();
                        break;
                }

            } while (menu is not "3");
        }

        static Contact EnterUser(Dictionary<Contact, List<Call>> phoneBook)
        {
            Console.WriteLine("Upisite broj kontakta s kojem zelite pristupiti:");
            var phoneNumber = Console.ReadLine();

            var contact = FindKeyWithNumber(phoneBook, phoneNumber);

            if (ValidPhoneNumber(phoneNumber) && phoneBook.ContainsKey(contact))
            {
                Console.WriteLine($"Ime i prezime: {contact.nameSurname}\n\n");
                return contact;
            }

            ConfirmContinue("Broj nije validan");
            return null;
        }

        static void MakeCall(Dictionary<Contact, List<Call>> phoneBook, Contact contact)
        {
            var callList = phoneBook[contact];

            if(contact.preference is Contact.Preference.Blocked)
            {
                ConfirmContinue("Kontakt blokiran");
                return;
            }

            if (callList.Any(call => call.status is Call.CallStatus.InProgress))
            {
                ConfirmContinue("Drugi poziv u tijeku");
                return;
            }

            Call currentCall = new Call();
            if (currentCall.status is Call.CallStatus.InProgress)
            {
                Random rndValue = new Random(DateTime.Now.Millisecond);
                var callDuration = rndValue.Next(1, 20);
                currentCall.callDuration = callDuration;

                Console.WriteLine($"Poziv u tijeku, traje {callDuration}s");
                Task.Delay(callDuration * 1000).Wait();

                ConfirmContinue("Poziv zavrsio");
                currentCall.isInProgress = false;
                currentCall.status = Call.CallStatus.Ended;

                callList.Add(currentCall);
                return;
            }

            currentCall.status = Call.CallStatus.Missed;

            callList.Add(currentCall);
            ConfirmContinue("Poziv je propusten");
            return;
        }

        static void PrintCallsByContact(Dictionary<Contact, List<Call>> phoneBook, Contact contact)
        {
            if(phoneBook[contact].Count is 0)
            {
                ConfirmContinue("Nema poziva");
                return;
            }

            foreach (var call in phoneBook[contact])
            {
                Console.WriteLine($"Vrijeme poziva: {call.timeOfCall}\n" +
                    $"Status poziva: {call.status}\n" +
                    $"Trajanje poziva: {call.callDuration}s\n");
            }

            ConfirmContinue("Pozivi ispisani");
        }
        
        static void PrintCalls(Dictionary<Contact, List<Call>> phoneBook)
        {
            if (IsEmpty(phoneBook))
            {
                ConfirmContinue("Imenik prazan");
                return;
            }

            foreach (var contact in phoneBook)
            {

                Console.WriteLine($"\nKontakt: {contact.Key.nameSurname}, Broj mobitela: {contact.Key.phoneNumber}");
                if (contact.Value.Count is 0) Console.WriteLine("Kontakt nema poziva\n");

                else
                {
                    foreach (var call in contact.Value)
                    {
                        Console.WriteLine($"Vrijeme poziva: {call.timeOfCall}\n" +
                            $"Status poziva: {call.status}\n" +
                            $"Trajanje poziva: {call.callDuration}s");
                    }
                }
            }

            ConfirmContinue("Pozivi ispisani");
        }
    }
}
