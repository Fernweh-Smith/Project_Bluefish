using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;
using System;
using System.Collections;
using System.Collections.Generic;


public class TapTracker {
    private float lastTapTime = 0f;
    private float doubleTapDeltaTime = 0.2f;
    private bool secondTap = false;
    
    IEnumerator coroutine;

    ARGestureInteractor gestureInteractor;
    public TapTracker(TapGesture gesture){
        gestureInteractor = Component.FindObjectOfType<ARGestureInteractor>();
        Debug.Log(gestureInteractor);
        gestureInteractor.tapGestureRecognizer.onGestureStarted += ctx => secondTap = true;

        gesture.onStart += ctx =>  Debug.Log("GestureStarted");
        gesture.onUpdated += ctx =>  Debug.Log("GestureUpdated");
        gesture.onFinished += ctx =>  Debug.Log("GestureFinished");
    }

    public bool IsDoubleTap(float tapTime){
        bool isDoubleTap = tapTime-lastTapTime < doubleTapDeltaTime;
        // Debug.Log("Delta = " + (tapTime-lastTapTime).ToString());
        lastTapTime = tapTime;
        return isDoubleTap;
    }

    public bool IsDoubleTap(){
        secondTap = false;
        coroutine = WaitCountdown();
        //StartCoroutine(coroutine);
        return secondTap;

    }

    IEnumerator WaitCountdown(){
        yield return new WaitForSeconds(doubleTapDeltaTime);
    }
}