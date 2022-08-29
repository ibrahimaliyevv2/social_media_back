using System;
using Microsoft.AspNetCore.Http;

namespace ChaosBackend.Data.Entities
{
	public class FileUpload
	{
		public IFormFile files { get; set; }
	}
}

