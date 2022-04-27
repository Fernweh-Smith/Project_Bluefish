using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;
using DG.Tweening;

[RequireComponent(typeof(ARSelectionInteractable_02))]
public class ARResetLocalTransformInteractable : ARBaseGestureInteractable
{
    TapTracker tapTracker = new TapTracker();

    protected override bool CanStartManipulationForGesture(TapGesture gesture) => IsGameObjectSelected();

    protected override void OnEndManipulation(TapGesture gesture)
    {
        if (gesture.isCanceled)
            return;
        if (!tapTracker.IsDoubleTap(Time.time))
            return;
        transform.DOLocalRotateQuaternion(Quaternion.identity, 0.5f);
        transform.DOScale(Vector3.one, 0.5f);
        transform.DOLocalMove(Vector3.zero, 0.5f);
    }

}