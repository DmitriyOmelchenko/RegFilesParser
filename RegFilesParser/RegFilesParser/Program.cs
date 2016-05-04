using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegFilesParser
{
    class Program
    {
        static void Main(string[] args)
        {
            RegistryFile registryFile=new RegistryFile();
            try
            {
                registryFile.Parse(filePath: @"C:\Users\pervolo\Downloads\test3.reg");
            }
            catch (ArgumentException argumentException)
            {
                
                Console.WriteLine(argumentException.Message);
            }
           
            Console.ReadKey();
        }
    }
}
