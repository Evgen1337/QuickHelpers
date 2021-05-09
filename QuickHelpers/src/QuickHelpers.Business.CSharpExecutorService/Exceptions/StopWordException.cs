using System;
using System.Runtime.Serialization;

namespace QuickHelpers.Business.CSharpExecutorService.Exceptions
{
    [Serializable]
    public class StopWordException : Exception
    {
        public StopWordException()
        {
        }

        public StopWordException(string message) : base(message)
        {
        }

        public StopWordException(string message, Exception inner) : base(message, inner)
        {
        }

        protected StopWordException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}