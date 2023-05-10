using System.Threading.Tasks;

namespace Padoru.Localization
{
	public interface ILocalizationFilesLoader
	{
		Task<ILocalizationFile> LoadFile(string fileUri);
	}
}
