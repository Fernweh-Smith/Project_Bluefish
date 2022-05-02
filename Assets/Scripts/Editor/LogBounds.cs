using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class LogBoundsWindow : EditorWindow
{
    
    [MenuItem("Window/Fernweh/LogBounds")]
    public static void ShowWindow(){
        EditorWindow.GetWindow<LogBoundsWindow>("Log Bounds");
    }

    private void OnGUI() {
        //Window Code
        GUILayout.Label("Logs the details of the objects bounds.", EditorStyles.boldLabel);

        if(GUILayout.Button("Log")){
            
            var sel = Selection.gameObjects;
            if(sel.Length == 0){
                Debug.LogWarning("No Objects Selected");
                return;
            }
            
            foreach (GameObject obj in sel)
            {
                LogBounds(obj);
            }
            
        }
    }

    void LogBounds(GameObject obj){
        Collider collider = obj.GetComponent<Collider>();
        MeshFilter meshFilter = obj.GetComponent<MeshFilter>();

        if(meshFilter == null){
            Debug.LogWarning($"No MeshFilter present on GameObject {meshFilter.transform}.");
            return;
        }else{
            Debug.Log(($"Mesh bounds size = {meshFilter.sharedMesh.bounds.size}"));
        }
        
        if(collider == null){
            Debug.LogWarning($"No MeshFilter present on GameObject {collider.transform}.");
            return;
        }else{
            Debug.Log(($"Collider bounds size = {collider.bounds.size}"));
        }

    }
}
