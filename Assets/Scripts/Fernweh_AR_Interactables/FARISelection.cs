using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.AR;

namespace Fernweh.AR.Interactables
{
    [AddComponentMenu("XR/Fernweh/FARISelection", 0)]
    public class FARISelection : ARBaseGestureInteractable
    {
        bool selectionActive = false;
        bool isWaiting = false;

        bool stateLastInteraction = false;

        Coroutine WaitCoroutine;

        [SerializeField]
        UnityEvent OnSelected;
        [SerializeField]
        UnityEvent OnDeselected;

        [SerializeField, Tooltip("The visualization GameObject that will become active when the object is selected.")]
        GameObject m_SelectionVisualization;
        /// <summary>
        /// The visualization <see cref="GameObject"/> that will become active when the object is selected.
        /// </summary>
        public GameObject selectionVisualization
        {
            get => m_SelectionVisualization;
            set => m_SelectionVisualization = value;
        }

        public override bool IsSelectableBy(IXRSelectInteractor interactor) => interactor is ARGestureInteractor && selectionActive;


        protected override bool CanStartManipulationForGesture(TapGesture gesture)
        {
            if (EventSystem.current == null)
            {
                return true;
            }
            else
            {

                return !EventSystem.current.IsPointerOverGameObject(gesture.fingerId);
            }
        }


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
                // Debug.Log("WaitForDoubleTap Coroutine Cancelled");
            }
        }

        IEnumerator WaitForDoubleTap(float maxDelta, TapGesture gesture)
        {
            // Debug.Log("WaitForDoubleTap Coroutine Started");
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

            if(stateLastInteraction!=selectionActive && selectionActive){
                OnSelected?.Invoke();
            } else if(stateLastInteraction!=selectionActive && !selectionActive){
                OnDeselected?.Invoke();
            }

            stateLastInteraction = selectionActive;

            // Debug.Log("WaitForDoubleTap Coroutine Ended");
        }

        /// <inheritdoc />
        protected override void OnSelectEntering(SelectEnterEventArgs args)
        {
            base.OnSelectEntering(args);
            if (m_SelectionVisualization != null)
                m_SelectionVisualization.SetActive(true);
        }

        /// <inheritdoc />
        protected override void OnSelectExiting(SelectExitEventArgs args)
        {
            base.OnSelectExiting(args);
            if (m_SelectionVisualization != null)
                m_SelectionVisualization.SetActive(false);
        }
    }

}

