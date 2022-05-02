using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StageManager : MonoBehaviour
{
    private void OnEnable()
    {
        PropController.InteractableSelected += HandleSelection;
        PropController.InteractableDeselected += HandleDeselection;
    }

    private void OnDisable()
    {
        PropController.InteractableSelected -= HandleSelection;
        PropController.InteractableDeselected -= HandleDeselection;
    }

    void HandleSelection(PropController ctrl){
        Debug.Log($"{ctrl} has been selected");
        float yStart = ctrl.transform.position.y;
        ctrl.transform.DOMoveY(yStart + 0.1f, 0.5f);
    }

    void HandleDeselection(PropController ctrl){
        Debug.Log($"{ctrl} has been deselected");
        float yStart = ctrl.transform.position.y;
        ctrl.transform.DOMoveY(yStart-0.1f, 0.5f);
        ctrl.ResetLocalTransform();
    }
}
