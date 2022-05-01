using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    void HandleDeselection(PropController ctrl){
        Debug.Log($"{ctrl} has been deselected");
    }
}
