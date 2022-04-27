using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class ARSelectionInteractable_02 : ARSelectionInteractable
{

    bool selectionActive;
    bool isWaiting = false;

    Coroutine WaitCoroutine;

    public override bool IsSelectableBy(IXRSelectInteractor interactor) => interactor is ARGestureInteractor && selectionActive;


    protected override bool CanStartManipulationForGesture(TapGesture gesture) => !EventSystem.current.IsPointerOverGameObject(gesture.fingerId);


    protected override void OnEndManipulation(TapGesture gesture)
    {
        if (gesture.isCanceled)
            return;
        if (gestureInteractor == null)
            return;

        if (!isWaiting)
        {

            WaitCoroutine = StartCoroutine(WaitForDoubleTap(0.2f, gesture));
        }
        else
        {
            StopCoroutine(WaitCoroutine);
            isWaiting = false;
            Debug.Log("WaitForDoubleTap Coroutine Cancelled");
        }
    }

    IEnumerator WaitForDoubleTap(float maxDelta, TapGesture gesture)
    {
        Debug.Log("WaitForDoubleTap Coroutine Started");
        isWaiting = true;
        yield return new WaitForSeconds(maxDelta);
        isWaiting = false;

        if (gesture.targetObject == gameObject)
        {
            // Toggle selection
            selectionActive = !selectionActive;
        }
        else
            selectionActive = false;

        Debug.Log("WaitForDoubleTap Coroutine Ended");
    }
}
