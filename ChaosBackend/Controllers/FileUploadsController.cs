using System;
using System.IO;
using ChaosBackend.Data.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;


namespace ChaosBackend.Controllers
{
	[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
	[ApiController]
	public class FileUploadsController:ControllerBase
	{
		public static IWebHostEnvironment _env;

        public FileUploadsController(IWebHostEnvironment env)
        {
			_env = env;
        }

		[HttpPost]
		public string Post([FromForm]FileUpload file)
        {
            try
            {
                if (file.files.Length > 0)
                {
                    string path = _env.WebRootPath + "/uploads/";

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using(FileStream filestream = System.IO.File.Create(path + file.files.FileName))
                    {
                        file.files.CopyTo(filestream);
                        filestream.Flush();
                        return "Uploaded";
                    }
                }
                else
                {
                    return "Not uploaded";
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
	}
}

