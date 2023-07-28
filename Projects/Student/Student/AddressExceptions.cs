namespace Student
{
    public class InvalidStreetException : Exception
    {
        public InvalidStreetException(string message) : base(message) { }
    }

    public class InvalidCityException : Exception
    {
        public InvalidCityException(string message) : base(message) { }
    }

    public class InvalidStateException : Exception
    {
        public InvalidStateException(string message) : base(message) { }
    }

    public class InvalidPostalCodeException : Exception
    {
        public InvalidPostalCodeException(string message) : base(message) { }
    }
}
