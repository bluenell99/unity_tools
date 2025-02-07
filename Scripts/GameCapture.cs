using System;
using System.IO;
using UnityEngine;

namespace Jamie.Tools
{
   public class GameCapture : MonoBehaviour
   {
      [Tooltip("Where to save the captures")]
      [SerializeField] private string _outputFolderPath = Application.dataPath + "/Captures";

      [Tooltip("Name of the file name. The file name will automatically be appended with the frame number and extension. EG: 'Output_0001.png'")]
      [SerializeField] private string _fileName = "Frame"; 
      
      [Tooltip("Target framerate to capture at.")]
      [SerializeField] private int frameRate = 5;
      
      private void Start()
      {
         Time.captureFramerate = frameRate;
         Application.targetFrameRate = frameRate;
         Directory.CreateDirectory(_outputFolderPath);
      }

      private void LateUpdate()
      {
         Capture();
      }

      private void Capture()
      {
         string fileName = $"{_outputFolderPath}/{_fileName}{Time.frameCount:D04}.png";
         string path = Path.Combine(_outputFolderPath, fileName);
         ScreenCapture.CaptureScreenshot(path);
         Debug.Log("Captured Frame:" + Time.frameCount + " at " + path); 
      }
   }
}
