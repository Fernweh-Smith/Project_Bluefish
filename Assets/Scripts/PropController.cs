using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using Fernweh.AR.Interactables;

public class PropController : MonoBehaviour
{
    public static event Action<PropController> InteractableSelected;
    public static event Action<PropController> InteractableDeselected;


    [SerializeField]
    Transform interactable;
    [SerializeField]
    Transform model;

    [SerializeField]
    List<Collider> preciseColliders;
    [SerializeField]
    List<Collider> roughColliders;

    public Transform Model
    {
        get => model;
        private set => model = value;
    }

    Vector3 homePos;
    Quaternion homeRot;
    Vector3 homeScale;

    float animTime = 0.5f;
    Ease easing = Ease.InOutCubic;

    private void OnEnable() {
        StageManager.SetHomeTransform += SetHome;
    }

    private void OnDisable() {
        StageManager.SetHomeTransform -= SetHome;
    }

    private void Awake() {
        if(interactable!=null){
            SwitchColliders(true);
        }
    }

    void SetHome(){
        homePos = transform.localPosition;
        homeRot = transform.localRotation;
        homeScale = transform.localScale;
    }

    public void HandleInteractaleSelected()
    {
        InteractableSelected?.Invoke(this);
    }
    public void HandleInteractaleDeselected()
    {
        InteractableDeselected?.Invoke(this);
    }

    public void ResetInteractableTransform()
    {

        interactable.DOLocalRotate(Vector3.zero, animTime).SetEase(easing);
        interactable.DOScale(Vector3.one, animTime).SetEase(easing);
        interactable.DOLocalMove(Vector3.zero, animTime).SetEase(easing);
    }

    
    public void ReturnToHome(){
        transform.DOLocalMove(homePos, animTime).SetEase(easing);
        transform.DOLocalRotateQuaternion(homeRot, animTime).SetEase(easing);
        transform.DOScale(homeScale, animTime).SetEase(easing);
        ResetInteractableTransform();
    }

    public Bounds GetTrueMeshBounds(){
        return Model.GetComponent<MeshFilter>().sharedMesh.bounds;
    }

    public void SwitchColliders(bool usePrecise){
        if(preciseColliders.Count==0 || roughColliders.Count==0)
            return;

        foreach(Collider c in preciseColliders){
            c.enabled = usePrecise;
        }

        foreach(Collider c in roughColliders){
            c.enabled = !usePrecise;
        }
    }
}
