using System;

namespace Infraestructure.Exceptions
{
   public abstract class InfraestructureException : Exception
   {
      protected InfraestructureException(string message) : base(message) { }
   }
}