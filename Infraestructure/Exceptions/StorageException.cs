using System;

namespace Infraestructure.Exceptions
{
   public sealed class StorageException : InfraestructureException
   {
      public StorageException(string message) : base(message) { }
   }
}