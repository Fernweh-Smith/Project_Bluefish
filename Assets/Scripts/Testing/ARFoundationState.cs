using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARSession))]
public class ARFoundationState : MonoBehaviour
{
    ARSession session;
    // Start is called before the first frame update
    void Start()
    {
        session = this.GetComponent<ARSession>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Session State");
        // Debug.Log(ARSession.state);
        // Debug.Log("Not Tracking Reason");
        // Debug.Log(ARSession.notTrackingReason);
        // Debug.Log("/n");
    }

    private void OnEnable() {
        ARSession.stateChanged += HandleSessionStateChange;
    }

    private void OnDisable() {
        ARSession.stateChanged -= HandleSessionStateChange;
    }

    private void HandleSessionStateChange(ARSessionStateChangedEventArgs e){
        switch (e.state)
        {
            case ARSessionState.SessionInitializing:
                HandleNotTrackingReason();
                break;
            case ARSessionState.SessionTracking:
                Debug.Log("ARFoundation is tracking Correctly");
                break;
            case ARSessionState.Ready:
                Debug.Log("ARFoundation is ready to begin.");
                break;
            case ARSessionState.NeedsInstall:
                Debug.Log("ARFoundation requires install of additional software.");
                break;
            case ARSessionState.Installing:
                Debug.Log("ARFoundation is install addition required software.");
                break;
            case ARSessionState.CheckingAvailability:
                Debug.Log("ARFoundation is checking availability.");
                break;
            case ARSessionState.Unsupported:
                Debug.LogError("ARFoundation is NOT SUPPORTED on this device.");
                break;
            case ARSessionState.None:
                Debug.Log("ARFoundation currently has no state or function.");
                break;
            default:
                Debug.Log($"ARFoundation has changed states.\nNew state is: {e.state}");
                break;
        }
    }

    private void HandleNotTrackingReason()
    {
        switch (ARSession.notTrackingReason)
        {
            case NotTrackingReason.None:
                Debug.Log("Tracking is working normally");
                break;
            case NotTrackingReason.CameraUnavailable:
                Debug.LogWarning("Camera Unavailable! Please adjust permissions or stop other apps that are using the camera");
                break;
            case NotTrackingReason.ExcessiveMotion:
                Debug.LogWarning("Excessive Motion! Please slow down your movements");
                break;
            case NotTrackingReason.InsufficientFeatures:
                Debug.LogWarning("Insufficient Features. Please find an environment with more detail");
                break;
            case NotTrackingReason.InsufficientLight:
                Debug.LogWarning("Insufficient Lighting. Please brighten your scene");
                break;
            case NotTrackingReason.Relocalizing:
                Debug.LogWarning("Tracking was interupted and is now relocalising. Do something while it does that");
                break;
            case NotTrackingReason.Unsupported:
                Debug.LogWarning("Tracking has been lost and we are not sure why. " +
                    "Please ensure your camera lens is clean and you are in an environment with plenty of lighting and features");
                break;
            case NotTrackingReason.Initializing:
                Debug.LogWarning("Tracking is initializing. Please wait");
                break;
            
            default:
                Debug.LogWarning($"ARSession tracking has been lost. Reason is {ARSession.notTrackingReason}");
                break;
        }
    }
}
