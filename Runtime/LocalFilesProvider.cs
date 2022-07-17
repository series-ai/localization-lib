using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

using Debug = Padoru.Diagnostics.Debug;

namespace Padoru.Localization
{
	public class LocalFilesProvider : ILocalizationFilesProvider
	{
		private Dictionary<string, LocalizationFile> files = new Dictionary<string, LocalizationFile>();

		private string path;
		private string fileExtension;

		public LocalFilesProvider(string path, string fileExtension)
		{
			this.path = path;
			this.fileExtension = fileExtension;
		}

		public LocalizationFile GetFile(string fileName)
		{
			if (!files.ContainsKey(fileName))
			{
				if (!LoadFile(fileName))
				{
					return null;
				}
			}

			return files[fileName];
		}

		// TODO: Use FileManager when there is one
		private bool LoadFile(string fileName)
		{
			var fullPath = Path.Combine(path, fileName + "." + fileExtension);

			if (!File.Exists(fullPath))
			{
				Debug.LogError($"Cannot load file {fullPath} because it does not exist", Constants.LOCALIZATION_LOG_CHANNEL);
				return false;
			}

			var json = File.ReadAllText(fullPath);
			try
			{
				var file = JsonConvert.DeserializeObject<LocalizationFile>(json);
				files.Add(fileName, file);

				Debug.Log($"Local file loaded {fullPath}", Constants.LOCALIZATION_LOG_CHANNEL);

				return true;
			}
			catch (Exception e)
			{
				Debug.LogException(e, Constants.LOCALIZATION_LOG_CHANNEL);
				return false;
			}
		}
	}
}
