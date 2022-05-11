using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(Light))]
public class MatchCameraAmbient : MonoBehaviour
{
      
    [SerializeField]
    ARCameraManager camManager;

    Light m_light;

    private void Awake() {
        m_light = GetComponent<Light>();
    }

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
            m_light.intensity = e.lightEstimation.averageBrightness.Value;
        }
        else
        {
            Debug.Log("Ambient Insensity Not Available");
        }

        if (e.lightEstimation.averageColorTemperature.HasValue)
        {
            Debug.Log($"Ambient Color Temp = {e.lightEstimation.averageColorTemperature.Value}");
            m_light.colorTemperature = e.lightEstimation.averageColorTemperature.Value;
        }
        else
        {
            Debug.Log("Ambient Color Temp Not Available");
        }

        if (e.lightEstimation.colorCorrection.HasValue)
        {
            Debug.Log($"Color Correction =  = {e.lightEstimation.colorCorrection.Value}");
            m_light.color = e.lightEstimation.colorCorrection.Value;
        }
        else
        {
            Debug.Log("Color Correction Not Available");
        }
        Debug.Log("");
    }
}
