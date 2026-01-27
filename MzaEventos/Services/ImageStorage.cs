using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace MzaEventos.Services
{
    public class ImageStorage
    {
        private readonly IWebHostEnvironment _env;
        private static readonly HashSet<string> _alowwed = new(StringComparer.OrdinalIgnoreCase)
        {
            "image/png",
            "image/jpeg",
            "image/webp"
        };

        public ImageStorage(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> SaveAsync(string userId, IFormFile file, CancellationToken ct = default)
        {
            if (file is null || file.Length == 0)
                throw new InvalidOperationException("Archivo vacío.");

            if (file.Length > 2 * 1024 * 1024)
                throw new InvalidOperationException("Supera el limite de 2MB");

            //1) validar content-type declarado
            if (!_alowwed.Contains(file.ContentType))
                throw new InvalidOperationException("Formato no permitido.");

            //2) Validar firma real (cargar con ImageSharp). Esto evita spooting.
            //puede lanzar exepciones si no es imagen o esta corrupta.
            //hay que aplicar manejo de excepciones aqui y luego en el controlador
            //en el controlador se puede capturar y guardar en el modelstate
            using var image = await Image.LoadAsync(file.OpenReadStream(), ct);

            //3) Normaliza: recortar cuadrado y redimensionar (p.ej. 512x512)
            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(512, 512),
                Mode = ResizeMode.Crop
            }));

            //4) Elegir extension de salida (recomendado: webp o jpg)
            var ext = ".webp";
            var folderRel = $"uploads/eventos/{userId}";
            var folderAbs = Path.Combine(_env.WebRootPath, "uploads", "eventos", userId);

            Directory.CreateDirectory(folderAbs);

            var fileName = $"{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}_{Guid.NewGuid():N}{ext}";
            var absPath = Path.Combine(folderAbs, fileName);
            var relPath = $"{folderRel}/{fileName}".Replace("\\", "/");

            await image.SaveAsWebpAsync(absPath, ct); // necesita SixLabors.ImageSharp.Formats.Webp

            return relPath;
        }

        public Task DeleteAsync(string? relativePath, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(relativePath)) return Task.CompletedTask;
            var abs = Path.Combine(_env.WebRootPath, relativePath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
            if (File.Exists(abs)) File.Delete(abs);
            return Task.CompletedTask;
        }
    }
}
