using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for checkEmail
/// </summary>
public class CheckExecl
{
	public CheckExecl()
	{
		//
		// TODO: Add constructor logic here
		//
	}
	public bool IscheckExcel(string filename, string folderPath)
	{
		var fullPath = Path.Combine(folderPath, filename);

		// Kiểm tra xem tệp có tồn tại không
		return File.Exists(fullPath);
	}
	public bool IsDocOrPdf(string filePath)
	{
		string extension = System.IO.Path.GetExtension(filePath);
		return extension.Equals(".doc", StringComparison.OrdinalIgnoreCase) || extension.Equals(".pdf", StringComparison.OrdinalIgnoreCase);
	}
}