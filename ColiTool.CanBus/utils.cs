using System.Collections.Generic;
using System.Linq;
using IniParser;
using IniParser.Model;

namespace ColiTool.Utils
{
    public class EdsParser
    {

        public Dictionary<string, object> ParseEdsFile(string edsFilePath)
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(edsFilePath);
            return ConvertToDictionary(data);
        }

        public Dictionary<string, object> ConvertToDictionary(IniData data)
        {
            var result = new Dictionary<string, object>();
            foreach (var section in data.Sections)
            {
                var sectionData = new Dictionary<string, string>();
                foreach (var key in section.Keys)
                {
                    sectionData[key.KeyName] = key.Value;
                }
                result[section.SectionName] = sectionData;
            }
            return result;
        }
    }
}
