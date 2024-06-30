using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;

namespace SimpleHttpServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        // Define the file content as a stream
        private Stream GetFileContentStream()
        {
            // Simulate a large content stream
            var largeContent = new string('A', 10 * 1024 * 1024); // 10MB content
            var byteArray = Encoding.UTF8.GetBytes(largeContent);
            return new MemoryStream(byteArray);
        }

        // Handle GET /file.txt
        [HttpGet("file.txt")]
        public IActionResult GetFile()
        {
            // Get the file content as a stream
            var fileStream = GetFileContentStream();
            // Return the file content as a downloadable file using FileStreamResult
            return new FileStreamResult(fileStream, "text/plain")
            {
                FileDownloadName = "file.txt"
            };
        }
    }
}
