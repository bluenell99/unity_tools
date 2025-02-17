#if UNITY_EDITOR
using UnityEditor;
#endif
using static UnityEngine.Application;
using static System.IO.Directory;
using static System.IO.Path;

namespace Jamie.Tools
{
    public static class ToolsMenu
    {
#if UNITY_EDITOR
        [MenuItem("Tools/Setup/Create Default Folders")]
        public static void CreateDefaultFolders()
        {
            CreateDirectories("_Project", "Scripts", "Art/Textures", "Art/Materials", "Art/Shaders", "Art/Meshes", "Prefabs", "Scenes", "Data","Settings");
            AssetDatabase.Refresh();
        }

        private static void CreateDirectories(string root, params string[] dir)
        {
            var fullPath = Combine(dataPath, root);

            foreach (var newDir in dir)
            {
                CreateDirectory(Combine(fullPath, newDir));
            }
        }
#endif
    }
}
