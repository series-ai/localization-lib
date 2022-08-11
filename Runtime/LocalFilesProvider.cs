using Padoru.Core.Files;
using Padoru.Core;
using System.IO;

using Debug = Padoru.Diagnostics.Debug;

namespace Padoru.Localization
{
	public class LocalFilesLoader : ILocalizationFilesLoader
	{
		private IFileManager fileManager;
		private string path;
		private string fileExtension;

		public LocalFilesLoader(string path, string fileExtension)
		{
			this.path = path;
			this.fileExtension = fileExtension;

			fileManager = Locator.GetService<IFileManager>();
		}

		public LocalizationFile LoadFile(string fileName)
		{
			var fullPath = Path.Combine(path, fileName + "." + fileExtension);

			var uri = Protocols.LOCAL_JSON_PPROTOCOL + fullPath;

			if (!fileManager.Exists(uri))
			{
				Debug.LogError($"Cannot load file {fullPath} because it does not exist", Constants.LOCALIZATION_LOG_CHANNEL);
				return null;
			}

			LocalizationFile result = null;
			fileManager.Get<LocalizationFile>(uri, (file) =>
			{
				result = file.Data;
			});

			return result;
		}
	}
}
