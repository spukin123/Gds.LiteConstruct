using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Gds.LiteConstruct.Environment
{
    internal static class WorkspaceData
    {
        internal const string TexturesFile = "Textures.dat";
        internal const string TexturesFolder = "Textures";

        internal const string ModelTexturesFolder = "Textures";
        internal const string ModelDataFile = "Model.dat";

        private static string workDirectory;

        internal static string WorkDirectory
        {
            get { return workDirectory; }
        }

        private static string texturesDirectory;

        internal static string TexturesDirectory
        {
            get { return texturesDirectory; }
        }

        internal static void SetWorkDirectory(string path)
        {
            workDirectory = path;
            texturesDirectory = Path.Combine(workDirectory, TexturesFolder);
        }
    }
}
