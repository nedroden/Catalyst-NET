using System;

namespace Catalyst.Api.Main.Exceptions
{
    [Serializable]
    public class RecordExistsException : Exception
    {
        public RecordExistsException() { }

        public RecordExistsException(string message) : base(message) { }

        public RecordExistsException(string message, Exception innerException) : base(message, innerException) { }
    }
}