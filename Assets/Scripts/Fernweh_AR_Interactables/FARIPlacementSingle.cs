using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.AR;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

/*
New Class Flow
Spawn Reticle and Prefab at start. Hide objects
On Frame Update - If no object has been placed, raycast into scene and place reticle. If not hit then hide reticle.
    If object has been placed, do nothing.
On Tap - Raycast into scene, If a hit occurs, show and place prefab, hide reticle, and set "Object Placed" to true.
    If "ObjectPlaced" is true, then dont allow placement to continue
*/

namespace Fernweh.AR.Interactables
{
    [AddComponentMenu("XR/Fernweh/FARIPlacementSingle")]
    public class FARIPlacementSingle : ARBaseGestureInteractable
    {
        [SerializeField]
        GameObject placementPrefab;

        GameObject placementObject;

        [SerializeField]
        GameObject reticlePrefab;

        GameObject reticleObject;

        bool IsObjectPlaced = false;
        [SerializeField]
        bool KeepReticleWhenPlaced;

        private static List<ARRaycastHit> hits = new List<ARRaycastHit>();


        private void Start()
        {
            placementObject = Instantiate(placementPrefab, Vector3.zero, Quaternion.identity);
            reticleObject = Instantiate(reticlePrefab, Vector3.zero, Quaternion.identity);

            placementObject.SetActive(false);
            reticleObject.SetActive(false);

            var StageManagerComponent = placementObject.GetComponent<StageManager>();
            if(StageManagerComponent != null){
                float retScale = StageManagerComponent.StageRadius * 2f;
                reticleObject.transform.localScale = new Vector3(retScale, retScale, retScale);
            }

        }


        private void Update()
        {
            if (IsObjectPlaced)
                return;

            Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
            if (GestureTransformationUtility.Raycast(screenCenter, hits, arSessionOrigin, TrackableType.PlaneWithinPolygon))
            {
                var firstHit = hits[0];
                reticleObject.transform.position = firstHit.pose.position;
                reticleObject.transform.rotation = firstHit.pose.rotation;
                reticleObject.SetActive(true);
            }
            else
            {
                reticleObject.SetActive(false);
            }
        }


        protected override bool CanStartManipulationForGesture(TapGesture gesture)
        {
            if (IsObjectPlaced)
                return false;

            if (Helpers.IsOverUI(gesture.fingerId))
                return false;

            if (gesture.targetObject != null)
                return false;

            return true;
        }


        protected override void OnEndManipulation(TapGesture gesture)
        {
            if(gesture.isCanceled)
                return;

            Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
            if (GestureTransformationUtility.Raycast(screenCenter, hits, arSessionOrigin, TrackableType.PlaneWithinPolygon))
            {
                var firstHit = hits[0];
                placementObject.transform.position = firstHit.pose.position;
                placementObject.transform.rotation = firstHit.pose.rotation;
                placementObject.SetActive(true);
                reticleObject.SetActive(KeepReticleWhenPlaced);
                IsObjectPlaced = true;
            }
            else
            {
                placementObject.SetActive(false);
            }
        }

    }
}

