using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

namespace Jamie.Tools
{
    public static class InstallPackages
    {
        private static AddRequest _request;
        
        [MenuItem("Tools/Packages/Install Input System")]
        private static void InstallInputSystem() => AddPackage("com.unity.inputsystem");
        
        [MenuItem("Tools/Packages/Install Cinemachine")]
        private static void InstallCinemachine() => AddPackage("com.unity.cinemachine");
        
        private static void AddPackage(string package)
        {
            _request = Client.Add(package);
            EditorApplication.update += Process;
            
        }

        private static void Process()
        {
            if (!_request.IsCompleted) return;
            
            switch (_request.Status)
            {
                case StatusCode.Success:
                    Debug.Log($"Installed Package: {_request.Result.packageId}");
                    break;
                case StatusCode.Failure:
                    Debug.Log($"Failed to Install Package: {_request.Result.packageId}");
                    break;
            }
            
            EditorApplication.update -= Process;
        }
    }
    
    
}
