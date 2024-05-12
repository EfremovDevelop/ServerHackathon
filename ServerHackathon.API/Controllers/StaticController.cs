using System.Reflection;
using Microsoft.AspNetCore.Mvc;


namespace ServerHackathon.API.Controllers
{
    [Route("static")]
    [ApiController]
    public class StaticController : ControllerBase
    {

        public static IWebHostEnvironment  _webHostEnvironment;
        public StaticController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            
            if (string.IsNullOrWhiteSpace(_webHostEnvironment.WebRootPath))
            {
                _webHostEnvironment.WebRootPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "wwwroot");
            }
            Console.WriteLine("env: WebRootPath = "+_webHostEnvironment.WebRootPath);
        }
        [HttpGet("{folder}/{filename}")]
        public IActionResult Get(string folder, string filename)
        {
            
            var path = Path.Combine(_webHostEnvironment.WebRootPath, folder, filename);
            try
            {
                var imageFileStream = System.IO.File.OpenRead(path);
                return File(imageFileStream, "image/jpeg");
                
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }
        [HttpGet("{folder}/{subfolder}/{filename}")]
        public IActionResult GetSubFolder(string folder, string subfolder,string filename)
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, folder, subfolder, filename);
            try
            {
                var imageFileStream = System.IO.File.OpenRead(path);
                return File(imageFileStream, "image/jpeg");
                
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }
    }
}