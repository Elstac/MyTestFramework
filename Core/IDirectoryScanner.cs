using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Core
{
    public interface IDirectoryScanner
    {
        Assembly[] ScanDirectory(string directory);
    }
    public class DirectoryScanner: IDirectoryScanner
    {
            public Assembly[] ScanDirectory(string directory)
            {
                var dlls = from file in Directory.GetFiles(directory)
                           where Regex.IsMatch(file, @"\.dll$")
                           select file;

                var assemblies = from dll in dlls
                                 select Assembly.LoadFrom(dll);

                return assemblies.ToArray();
            }
    }
}