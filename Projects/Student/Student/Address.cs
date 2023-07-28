using System.Text.RegularExpressions;

namespace Student
{
    public class Address
    {
        private string street;
        private string city;
        private string state;
        private string postalCode;

        public Address(string street, string city, string state, string postalCode)
        {
            this.street = street;
            this.city = city;
            this.state = state;
            this.postalCode = postalCode;
        }

        public string Street
        {
            get { return street; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new InvalidStreetException("Street cannot be null or empty.");
                street = value;
            }
        }

        public string City
        {
            get { return city; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new InvalidCityException("City cannot be null or empty.");
                city = value;
            }
        }

        public string State
        {
            get { return state; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new InvalidStateException("State cannot be null or empty.");
                state = value;
            }
        }

        public string PostalCode
        {
            get { return postalCode; }
            set
            {
                if (!Regex.IsMatch(value, @"^\d{5}$"))
                    throw new InvalidPostalCodeException("Postal code must be a 5-digit number.");
                postalCode = value;
            }
        }

        public override string ToString()
        {
            return $"{street}, {city}, {state}, {postalCode}";
        }
    }
}
