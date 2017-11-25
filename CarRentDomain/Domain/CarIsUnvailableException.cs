using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using CarRent.Common;

namespace CarRent.Domain
{
    [Serializable]
    public class CarIsUnvailableException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public CarIsUnvailableException(int carId,DatePeriod datePeriod):base($"car by id {carId} unvailable from {datePeriod.From} to {datePeriod.To}")
        {
        }

        public CarIsUnvailableException(string message) : base(message)
        {
        }

        public CarIsUnvailableException(string message, Exception inner) : base(message, inner)
        {
        }

        protected CarIsUnvailableException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
