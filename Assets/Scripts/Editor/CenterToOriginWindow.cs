using UnityEngine;
using System;
using UnityEditor;


public class CenterToOriginWindow : EditorWindow
{
    bool useLocalSpace;
    [MenuItem("Window/CenterToOrigin")]
    public static void ShowWindow(){
        EditorWindow.GetWindow<CenterToOriginWindow>("CenterToOrigin");
    }
    private void OnGUI() {
        //Window Code
        GUILayout.Label("Transforms the object center to origin.", EditorStyles.boldLabel);
        useLocalSpace = GUILayout.Toggle(useLocalSpace, "Use Local Space");

        if(GUILayout.Button("Transform")){
            
            var sel = Selection.gameObjects;
            if(sel.Length == 0){
                Debug.LogWarning("No Objects Selected");
                return;
            }
            
            foreach (GameObject obj in sel)
            {
                // Undo.RecordObject(obj, "CenterToOrigin");
                // PrefabUtility.RecordPrefabInstancePropertyModifications(obj);
                CenterObject(obj);
                // EditorUtility.SetDirty(obj);
            }
            
        }
    }

    void CenterObject(GameObject obj){
        Collider objCollider = obj.GetComponent<Collider>();
        if(objCollider == null){
            Debug.LogWarning($"{obj.name} has no collider attatched to it.");
            return;
        }
        Vector3 boundsCenter = objCollider.bounds.center;
        Vector3 pivotPos = obj.transform.position;
        Vector3 localCenter = boundsCenter-pivotPos;
        
        if(useLocalSpace){
            obj.transform.localPosition = -localCenter;
            
        } else {
            obj.transform.position = -localCenter;
        }
    }
}
