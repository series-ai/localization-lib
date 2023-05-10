using System.Collections.Generic;

namespace Padoru.Localization
{
	public class LocalizationFile : ILocalizationFile
	{
		public string fileName;
		
		public Dictionary<string, Dictionary<Languages, string>> entries;
		
		public string FileName => fileName;
		
		public Dictionary<string, Dictionary<Languages, string>> Entries => entries;
	}
}
