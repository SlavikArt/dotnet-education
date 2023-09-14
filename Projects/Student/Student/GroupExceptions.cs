namespace Student
{
    public class InvalidGroupStudentsException : Exception
    {
        public InvalidGroupStudentsException(string message) : base(message) {}
    }

    public class GroupDuplicateStudentsException : Exception
    {
        public GroupDuplicateStudentsException(string message) : base(message) {}
    }

    public class GroupIndexOutOfRangeException : Exception
    {
        public GroupIndexOutOfRangeException(string message) : base(message) {}
    }

    public class InvalidGroupNameException : Exception
    {
        public InvalidGroupNameException(string message) : base(message) {}
    }

    public class InvalidGroupSpecializationException : Exception
    {
        public InvalidGroupSpecializationException(string message) : base(message) {}
    }

    public class InvalidGroupCourseNumberException : Exception
    {
        public InvalidGroupCourseNumberException(string message) : base(message) {}
    }

    public class InvalidGroupTransferStudentException : Exception
    {
        public InvalidGroupTransferStudentException(string message) : base(message) { }
    }

    public class InvalidGroupReplaceStudentException : Exception
    {
        public InvalidGroupReplaceStudentException(string message) : base(message) { }
    }
}
