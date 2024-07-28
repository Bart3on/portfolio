using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneBook.Contact.enums;

namespace PhoneBook.Contact.Models
{
    
    class ContactGenerator
    {
        private Dictionary<Country, List<string>> _firstNames = new Dictionary<Country, List<string>>
        {
            { Country.USA , new List<string> { "James", "John", "Robert", "Michael", "William" } },
            { Country.Germany, new List<string> { "Hans", "Peter", "Klaus", "Jürgen", "Wolfgang" } }
        };

        private Dictionary<Country, List<string>> _lastNames = new Dictionary<Country, List<string>>
        {
            { Country.USA, new List<string> { "Smith", "Johnson", "Williams", "Brown", "Jones" } },
            { Country.Germany, new List<string> { "Müller", "Kohl", "Schmidt", "Falke", "Scholz" } },
        };

        private Dictionary<Country, uint> _countriesPhoneNumberLength = new Dictionary<Country, uint>
        {
            { Country.USA, 10 },
            { Country.Germany, 11 },
        };

        private Dictionary<Country, string> _phoneNumberPrefix = new Dictionary<Country, string> { { Country.Germany, "+49" }, { Country.USA, "+1" } };

        private Dictionary<string, Country> _countryCodeMapping = new Dictionary<string, Country> { { "de", Country.Germany }, { "us", Country.USA } };

        private Country? GetCountryFromCode(string countryCodeIso3166Alpha2)
        {
            if (_countryCodeMapping.TryGetValue(countryCodeIso3166Alpha2.ToLower(), out var country))
            {
                return country;
            }
            return null;
        }

        public Contact? GenereateContact(string countryCodeIso3166Alpha2)
        {
            try
            {
                Country? country = GetCountryFromCode(countryCodeIso3166Alpha2);
                if (country == null)
                {
                    throw new Exception("Invalid Country Code. You Should Provide A Country Code In ISO 3166 ALPHA-2 (e.g., 'de' For Germany).");
                    return null;
                }

                Random rnd = new Random();
                List<string>? firstNameList = _firstNames[country.Value];
                List<string>? lastNameList = _lastNames[country.Value];
                string? phonePrefix = _phoneNumberPrefix[country.Value];
                uint? countryPhoneNumberLength = _countriesPhoneNumberLength[country.Value];


                if (
                    firstNameList == null ||
                    lastNameList == null ||
                    phonePrefix == null ||
                    countryPhoneNumberLength == null
                )
                {
                    throw new Exception($"Missing Data is missing for country code {country}: Check The Stored Data."); ;
                }

                string randomFirstName = firstNameList[rnd.Next(0, firstNameList.Count - 1)];
                string randomLastName = lastNameList[rnd.Next(0, lastNameList.Count - 1)];
                string? randomPhoneNumberForCountry = null;

                switch (country.Value)
                {
                    case Country.Germany:
                        for (int i = 0; i < phonePrefix.Length; i++)
                        {
                            randomPhoneNumberForCountry += rnd.Next(0, 9).ToString();
                        }
                        break;
                    case Country.USA:
                        for (int i = 0; i < phonePrefix.Length; i++)
                        {
                            randomPhoneNumberForCountry += rnd.Next(0, 9).ToString();
                        }
                        break;
                }
                string randomPhoneNumber = phonePrefix + randomPhoneNumberForCountry;

                return new Contact(randomFirstName, randomLastName, randomPhoneNumber);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }

        }

    }
}
