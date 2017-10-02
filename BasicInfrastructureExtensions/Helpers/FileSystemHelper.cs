using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BasicInfrastructureExtensions.Helpers
{
    
    public static class FileSystemHelper
    {
        public static void EnsureDirectoriesExist(string path)
        {
            var file = Path.GetFileName(path);
            
            if (!string.IsNullOrEmpty(file)) path = path.Replace(file, "");
            
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }
        
        /// <summary>
        /// Delete the file if its size is greater then size
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="size">File size in bytes</param>
        public static void DeleteIf(string path, int size)
        {
            if (!File.Exists(path)) return;
            var info = new FileInfo(path);
            if (info.Length > size) File.Delete(path);
        }
        
        public static void Delete(string path)
        {
            if (!File.Exists(path)) return;
            File.Delete(path);
        }
        
        public static long DirSize(string sourceDir, bool recurse)
        {
            long size = 0;
            var fileEntries = Directory.GetFiles(sourceDir);
            
            foreach (var fileName in fileEntries)
            {
                Interlocked.Add(ref size, (new FileInfo(fileName)).Length);
            }
            
            if (recurse)
            {
                var subdirEntries = Directory.GetDirectories(sourceDir);
                
                Parallel.For<long>(0, subdirEntries.Length, () => 0, (i, loop, subtotal) =>
                {
                    if ((File.GetAttributes(subdirEntries[i]) & FileAttributes.ReparsePoint) != FileAttributes.ReparsePoint)
                    {
                        subtotal += DirSize(subdirEntries[i], true);
                        return subtotal;
                    }
                    return 0;
                },
                    x => Interlocked.Add(ref size, x));
            }
            return size;
        }
    }
}