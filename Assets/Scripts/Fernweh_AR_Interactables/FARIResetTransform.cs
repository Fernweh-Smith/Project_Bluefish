using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.AR;
using DG.Tweening;

namespace Fernweh.AR.Interactables
{
    [AddComponentMenu("XR/Fernweh/FARIResetTransform")]
    [RequireComponent(typeof(FARISelection))]
    public class FARIResetTransform : ARBaseGestureInteractable
    {
        float lastTapTime = 0f;
        float doubleTapDeltaTime = 0.2f;
        protected override bool CanStartManipulationForGesture(TapGesture gesture) => IsGameObjectSelected();

        protected override void OnEndManipulation(TapGesture gesture)
        {

            if (gesture.isCanceled)
                return;

            float tapTime = Time.time;
            bool isDoubleTap = (tapTime-lastTapTime) < doubleTapDeltaTime;
            lastTapTime = tapTime;

            if (!isDoubleTap)
                return;

            transform.DOLocalRotateQuaternion(Quaternion.identity, 0.5f);
            transform.DOScale(Vector3.one, 0.5f);
            transform.DOLocalMove(Vector3.zero, 0.5f);
        }
    }
}