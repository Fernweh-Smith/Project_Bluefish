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
        if (e.lightEstimation.averageBrightness.HasValue)
        {
            m_light.intensity = e.lightEstimation.averageBrightness.Value;
        }

        if (e.lightEstimation.averageColorTemperature.HasValue)
        {            
            m_light.colorTemperature = e.lightEstimation.averageColorTemperature.Value;
        }

        if (e.lightEstimation.colorCorrection.HasValue)
        {
            m_light.color = e.lightEstimation.colorCorrection.Value;
        }
    }
}
