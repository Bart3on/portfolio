using System.Runtime.CompilerServices;
using ContactModels = PhoneBook.Contact.Models;

namespace PhoneBook
{
    class Program
    {
        static void DisplayMenu()
        {
            Console.WriteLine("Choose An Action:");
            Console.WriteLine("1. Show All Contacts");
            Console.WriteLine("2. Find Contact By Name");
            Console.WriteLine("3. Find Contact By Phonenumber");
            Console.WriteLine("4. Edit Contact");
            Console.WriteLine("5. Remove Contact");
            Console.WriteLine("6. Generate Contacts");
            Console.WriteLine("7. Close The Application");
            Console.WriteLine("Input A Number Between 1 and 7: ");
        }

        static void RenderSeperator()
        {
            for (int i = 0; i < 50; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }

        static bool ConfirmActionInput(string input, string confirmationMsg, string cancelMsg) {
            if (input == "n")
            {
                Console.WriteLine(cancelMsg);
                RenderSeperator();
                return false;
            }
            if (input != "y")
            {
                Console.WriteLine("Invalid Input");
                Console.WriteLine(cancelMsg);
                RenderSeperator();
                return false;
            }
            Console.WriteLine(confirmationMsg);
            RenderSeperator();
            return true;
        }

        static void Main(string[] args)
        {

            PhoneBook phoneBook = new PhoneBook();
            phoneBook.AddContact(new ContactModels.Contact("Joe", "Doe", "+4917656780956"));
            phoneBook.AddContact(new ContactModels.Contact("Harry", "Potter", "+4917656780954"));
            phoneBook.AddContact(new ContactModels.Contact("Justin", "Timberlake", "+4917656780932"));
            Boolean isRunning = true;

            Console.WriteLine("Welcome To Phone Book!");

            while (isRunning)
            {
                DisplayMenu();

                byte? userInputParsed = null;

                while (userInputParsed == null)
                {
                    try
                    {
                        
                        string? userInput = Console.ReadLine();
                        userInputParsed = byte.Parse(userInput);
                    }
                    catch
                    {    
                        throw new Exception("Invalid Input: You can input only numbers in range 1-7");
                    }

                }

                ContactModels.Contact? foundContact = null;


                switch (userInputParsed)
                {
                    case 1:
                        RenderSeperator();
                        Console.WriteLine($"Found {ContactModels.Contact.TotalExistingAmount} Contacts:");
                        foreach (ContactModels.Contact contact in phoneBook.Contacts)
                        {
                            Console.WriteLine(contact.ToString());
                        }
                        RenderSeperator();
                        break;
                    case 2:
                        RenderSeperator();

                        Console.WriteLine($"Enter Contact Name (Joe Doe) Or (Doe Joe): ");
                        foundContact = phoneBook.GetContactByName(Console.ReadLine());
                        if (foundContact == null)
                        {
                            Console.WriteLine("Contact Not Found");
                        }
                        else
                        {
                            Console.WriteLine($"Contact Found: {foundContact.ToString()}");
                        }
                        RenderSeperator();
                        break;
                    case 3:
                        RenderSeperator();
                        Console.WriteLine($"Enter Contact PhoneNumber (+4917656780956): ");
                        try
                        {
                            foundContact = phoneBook.GetContactByPhoneNumber(Console.ReadLine());
                        }
                        catch 
                        {
                            Console.WriteLine($"Invalid Input");
                        }
                        if (foundContact == null)
                        {
                            Console.WriteLine("Contact Not Found");

                        }
                        else
                        {
                            Console.WriteLine($"Contact Found: {foundContact.ToString()}");
                        }
                        RenderSeperator();
                        break;
                    case 4:
                        RenderSeperator();

                        Console.WriteLine($"Enter Contact Id, Name or Number Which Should Be Updated: ");
                        string? contactToEditInput = Console.ReadLine();
                        if(contactToEditInput == null)
                        {
                            Console.WriteLine("Contact Not Found");
                            break;
                        }
                        if (string.IsNullOrEmpty(contactToEditInput))
                        {
                            Console.WriteLine("Contact Not Found");
                            break;
                        }

                        if (contactToEditInput.StartsWith("+"))
                        {
                            foundContact = phoneBook.GetContactByPhoneNumber(contactToEditInput);
                        }

                        if (uint.TryParse(contactToEditInput, out uint number))
                        {
                            foundContact = phoneBook.GetContactById(uint.Parse(contactToEditInput));
                        }
                        if(foundContact == null)
                        {
                            foundContact = phoneBook.GetContactByName(contactToEditInput);
                        }

                        Console.WriteLine("Enter New Firstname");
                        string editedContactFirstName = Console.ReadLine();
                        Console.WriteLine("Enter New Lastname");
                        string editedContactLastName = Console.ReadLine();
                        Console.WriteLine("Enter New PhoneNumber");

                        string editedContactPhoneNumber = Console.ReadLine();
                        Console.WriteLine(
                            $"Are You Sure That You Want Edit: ${foundContact.ToString()}?\n" +
                            $"New Contact Data: {editedContactFirstName} {editedContactLastName} {editedContactPhoneNumber}\n" +
                            $"Enter (y/n): "
                            );
                        string userAnswer = Console.ReadLine();

                        bool confirmed = ConfirmActionInput(userAnswer, "Contact Has Been Updated", "Contact Update Has Been Canceled");

                        if (!confirmed) break;

                        foundContact.PhoneNumber = !string.IsNullOrEmpty(editedContactPhoneNumber) ? editedContactPhoneNumber : foundContact.PhoneNumber ;
                        foundContact.FirstName = !string.IsNullOrEmpty(editedContactFirstName) ? editedContactFirstName : foundContact.FirstName;
                        foundContact.LastName = !string.IsNullOrEmpty(editedContactLastName) ? editedContactLastName : foundContact.FirstName;

                        phoneBook.EditContact(foundContact);
                        RenderSeperator();
                        break;
                    case 5:
                        Console.Write("Enter User ID Which Should Be Removed: ");
                        string contactToRemoveId = Console.ReadLine();
                        if (uint.TryParse(contactToRemoveId, out uint parsedContactToRemoveId))
                        {
                            phoneBook.RemoveContactById(parsedContactToRemoveId);
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID. Please Enter A Valid Number.");
                        }
                        phoneBook.RemoveContactById(parsedContactToRemoveId);
                        break;
                    case 6:
                        Console.Write("How Many Contacts Do You Want To Create?: ");
                        string newContactsAmount = Console.ReadLine();
                        Console.Write("What Nationality Should The Contacts Be (e.g: us, de)?: ");
                        string newContactsNationality = Console.ReadLine();
                        try
                        {
                            uint parsedNewContactsAmount = uint.Parse(newContactsAmount);
                            phoneBook.GenerateContacts(parsedNewContactsAmount, newContactsNationality);
                            Console.Write($"{newContactsAmount} Have Been Generated");

                            break;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            break;
                        }
                    case 7:
                        Console.WriteLine("Closing...");
                        isRunning = false;
                        break;

                }
            }
           
        }
    }
}
