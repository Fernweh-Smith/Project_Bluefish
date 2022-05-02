using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class PropController : MonoBehaviour
{
    public static event Action<PropController> InteractableSelected;
    public static event Action<PropController> InteractableDeselected;
    

    [SerializeField]
    Transform interactable;
    [SerializeField]
    Transform model;
    


    public void HandleInteractaleSelected(){
        InteractableSelected?.Invoke(this);
    }
    public void HandleInteractaleDeselected(){
        InteractableDeselected?.Invoke(this);
    }

    public void ResetLocalTransform(){
        
        interactable.DOLocalRotate(Vector3.zero, 0.5f);
        interactable.DOScale(Vector3.one, 0.5f);
        interactable.DOLocalMove(Vector3.zero, 0.5f);
    }

    public void MoveProp(){

    }
}
