using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.AR;
using DG.Tweening;

namespace Fernweh.AR.Interactables
{
    [AddComponentMenu("XR/Fernweh/FARIViewAxisRotation")]
    [RequireComponent(typeof(FARISelection))]
    public class FARIViewAxisRotation : ARBaseGestureInteractable
    {
        private Camera arcamera;
        [SerializeField]
        private float dragRotationRate = 1f;
        [SerializeField]
        private float twistRotationRate = 2.5f;


        protected override bool CanStartManipulationForGesture(DragGesture gesture) => IsGameObjectSelected();
        protected override bool CanStartManipulationForGesture(TwistGesture gesture) => IsGameObjectSelected();


        protected override void OnStartManipulation(DragGesture gesture)
        {
            Debug.Log("OnStartManipulation for drag called");
            transform.DOKill();
            arcamera = Camera.main;
        }

        protected override void OnStartManipulation(TwistGesture gesture)
        {
            Debug.Log("OnStartManipulation for twist called");

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

            float dragNoramliser = Mathf.Min(Screen.width, Screen.height);

            var horizontalRotation = -360f * (gesture.delta.x / dragNoramliser) * dragRotationRate;
            var verticalRotation = -360f * (gesture.delta.y / dragNoramliser) * dragRotationRate;

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
}
