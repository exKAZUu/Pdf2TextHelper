using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Pdf2TextForAntConc
{
	class Program
	{
		static void Main(string[] args) {
			//args = new[] { @"C:\Users\exKAZUu\Dropbox\Private\Tools\English\pdfs" };
			args = new[] { @"C:\Users\exKAZUu\Dropbox\Private\TeX\EclipseWorkspace\GAS2013" };
			var filePaths = args.SelectMany(a => Directory.EnumerateFiles(a, "*.pdf"));
			foreach (var filePath in filePaths) {
				var info = new ProcessStartInfo {
						FileName = "pdftotext.exe",
						Arguments = "-enc UTF-8 \"" + filePath + "\"",
						CreateNoWindow = true,
						UseShellExecute = false,
				};
				var proc = Process.Start(info);
				var textPath = Path.ChangeExtension(filePath, ".txt");
				proc.WaitForExit();
				var text = File.ReadAllText(textPath, Encoding.UTF8);
				text = text.Replace("ﬁ", "fi")
						.Replace("ﬂ", "fl")
						.Replace("ﬀ", "ff")
						.Replace("ﬃ", "ffi")
						.Replace("’", "'");
				File.WriteAllText(textPath, text);
			}
		}
	}
}
