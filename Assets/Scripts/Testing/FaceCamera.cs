using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FaceCamera : MonoBehaviour
{
    public Camera cam;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        Vector3 ToCamVec = Vector3.Normalize(cam.transform.position - transform.position);
        Vector3 UpVec = cam.transform.up;
        Vector3.OrthoNormalize(ref ToCamVec, ref UpVec);
        Quaternion targetRotation = Quaternion.LookRotation(ToCamVec, UpVec);
        transform.rotation = targetRotation;

    }
}
