using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;
using System;
using System.Collections;
using System.Collections.Generic;


public class TapTracker {
    private float lastTapTime = 0f;
    private float doubleTapDeltaTime = 0.2f;
    
    public TapTracker(){}

    public bool IsDoubleTap(float tapTime){
        bool isDoubleTap = tapTime-lastTapTime < doubleTapDeltaTime;
        // Debug.Log("Delta = " + (tapTime-lastTapTime).ToString());
        lastTapTime = tapTime;
        return isDoubleTap;
    }
}