using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;
using DG.Tweening;

[RequireComponent(typeof(ARSelectionInteractable_02))]
public class ARResetLocalTransformInteractable : ARBaseGestureInteractable
{
    TapTracker tapTracker = new TapTracker();
    ARSelectionInteractable_02 selectionInteractable;


    private void Start() {
        selectionInteractable = GetComponent<ARSelectionInteractable_02>();
        Debug.Log(selectionInteractable);
        selectionInteractable.OnDoubleTap += HandleDoubleTapEvent;
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        selectionInteractable.OnDoubleTap -= HandleDoubleTapEvent;
        
    }

    protected override bool CanStartManipulationForGesture(TapGesture gesture) => IsGameObjectSelected();

    private void HandleDoubleTapEvent(bool NowSelected){
        if(NowSelected)
            ResetRotation();
    }

    public void ResetRotation()
    {
        transform.DOLocalRotateQuaternion(Quaternion.identity, 0.5f);
    }

    protected override void OnEndManipulation(TapGesture gesture)
    {
        Debug.Log("OnEndManipulation for tap called");
        if (gesture.isCanceled)
            return;
        if (tapTracker.IsDoubleTap(Time.time))
        {
            transform.DOLocalRotateQuaternion(Quaternion.identity, 0.5f);

        }

    }

}