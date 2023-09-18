using System.Collections.Generic;
using UnityEngine.Scripting;

namespace Padoru.Localization
{
	public class LocalizationFile : ILocalizationFile
	{
		public string fileName;
		
		public Dictionary<string, Dictionary<Languages, string>> entries;
		
		public string FileName => fileName;
		
		public Dictionary<string, Dictionary<Languages, string>> Entries => entries;

		// We need a constructor with Preserve attribute so unity doesn't strip it out.
		// Stripping it will cause runtime errors when the game tries to load the localization file. 
		[Preserve]
		public LocalizationFile()
		{
			
		}
	}
}
