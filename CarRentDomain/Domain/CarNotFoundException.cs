using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CarRent.Domain
{
    [Serializable]
    public class CarNotFountException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public CarNotFountException(int carId):base($"car by id {carId} not found")
        {
        }

        public CarNotFountException(string message) : base(message)
        {
        }

        public CarNotFountException(string message, Exception inner) : base(message, inner)
        {
        }

        protected CarNotFountException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
    [Serializable]
    class CarNotFoundException
    {
    }
}
