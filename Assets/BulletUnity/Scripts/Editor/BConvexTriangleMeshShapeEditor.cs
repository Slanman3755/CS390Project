﻿using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;
using BulletUnity;

[CustomEditor(typeof(BConvexTriangleMeshShape))]
public class BConvexTriangleShapeEditor : Editor {

    BConvexTriangleMeshShape script;
    //SerializedProperty hullMesh;

    void OnEnable()
    {
        script = (BConvexTriangleMeshShape)target;
        //GetSerializedProperties();
    }

    /*
    void GetSerializedProperties()
    {
        hullMesh = serializedObject.FindProperty("hullMesh");
    }
    */

    public override void OnInspectorGUI()
    {
        if (script.transform.localScale != Vector3.one)
        {
            EditorGUILayout.HelpBox("This shape doesn't support scale of the object.\nThe scale must be one", MessageType.Warning);
        }
        script.HullMesh = (Mesh)EditorGUILayout.ObjectField("Hull Mesh", script.HullMesh, typeof(Mesh), true);
        script.LocalScaling = EditorGUILayout.Vector3Field("Local Scaling", script.LocalScaling);
        if (GUI.changed)
        {
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(script);
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            Repaint();
        }
    }
}
