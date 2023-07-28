namespace Student
{
    public class InvalidSurnameException : Exception
    {
        public InvalidSurnameException(string message) : base(message) { }
    }

    public class InvalidNameException : Exception
    {
        public InvalidNameException(string message) : base(message) { }
    }

    public class InvalidPatronymicException : Exception
    {
        public InvalidPatronymicException(string message) : base(message) { }
    }

    public class InvalidBirthDateException : Exception
    {
        public InvalidBirthDateException(string message) : base(message) { }
    }

    public class InvalidAddressException : Exception
    {
        public InvalidAddressException(string message) : base(message) { }
    }

    public class InvalidPhoneNumberException : Exception
    {
        public InvalidPhoneNumberException(string message) : base(message) { }
    }

    public class InvalidGradeException : Exception
    {
        public InvalidGradeException(string message) : base(message) { }
    }
}
