using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    float stageRadius = 1f;
    public float StageRadius { get => stageRadius; }
    [SerializeField]
    float packedSize = 0.25f;

    [SerializeField]
    List<PropController> props;

    public static event Action SetHomeTransform;

    private void Start()
    {
        if (props.Count == 0)
            return;

        for (int i = 0; i < props.Count; i++)
        {
            float theta = (Mathf.PI * 2) / (props.Count) * i;
            float x = stageRadius * Mathf.Cos(theta);
            float z = stageRadius * Mathf.Sin(theta);

            var obj = Instantiate(props[i].transform, new Vector3(x, 0, z), Quaternion.identity);

            obj.transform.rotation = Quaternion.LookRotation(Vector3.Normalize(obj.transform.position), Vector3.up);
            PropController objPropController = obj.GetComponent<PropController>();

            Vector3 objBoundsSize = objPropController.GetTrueMeshBounds().size;
            float maxAxis = Mathf.Max(objBoundsSize.x, objBoundsSize.y, objBoundsSize.z);
            obj.transform.localScale *= packedSize / maxAxis;

            obj.transform.Translate(Vector3.up * (packedSize * 0.5f));

            obj.SetParent(this.transform, false);

            objPropController.SwitchColliders(false);
        }
        SetHomeTransform?.Invoke();
    }

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

    void HandleSelection(PropController ctrl)
    {
        Debug.Log($"{ctrl} has been selected");
        float height = ctrl.GetTrueMeshBounds().size.y;
        ctrl.transform.DOLocalMove(new Vector3(0, (height * 0.5f) + 0.2f, 0), 0.5f);
        ctrl.transform.DOScale(Vector3.one, 0.5f);
        ctrl.SwitchColliders(true);
    }

    void HandleDeselection(PropController ctrl)
    {
        Debug.Log($"{ctrl} has been deselected");
        ctrl.ReturnToHome();
        ctrl.SwitchColliders(false);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.HSVToRGB(330f / 360f, 1f, 1f);
        Gizmos.DrawSphere(Vector3.zero, 0.1f);
    }
#endif

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {

        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.HSVToRGB(197 / 360f, 1f, 1f);

        Gizmos.DrawWireSphere(Vector3.zero, stageRadius);

        if (props.Count == 0)
            return;

        for (int i = 0; i < props.Count; i++)
        {
            float theta = (Mathf.PI * 2) / (props.Count) * i;
            float x = stageRadius * Mathf.Cos(theta);
            float z = stageRadius * Mathf.Sin(theta);
            Gizmos.DrawWireCube(new Vector3(x, packedSize * 0.5f, z), new Vector3(packedSize, packedSize, packedSize));

        }
    }
#endif
}
