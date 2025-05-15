using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Summary description for link
/// </summary>
public class link
{
	public link()
	{
		//
		// TODO: Add constructor logic here
		//
	}
	public string GenerateNodePath(string title)
	{
		string lowerCaseTitle = title.ToLower();

		string normalizedString = lowerCaseTitle.Normalize(NormalizationForm.FormD);
		Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
		string withoutDiacritics = regex.Replace(normalizedString, string.Empty)
										.Replace('\u0111', 'd') 
										.Replace('\u0110', 'd');
		// Giữ lại ký tự chữ, số và dấu gạch ngang
		string onlyAlphanumericAndSpace = Regex.Replace(withoutDiacritics, @"[^a-z0-9\s\-]", "").Trim();

		// Xóa các khoảng trắng thừa trước
		string removedExtraSpaces = Regex.Replace(onlyAlphanumericAndSpace, @"\s{2,}", " "); // Xóa khoảng trắng dư thừa (hơn 1 khoảng trắng liên tiếp)

		// Thay thế các khoảng trắng còn lại bằng dấu gạch ngang
		string replacedSpaces = Regex.Replace(removedExtraSpaces, @"\s+", "-");

		return replacedSpaces;
	}
}