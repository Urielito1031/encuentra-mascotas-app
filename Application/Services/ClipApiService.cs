using Domain.Interfaces.Services;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Application.Services
{
   public class ClipApiService : IClipApiService
   {
      private readonly InferenceSession _session;

   
      public ClipApiService(string modelPath)
      {
         _session = new InferenceSession(modelPath);

      }
      public float[] ObtenerImagenEnbedding(Stream imagenStream)
      {
         //convierte imagen a matriz de píxeles
         using var imagen = Image.Load<Rgb24>(imagenStream);

         imagen.Mutate(img => img.Resize(new ResizeOptions 
         { 
            Size = new Size(224, 224),
            Mode = ResizeMode.Stretch
         }));

         //crear el Tensor(el formato que entiende ONNX)
         //Dimensiones = [BatchSize=1, Canales=3, Alto=224, Ancho=224]
         var tensor = new DenseTensor<float>([1, 3, 224, 224]);

         imagen.ProcessPixelRows(accessor =>
         {
            for (int y = 0; y < accessor.Height; y++)
            {
               Span<Rgb24> pixelRow = accessor.GetRowSpan(y);
               for (int x = 0; x < accessor.Width; x++)
               {
                  var pixel = pixelRow[x];

                  //LA FÓRMULA DE OPENAI: (Val / 255 - Mean) / Std
                  // Tensor[Batch, Canal, Y, X]
                  // Canal 0: ROJO (R)
                  tensor[0, 0, y, x] = (pixel.R / 255f - 0.48145466f) / 0.26862954f;

                  // Canal 1: VERDE (G)
                  tensor[0, 1, y, x] = (pixel.G / 255f - 0.4578275f) / 0.26130258f;

                  // Canal 2: AZUL (B)
                  tensor[0, 2, y, x] = (pixel.B / 255f - 0.40821073f) / 0.27577711f;
               }
            }
         });
         var inputs = new List<NamedOnnxValue>
         {
            NamedOnnxValue.CreateFromTensor("pixel_values",tensor)
         };
         using var resultados = _session.Run(inputs);
         var outputVector = resultados.First().AsEnumerable<float>().ToArray();
         return outputVector;
      }

   }
}
