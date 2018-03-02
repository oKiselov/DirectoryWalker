using System;

namespace DirectoryWalker.Infrastructure.Exceptions
{
    internal class AutofacNotRegisteredException : Exception
    {
        public AutofacNotRegisteredException()
        {
        }

        public AutofacNotRegisteredException(string message)
            : base(message)
        {
        }

        public AutofacNotRegisteredException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
