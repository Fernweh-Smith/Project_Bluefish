using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PropController : MonoBehaviour
{
    public static event Action<PropController> InteractableSelected;
    public static event Action<PropController> InteractableDeselected;


    public void HandleInteractaleSelected(){
        InteractableSelected?.Invoke(this);
    }
    public void HandleInteractaleDeselected(){
        InteractableDeselected?.Invoke(this);
    }
}
