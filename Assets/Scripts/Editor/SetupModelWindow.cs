using UnityEngine;
using UnityEditor;

public class SetupModelWindow : EditorWindow {
    Mesh modelMesh;
    Material modelMaterial;
    Transform modelTransform;

    [MenuItem("Project_Bluefish/SetupModelWindow")]
    private static void ShowWindow() {
        var window = GetWindow<SetupModelWindow>();
        window.titleContent = new GUIContent("SetupModelWindow");
        window.Show();
    }

    private void OnGUI() {
        GUILayout.Label("Sets Prop model properties");
        modelMesh = (Mesh) EditorGUILayout.ObjectField(modelMesh, typeof(Mesh), true);
        modelMaterial = (Material) EditorGUILayout.ObjectField(modelMaterial, typeof(Material), true);
        modelTransform = (Transform) EditorGUILayout.ObjectField(modelTransform, typeof(Transform), true);

        if(GUILayout.Button("Set Properties")){
            SetProperties();
        }


    }

    void SetProperties(){
        MeshFilter mf = modelTransform.GetComponent<MeshFilter>();
        MeshRenderer mr = modelTransform.GetComponent<MeshRenderer>();
        MeshCollider mc = modelTransform.GetComponent<MeshCollider>();

        if(mf==null || mr==null || mc==null){
            Debug.LogError("Provided Model Object does not have necessary components.\nPlease ensure that MeshFilter, MeshRenderer, and MeshCollider components are all present on the provedided object.");
            return;
        }

        mf.sharedMesh = modelMesh;
        mr.sharedMaterial = modelMaterial;
        mc.sharedMesh = modelMesh;

        Bounds meshBounds = modelMesh.bounds;
        modelTransform.localPosition = -meshBounds.center;
        modelTransform.localRotation = Quaternion.identity;
        modelTransform.localScale = Vector3.one;
    }
}
