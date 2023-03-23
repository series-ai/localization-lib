using Padoru.Core.Files;
using Padoru.Core;
using System.IO;
using System.Threading.Tasks;
using Debug = Padoru.Diagnostics.Debug;

namespace Padoru.Localization
{
	public class LocalFilesLoader : ILocalizationFilesLoader
	{
		private IFileManager fileManager;
		private string path;
		private string fileExtension;

		public LocalFilesLoader(string path, string fileExtension, IFileManager fileManager)
		{
			this.path = path;
			this.fileExtension = fileExtension;

			this.fileManager = fileManager;
		}

		public async Task<LocalizationFile> LoadFile(string fileName)
		{
			var fullPath = Path.Combine(path, fileName + "." + fileExtension);

			var uri = Protocols.LOCAL_JSON_PPROTOCOL + fullPath;

			if (!await fileManager.Exists(uri))
			{
				Debug.LogError($"Cannot load file {fullPath} because it does not exist", Constants.LOCALIZATION_LOG_CHANNEL);
				return null;
			}

			var file = await fileManager.Read<LocalizationFile>(uri);

			return file.Data;
		}
	}
}
