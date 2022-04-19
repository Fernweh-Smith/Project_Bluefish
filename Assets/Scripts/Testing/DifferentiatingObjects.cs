using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifferentiatingObjects : MonoBehaviour
{
    public FaceCamera go1;
    public FaceCamera go2;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(go1==go2);
    }

}
