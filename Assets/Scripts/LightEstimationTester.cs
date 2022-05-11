using UnityEngine;
using UnityEngine.XR.ARFoundation;


public class LightEstimationTester : MonoBehaviour
{
    [SerializeField]
    ARCameraManager camManager;

    private void OnEnable()
    {
        camManager.frameReceived += FrameUpdated;
    }

    private void OnDisable()
    {
        camManager.frameReceived -= FrameUpdated;
    }

    private void FrameUpdated(ARCameraFrameEventArgs e)
    {
        Debug.Log("New Frame Received");
        if (e.lightEstimation.averageBrightness.HasValue)
        {
            Debug.Log($"Ambient Intensity = {e.lightEstimation.averageBrightness.Value}");
        }
        else
        {
            Debug.Log("Ambient Insensity Not Available");
        }

        if (e.lightEstimation.averageColorTemperature.HasValue)
        {
            Debug.Log($"Ambient Color Temp = {e.lightEstimation.averageColorTemperature.Value}");
        }
        else
        {
            Debug.Log("Ambient Color Temp Not Available");
        }

        if (e.lightEstimation.colorCorrection.HasValue)
        {
            Debug.Log($"Color Correction =  = {e.lightEstimation.colorCorrection.Value}");
        }
        else
        {
            Debug.Log("Color Correction Not Available");
        }
        Debug.Log("");
    }
}