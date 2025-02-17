#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

namespace Jamie.Tools
{

    public class PackageTools : Editor
    {

        [MenuItem("Tools/Packages/Install Input System")]
        private static void InstallInputSystem()
        {
            InstallPackage("com.unity.inputsystem");
        }

        [MenuItem("Tools/Packages/Install Cinemachine")]
        private static void InstallCinemachine()
        {
            InstallPackage("com.unity.cinemachine");
        }

        [MenuItem("Tools/Packages/Install Recorder")]
        private static void InstallRecorder()
        {
            InstallPackage("com.unity.recorder");
        }

        private static AddRequest Request;

        private static void InstallPackage(string descriptor)
        {

            Debug.Log("Starting install of " + descriptor);
            Request = Client.Add(descriptor);
            EditorApplication.update += Progress;

        }

        private static void Progress()
        {
            if (Request.IsCompleted)
            {
                if (Request.Status == StatusCode.Success)
                    Debug.Log("Installed: " + Request.Result.packageId);
                else if (Request.Status >= StatusCode.Failure)
                    Debug.Log(Request.Error.message);

                EditorApplication.update -= Progress;
            }
        }

    }
}
#endif

