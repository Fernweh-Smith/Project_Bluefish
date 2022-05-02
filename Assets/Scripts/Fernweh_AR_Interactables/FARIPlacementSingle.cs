using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.AR;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;



namespace Fernweh.AR.Interactables
{
    [Serializable]
    public class ARObjectSinglePlacementEvent : UnityEvent<ARObjectSinglePlacementEventArgs>
    {
    }

    public class ARObjectSinglePlacementEventArgs
    {
        //Hello There
        /// <summary>
        /// The Interactable that placed the object.
        /// </summary>
        public FARIPlacementSingle placementInteractable { get; set; }

        /// <summary>
        /// The object that was placed.
        /// </summary>
        public GameObject placementObject { get; set; }
    }



    [AddComponentMenu("XR/Fernweh/FARIPlacementSingle")]
    public class FARIPlacementSingle : ARBaseGestureInteractable
    {
        [SerializeField]
        private GameObject placementPrefab;

        [SerializeField]
        private ARObjectSinglePlacementEvent onObjectPlaced;


        private GameObject placedObject;

        private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

        private static GameObject trackablesObject;



        protected override bool CanStartManipulationForGesture(TapGesture gesture)
        {

            // Debug.Log("CanStartManipulationForGesture Called");
            // Debug.Log(gesture.startPosition);
            //if (Helpers.IsOverUI(gesture.startPosition)) { return false; }
            // Only Works On Standalone Input Module
            if(EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(gesture.fingerId))
            {
                Debug.Log("Gesture Over UI");
                return false;
            }

            //if(Helpers.IsOverUINEW(gesture.startPosition)){ return false; }



            if (gesture.targetObject != null)
            {
                Debug.Log("Gesture Has Target");
                Debug.Log("Target Object = " + gesture.targetObject.ToString());

                return false;
            }


            return true;

        }



        protected override void OnEndManipulation(TapGesture gesture)
        {
            base.OnEndManipulation(gesture);
            // Debug.Log("OnEndManipulation Called");
            if (gesture.isCanceled)
            {
                Debug.Log("Gesture Cancelled");
                return;
            }



            if (GestureTransformationUtility.Raycast(gesture.startPosition, hits,
                this.arSessionOrigin, TrackableType.PlaneWithinPolygon))
            {
                var hit = hits[0];


                if (placedObject == null)
                {
                    placedObject = Instantiate(placementPrefab, hit.pose.position, hit.pose.rotation);

                    var anchor = new GameObject("PlacementAnchor!").transform;
                    anchor.position = hit.pose.position;
                    anchor.rotation = hit.pose.rotation;
                    placedObject.transform.parent = anchor; //Without this we cannot move object with interactables


                    //if(trackablesObject == null) { trackablesObject = GameObject.Find("Trackabeles"); }
                    //if(trackablesObject != null){ anchor.parent = trackablesObject.transform; }

                    ARObjectSinglePlacementEventArgs e = new ARObjectSinglePlacementEventArgs();
                    e.placementInteractable = this;
                    e.placementObject = placedObject;

                    onObjectPlaced?.Invoke(e);
                }
                else if (!placedObject.GetComponent<ARSelectionInteractable>().isSelected)
                {
                    placedObject.transform.position = hit.pose.position;
                    placedObject.transform.rotation = hit.pose.rotation;
                }
            }
        }
    }
}

