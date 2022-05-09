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

        bool m_IsActive;

        Quaternion desiredLocalRotation = Quaternion.identity;
        [SerializeField]
        bool useSmothing = true;
        [SerializeField]
        float trailTime = 0.1f;
        [SerializeField]
        float rotDiffThreshold = 0.0001f;

        
        //This is an interface function. I have no idea how this works other than Its called every frame.
        /// <inheritdoc />
        public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            base.ProcessInteractable(updatePhase);

            if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Fixed)
                UpdateRotation();
        }

        protected override bool CanStartManipulationForGesture(DragGesture gesture) => IsGameObjectSelected();
        protected override bool CanStartManipulationForGesture(TwistGesture gesture) => IsGameObjectSelected();


        protected override void OnStartManipulation(DragGesture gesture)
        {
            transform.DOKill();
            arcamera = Camera.main;
            desiredLocalRotation = transform.localRotation;
        }

        protected override void OnStartManipulation(TwistGesture gesture)
        {
            transform.DOKill();
            arcamera = Camera.main;
            desiredLocalRotation = transform.localRotation;
        }

        protected override void OnContinueManipulation(DragGesture gesture)
        {
            if (arcamera == null)
                return;

            m_IsActive = true;

            //Producing Appropriate rotation from 
            Vector3 ToCamVec = Vector3.Normalize(arcamera.transform.position - transform.position);
            Vector3 UpVec = arcamera.transform.up;
            Vector3.OrthoNormalize(ref ToCamVec, ref UpVec);
            Quaternion ToCamRotation = Quaternion.LookRotation(ToCamVec, UpVec);

            float dragNoramliser = Mathf.Min(Screen.width, Screen.height);

            var horizontalRotation = -360f * (gesture.delta.x / dragNoramliser) * dragRotationRate;
            var verticalRotation = -360f * (gesture.delta.y / dragNoramliser) * dragRotationRate;

            // transform.Rotate((ToCamRotation * Vector3.up), horizontalRotation, Space.World);
            // transform.Rotate((ToCamRotation * Vector3.right), verticalRotation, Space.World);
        
            var camYRot = Quaternion.AngleAxis(horizontalRotation, (ToCamRotation * Vector3.up));
            var camXRot = Quaternion.AngleAxis(verticalRotation, (ToCamRotation * Vector3.right));
            var dragRot = camYRot * camXRot;

            desiredLocalRotation = dragRot * desiredLocalRotation;
            
        }

        protected override void OnContinueManipulation(TwistGesture gesture)
        {
            if (arcamera == null)
                return;

            m_IsActive = true;

            Vector3 ToCamVec = Vector3.Normalize(arcamera.transform.position - transform.position);

            float rotationAmount = -gesture.deltaRotation * twistRotationRate;

            // transform.Rotate(ToCamVec, rotationAmount, Space.World);
            var toCamRot = Quaternion.AngleAxis(rotationAmount, ToCamVec);
            desiredLocalRotation = toCamRot * desiredLocalRotation;

        }

        void UpdateRotation(){
            if(!m_IsActive)
                return;

            Quaternion oldLocalRotation = transform.localRotation;
            Quaternion newLocalRotation = Quaternion.Lerp(
                oldLocalRotation, desiredLocalRotation, (1f/trailTime)* Time.fixedDeltaTime);
            
            if(!useSmothing || Quaternion.Dot(desiredLocalRotation, newLocalRotation)>(1f-rotDiffThreshold)){
                newLocalRotation = desiredLocalRotation;
                m_IsActive = false;
            }

            transform.localRotation = newLocalRotation;
        }
    }
}
