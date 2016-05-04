using System.Collections.Generic;

namespace RegFilesParser
{
    public class RegistryKey
    {
        public string Name { get; set; }
        public List<RegistryValue> Values { get; set; }
    }
}