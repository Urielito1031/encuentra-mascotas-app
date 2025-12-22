namespace Infraestructure.Storage
{
   public sealed class AzureBlobOptions
   {
      public const string SectionName = "AzureBlobStorage";
      public string ConnectionString { get; init; } = null!;

   }
  
}
