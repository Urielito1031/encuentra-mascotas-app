using System;

namespace Infraestructure.Exceptions
{
   public sealed class EmbeddingException : InfraestructureException
   {
      public EmbeddingException(string message) : base(message) { }
   }
}