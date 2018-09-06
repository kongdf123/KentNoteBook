using System;
using System.Collections.Generic;
using System.IO;
using KentNoteBook.Core;
using KentNoteBook.Infrastructure.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace KentNoteBook.WebApp.Handlers
{
	public class FileUploadController : Controller
	{
		public FileUploadController(KentNoteBookDbContext db, IConfiguration configuration, IHostingEnvironment hostingEnv) {
			_db = db;
			_configuration = configuration;
			_hostingEnv = hostingEnv;
		}

		readonly KentNoteBookDbContext _db;
		readonly IConfiguration _configuration;
		readonly IHostingEnvironment _hostingEnv;

		public class FileModel
		{
			public string FileName { get; set; }
			public string FileRelativePath { get; set; }
		}

		[HttpPost]
		public IActionResult Upload([FromForm] IFormFile attachmentFile) {
			if ( attachmentFile == null || attachmentFile.Length == 0 ) {
				return new FailResult(0, "Please select a file to upload.");
			}

			var fileModel = SaveFile(attachmentFile);

			return new SuccessResult(1, fileModel);
		}

		[HttpPost]
		public IActionResult MultiUpload([FromForm] IFormFile[] attachmentFiles) {
			if ( attachmentFiles == null || attachmentFiles.Length == 0 ) {
				return new FailResult(0, "Please select a file to upload.");
			}

			var fileModels = new List<FileModel>();
			foreach ( var file in attachmentFiles ) {
				var fileModel = SaveFile(file);
				fileModels.Add(fileModel);
			}

			return new SuccessResult(1, fileModels);
		}

		FileModel SaveFile(IFormFile file) {
			var extension = Path.GetExtension(file.FileName);
			var fileRelativePath = $"/UploadedFiles/{DateTime.Now.ToString("yyyy/MM/dd")}/{file.FileName.Replace(extension, "")}_{DateTime.Now.ToString("yyyyMMddHHmmss")}{extension}";
			var physicalFileName = $"{_hostingEnv.WebRootPath}/{fileRelativePath}";
			var physicalDirectory = Path.GetDirectoryName(physicalFileName);
			if ( !Directory.Exists(physicalDirectory) ) {
				Directory.CreateDirectory(physicalDirectory);
			}

			using ( var stream = new FileStream(physicalFileName, FileMode.OpenOrCreate) ) {
				file.CopyTo(stream);
				stream.Flush();
			}

			return new FileModel {
				FileName = file.FileName,
				FileRelativePath = fileRelativePath
			};
		}
	}
}