using System.Collections.Generic;

namespace Padoru.Localization
{
	public class LocalizationFile
	{
		public string fileName;
		public Dictionary<string, Dictionary<Languages, string>> entries;
	}
}
