using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class ARSelectionInteractable_02 : ARSelectionInteractable
{
    TapTracker tapTracker;
    TapGesture lastTap;

    // gestureInteractor;
    bool secondTap = false;
    IEnumerator coroutine;

    bool trackStarted = false;
    bool canProceed = false;

    public bool toggle;

    //// NEW APPROACH ////
    // Store if selection state changed on last tap
    // If it did and current tap is a double tap, reverse the selection mode
    
    private void Start()
    {
        CanStartManipulationForGesture(new TapGesture(new TapGestureRecognizer(), new Touch()));
    }

    public override bool IsSelectableBy(IXRSelectInteractor interactor) => toggle;

    protected override bool CanStartManipulationForGesture(TapGesture gesture)
    {
        if (EventSystem.current.IsPointerOverGameObject(gesture.fingerId))
        {
            return false;
        }

        // Wait for second tap
        //If none comes then we good to go

        if (tapTracker == null)
        {
            tapTracker = new TapTracker(gesture);
            //gestureInteractor = Component.FindObjectOfType<ARGestureInteractor>();
            Debug.Log(gestureInteractor);
            gestureInteractor.tapGestureRecognizer.onGestureStarted += HandleTap;
        }

        trackStarted = true;
        if (trackStarted)
        {
            Debug.Log("Start");
            secondTap = false;
            coroutine = WaitCountdown();
            StartCoroutine(coroutine);
            Debug.Log("End");
            Debug.Log(canProceed);
            //return canProceed;
            
        }

        
        return true;

    }

    IEnumerator WaitCountdown()
    {
        yield return new WaitForSeconds(0.2f);
        if(!secondTap){
            canProceed = true;
        } else {
            canProceed = false;
        }

    }

    public void HandleTap(TapGesture g)
    {
        secondTap = true;
        Debug.Log("Tapped");
    }
}
