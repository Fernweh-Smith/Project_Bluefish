using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;
using DG.Tweening;

[RequireComponent(typeof(ARSelectionInteractable))]
public class ARAxisRotationInteractable : ARBaseGestureInteractable
{

    private Camera arcamera;
    [SerializeField]
    private float dragRotationRate = 10f;
    [SerializeField]
    private float twistRotationRate = 2.5f;


    protected override bool CanStartManipulationForGesture(DragGesture gesture) => IsGameObjectSelected();
    protected override bool CanStartManipulationForGesture(TwistGesture gesture) => IsGameObjectSelected();

    public void OnResetRotation(){
        transform.DORotateQuaternion(Quaternion.identity, 0.5f); 
    }

    protected override void OnStartManipulation(DragGesture gesture)
    {
        transform.DOKill();
        arcamera = Camera.main;
    }

    protected override void OnStartManipulation(TwistGesture gesture)
    {
        transform.DOKill();
        arcamera = Camera.main;
    }

    protected override void OnContinueManipulation(DragGesture gesture)
    {
        if (arcamera == null)
            return;

        //Producing Appropriate rotation from 
        Vector3 ToCamVec = Vector3.Normalize(arcamera.transform.position - transform.position);
        Vector3 UpVec = arcamera.transform.up;
        Vector3.OrthoNormalize(ref ToCamVec, ref UpVec);
        Quaternion ToCamRotation = Quaternion.LookRotation(ToCamVec, UpVec);

        var horizontalRotation = -1f * (gesture.delta.x / Screen.dpi) * dragRotationRate;
        var verticalRotation = -1f * (gesture.delta.y / Screen.dpi) * dragRotationRate;

        transform.Rotate((ToCamRotation * Vector3.up), horizontalRotation, Space.World);
        transform.Rotate((ToCamRotation * Vector3.right), verticalRotation, Space.World);

    }

    protected override void OnContinueManipulation(TwistGesture gesture)
    {
        Debug.Log("OnContinueManipulation Called");

        if (arcamera == null)
            return;

        Vector3 ToCamVec = Vector3.Normalize(arcamera.transform.position - transform.position);

        float rotationAmount = -gesture.deltaRotation * twistRotationRate;

        transform.Rotate(ToCamVec, rotationAmount, Space.World);

    }
}
