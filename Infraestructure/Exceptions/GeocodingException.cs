using System;

namespace Infraestructure.Exceptions
{
   public sealed class GeocodingException : InfraestructureException
   {
      public GeocodingException(string message) : base(message) { }
   }
}