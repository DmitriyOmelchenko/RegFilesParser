using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RegFilesParser
{
    public class RegistryFile
    {
        public List<RegistryKey> Keys { get; set; }

        public void Parse(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("Value cannot be null or empty.", nameof(filePath));
            if(!File.Exists(filePath))
                throw new ArgumentException("File is not exists", nameof(filePath));
            Keys=new List<RegistryKey>();
            var j=0;
            var lines = File.ReadAllLines(filePath);
            for (var i = 0; i < lines.Length; i=j)
            {

                if (lines[i].FirstOrDefault() != '[')
                {
                    j++;
                    continue;
                }

                var registryKey = new RegistryKey {Name = lines[i].Trim('[', ']'),Values = new List<RegistryValue>()};
                var registryValues = new List<RegistryValue>();
                //get registry value
                for ( j = ++i;  j<lines.Length && lines[j].FirstOrDefault() != '[' ; j++)
                {

                    var strings = lines[j].Split('=');
                    if (strings.Length < 2)
                        continue;
                    var name = strings[0].Trim('"');
                    string value;
                    var type = "string";
                    if (strings[1].Contains("\""))
                    {
                        value = strings[1];
                        if (value[1] == '\\')
                            value = value.Remove(0, 2);
                        if (value.Contains("\"\""))
                            value=value.Replace("\"\"", "\"");

                    }
                      
                    else
                    {
                        strings = strings[1].Split(':');
                        type = strings[0];
                        value = strings[1];
                        while (lines[j].LastOrDefault() == '\\')
                        {
                            value=value.Trim('\\', '\n');
                            value += lines[++j].Trim();
                        }
                    }
                    
                    registryValues.Add(new RegistryValue {Name = name,Type = type,Value = value});
                }

                registryKey.Values.AddRange(registryValues); 
                Keys.Add(registryKey);
            }
        }

    }
}