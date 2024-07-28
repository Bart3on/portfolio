using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Contact.Models
{

    class Contact
    {
        private static uint _totalAmount { get; set; }
        private static uint _totalExistingAmount { get; set; }
        public static uint TotalExistingAmount => _totalExistingAmount;
        public uint Id { get; } = 0;
        public string PhoneNumber { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public Contact(string firstName, string lastName, string phoneNumber)
        {
            _totalExistingAmount++;
            _totalAmount++;
            Id = _totalAmount;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
        }

        public enum EditType { All, FirstNameOnly, LastNameOnly, PhoneNumberOnly }

        private EditType GetEditType(string? firstName, string? lastName, string? phoneNumber)
        {
            if (firstName != null && lastName != null && phoneNumber != null)
                return EditType.All;
            else if (firstName != null)
                return EditType.FirstNameOnly;
            else if (lastName != null)
                return EditType.LastNameOnly;
            else if (phoneNumber != null)
                return EditType.PhoneNumberOnly;
            else
                throw new ArgumentException("At Least One Parameter Have Be Provided For Editing.");
        }
        public override string ToString()
        {
            return $"ID:{Id} Name: {FirstName + " " + LastName}, Phone Number: {PhoneNumber}";
        }

        public void Edit(string? firstName = null, string? lastName = null, string? phoneNumber = null)
        {
            switch (GetEditType(firstName, lastName, phoneNumber))
            {
                case EditType.All:
                    FirstName = firstName!;
                    LastName = lastName!;
                    PhoneNumber = phoneNumber!;
                    break;
                case EditType.FirstNameOnly:
                    FirstName = firstName!;
                    break;
                case EditType.LastNameOnly:
                    LastName = lastName!;
                    break;
                case EditType.PhoneNumberOnly:
                    PhoneNumber = phoneNumber!;
                    break;
            }
        }

        ~Contact()
        {
            _totalExistingAmount--;
        }

    }
}
