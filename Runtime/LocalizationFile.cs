using System.Collections.Generic;
using UnityEngine.Scripting;

namespace Padoru.Localization
{
	public class LocalizationFile
	{
		public Dictionary<string, string> entries { get; set; }

		// We need a constructor with Preserve attribute so unity doesn't strip it out.
		// Stripping it will cause runtime errors when the game tries to load the localization file. 
		[Preserve]
		public LocalizationFile()
		{
			
		}
	}
}
