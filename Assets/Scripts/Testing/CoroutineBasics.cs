using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineBasics : MonoBehaviour
{
    bool isWaiting = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MyCoroutine(2f));
    }

    IEnumerator MyCoroutine(float duration){
        Debug.Log("Coroutine Start");
        yield return new WaitForSeconds(duration);
        Debug.Log("Coroutine End");
    }

   
}
