using Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Application.Services
{
   public class ClipVectorizacionService : IClipApiService, IDisposable
   {
      private readonly InferenceSession _session;
      private bool _disposed = false;

      //constantes de normalizacion CLIP
      private const float MEAN_R = 0.48145466f;
      private const float MEAN_G = 0.4578275f;
      private const float MEAN_B = 0.40821073f;
      private const float STD_R = 0.26862954f;
      private const float STD_G = 0.26130258f;
      private const float STD_B = 0.27577711f;


      public ClipVectorizacionService(string modelPath)
      {
         _session = new InferenceSession(modelPath);

      }

      public float[] ObtenerImagenEmbedding(Stream imagenStream)
      {
         //convierte imagen a matriz de píxeles
         using var imagen = Image.Load<Rgb24>(imagenStream);

         imagen.Mutate(img => img.Resize(new ResizeOptions 
         { 
            Size = new Size(224, 224),
            Mode = ResizeMode.Stretch
         }));

         var tensor = CrearTensorNormalizado(imagen);

       
         var inputs = new List<NamedOnnxValue>
         {
            NamedOnnxValue.CreateFromTensor("pixel_values",tensor)
         };
         using var resultados = _session.Run(inputs);
         var outputVector = resultados.First().AsEnumerable<float>().ToArray();
         return outputVector;
      }

      private Tensor<float> CrearTensorNormalizado(Image<Rgb24> imagen)
      {
         var tensor = new DenseTensor<float>([1, 3, 224, 224]);

         imagen.ProcessPixelRows(accessor =>
         {
            for (int y = 0; y < accessor.Height; y++)
            {
               Span<Rgb24> pixelRow = accessor.GetRowSpan(y);

               for (int x = 0; x < accessor.Width; x++)
               {
                  var pixel = pixelRow[x];
                  // Normalización CLIP: (Val / 255 - Mean) / Std
                  tensor[0, 0, y, x] = (pixel.R / 255f - MEAN_R) / STD_R; // Canal R
                  tensor[0, 1, y, x] = (pixel.G / 255f - MEAN_G) / STD_G; // Canal G
                  tensor[0, 2, y, x] = (pixel.B / 255f - MEAN_B) / STD_B; // Canal B
               }

            }
         });
         return tensor;

      }

      public void Dispose()
      {
         if(!_disposed)
         {
            _session?.Dispose();
            _disposed = true;
            Console.WriteLine("Sesión ONNX liberada");
         }
      }

   }
}
