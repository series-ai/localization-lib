using System.Collections.Generic;

namespace Padoru.Localization
{
    public interface ILocalizationFile
    {
        string FileName { get; }
        
        Dictionary<string, Dictionary<Languages, string>> Entries { get; }
    }
}