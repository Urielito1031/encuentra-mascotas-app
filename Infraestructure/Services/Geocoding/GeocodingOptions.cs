namespace Infraestructure.Services.Geocoding
{
   public sealed class GeocodingOptions
   {
      public const string SectionName = "Geocoding";
      public string BaseUrl { get; init; }  = default!;
      public string UserAgent { get; init; } = default!;
   }
}
