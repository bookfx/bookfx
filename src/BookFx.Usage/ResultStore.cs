namespace BookFx.Usage
{
    using System;
    using System.IO;
    using System.Reflection;

    public static class ResultStore
    {
        private const string ResultsDirName = "Results";

        public static void Save(byte[] bookBytes, string fileName)
        {
            var dirPath = Path.Combine(GetAssemblyDirectory(), ResultsDirName);
            var filePath = Path.Combine(dirPath, fileName);
            Directory.CreateDirectory(dirPath);
            File.WriteAllBytes(filePath, bookBytes);
        }

        private static string GetAssemblyDirectory()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }
}