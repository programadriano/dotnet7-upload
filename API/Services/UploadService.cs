using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;


namespace API.Services
{
    public class UploadService
    {
        public string UploadFile(IFormFile file)
        {
            //Salvando a imagem no formato enviado pelo usuário
            using (var stream = new FileStream(Path.Combine("Medias/Imagens", file.FileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var urlfile = Guid.NewGuid() + ".webp";

            // Salvando no formato WebP
            using (var webPFileStream = new FileStream(Path.Combine("Medias/Imagens", urlfile), FileMode.Create))
            {
                using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false))
                {
                    imageFactory.Load(file.OpenReadStream()) //carregando os dados da imagem
                                .Format(new WebPFormat()) //formato
                                .Quality(100) //parametro para não perder a qualidade no momento da compressão
                                .Save(webPFileStream); //salvando a imagem
                }
            }

            return $"http://localhost:5067/medias/imagens/{urlfile}";
        }
    }
}
