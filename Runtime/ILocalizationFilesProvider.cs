using System.Threading.Tasks;

namespace Padoru.Localization
{
	public interface ILocalizationFilesLoader
	{
		Task<LocalizationFile> LoadFile(string fileUri);
	}
}
