using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly UploadService _uploadService;

        public UploadController(UploadService uploadService)
        {
            _uploadService = uploadService;
        }

        [HttpPost]
        public IActionResult Post(List<IFormFile> file)
        {
            try
            {
                if (file == null) return null;
                var urlImage  = new List<string>();

                foreach (var item in file)
                {
                    var urlFile = _uploadService.UploadFile(item);
                    urlImage.Add(urlFile);
                }

                return Ok(new
                {
                    mensagem = "Arquivo salvo com sucesso!",
                    urlImagem = urlImage
                });


            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro no upload: " + ex.Message);
            }

        }
    }
}