using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class ARSelectionInteractable_02 : ARSelectionInteractable
{
    TapTracker tapTracker = new TapTracker();

    bool m_GestureSelected;
    bool changedLastTap;

    public event Action<bool> OnDoubleTap;

    //// NEW APPROACH ////
    // Store if selection state changed on last tap
    // If it did and current tap is a double tap, reverse the selection mode


    public override bool IsSelectableBy(IXRSelectInteractor interactor) => interactor is ARGestureInteractor && m_GestureSelected;


    protected override bool CanStartManipulationForGesture(TapGesture gesture)
    {
        if (EventSystem.current.IsPointerOverGameObject(gesture.fingerId))
        {
            return false;
        }

        return true;

    }

    protected override void OnEndManipulation(TapGesture gesture)
    {
        bool selectionStatePreManipulation = m_GestureSelected;

        if (gesture.isCanceled)
            return;
        if (gestureInteractor == null)
            return;

        if(tapTracker.IsDoubleTap(Time.time) && changedLastTap){
            Debug.Log("Double Tap Occured. Undoing Change.");
            m_GestureSelected = !m_GestureSelected;
            changedLastTap = false;
            OnDoubleTap?.Invoke(m_GestureSelected);
            return;
        }

        if (gesture.targetObject == gameObject)
        {
            // Toggle selection
            m_GestureSelected = !m_GestureSelected;
        }
        else
            m_GestureSelected = false;

        changedLastTap = selectionStatePreManipulation!=m_GestureSelected;
    }
}
