using System.IO;
using UnityEditor;
using UnityEngine;

namespace Jamie.Tools
{

    /// <summary>
    /// Tools for creating starter directories
    /// </summary>
    public static class CreateFolders
    {
        
        /// <summary>
        /// Creates starting directories
        /// </summary>
        [MenuItem("Tools/Create Folders")]
        public static void SetupDefaultFolders()
        {
            CreateFolder("_Project", 
                "Art",
                "Art/3D",
                "Art/2D",
                "Art/Material",
                "Art/Shader",
                "Scripts",
                "Settings",
                "Data");
            
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// Creates folders at a given ro
        /// </summary>
        /// <param name="root"></param>
        /// <param name="directories"></param>
        private static void CreateFolder(string root, params string[] directories)
        {
            string fullPath = Path.Combine(Application.dataPath, root);

            foreach (var dir in directories)
            {
                System.IO.Directory.CreateDirectory(Path.Combine(fullPath, dir));
            }
        }

    }
}