using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactModel = PhoneBook.Contact.Models;


namespace PhoneBook
{
    class PhoneBook
    {
        public List<ContactModel.Contact> Contacts { get; }
        private ContactModel.ContactGenerator _contactGenerator;

        public PhoneBook()
        {
            this.Contacts = new List<ContactModel.Contact>();
            this._contactGenerator = new ContactModel.ContactGenerator();
        }

        public PhoneBook(List<ContactModel.Contact> contacts)
        {
            this.Contacts = contacts;
        }

        public void AddContact(ContactModel.Contact contact) { 
            this.Contacts.Add(contact);
        }

        public void EditContact(ContactModel.Contact contact)
        {
            int index = Contacts.FindIndex(c => c.Id == contact.Id);

            if (index == -1)
            {
                Console.WriteLine("Contact Not Found");
                return;
            }

            Contacts[index] = contact;
            Console.WriteLine("Contact Has Been Updated");
        }

        public void RemoveContact(ContactModel.Contact contact)
        {
            this.Contacts.Remove(contact);
        }

        public void RemoveContactById(uint id)
        {
            ContactModel.Contact? foundContact = this.GetContactById(id);
            if (foundContact == null) {
                Console.WriteLine($"Contact With Id: {id} Has Been Not Found");
                return;
            }
            this.Contacts.Remove(foundContact);
            Console.WriteLine("Contact Has Been Removed");

        }

        public void GenerateContacts(uint amount, string nationality)
        {

            for(uint i = 0; i < amount; i++ )
            {
                ContactModel.Contact generetedContact = this._contactGenerator.GenereateContact(nationality);
                if (generetedContact == null) { Console.WriteLine($"Contact could not be genereted"); continue; }

                this.AddContact(generetedContact);

            }
            Console.WriteLine($"{amount} Contacts Have Been Generated");
        }

        public ContactModel.Contact? GetContactById(uint id)
        {
            int index = this.Contacts.FindIndex(c => c.Id == id);

            if (index != -1)
            {
                return this.Contacts[index];
            }
           
            return null;
            
        }

        public ContactModel.Contact? GetContactByPhoneNumber(string number)
        {
            return this.Contacts.FirstOrDefault<ContactModel.Contact>(c => c.PhoneNumber == number);
        }

        public ContactModel.Contact? GetContactByName(string name)
        {
            List<string> names =  new List<string>(name.Split(" "));

            if (names.Count != 2) throw new ArgumentException("Invalid Name Input");

            return Contacts.FirstOrDefault(c =>
                (c.FirstName == names[0] && c.LastName == names[1]) ||
                (c.FirstName == names[1] && c.LastName == names[0])); ;
        }

    }
}
