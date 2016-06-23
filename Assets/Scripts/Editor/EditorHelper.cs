using UnityEngine;
using System.Collections;
using UnityEditor;
using SeafoodEditorHelper;
using System;

[CustomEditor(typeof(PlayerStateMachine))]
public class PlayerStateMachineEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PlayerStateMachine myScript = (PlayerStateMachine)target;
        if (GUILayout.Button("Reset StateMachine"))
        {
            myScript.GotoState("GroundState");
            EditorGUIUtility.ExitGUI();
        }
    }
}

[CustomEditor(typeof(MountainBuilder))]
public class MountainBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Box("", new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.Height(1) });

        MountainBuilder myScript = (MountainBuilder)target;
        if (GUILayout.Button("Build Basic"))
        {
            myScript.BuildBasic();
            EditorGUIUtility.ExitGUI();
        }
        if (GUILayout.Button("Build Collider"))
        {
            myScript.BuildCollider();
            EditorGUIUtility.ExitGUI();
        }
    }
}

[CustomEditor(typeof(MountainPartBuilder))]
public class MountainPartBuilderEditor : Editor
{
    static int selectedNeighbor = 0;
    static int selectedpart = 0;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Box("", new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.Height(1) });

        string[] neighborOptions = Enum.GetNames(typeof(MountainNeighborType));
        selectedNeighbor = EditorGUILayout.Popup("NeighborType", selectedNeighbor, neighborOptions);

        string[] partOptions = Enum.GetNames(typeof(MountainPartType));
        selectedpart = EditorGUILayout.Popup("PartType", selectedpart, partOptions);

        if (GUILayout.Button("Build Neighbor"))
        {
            MountainPartBuilder myScript = (MountainPartBuilder)target;
            myScript.BuildNeighbor((MountainNeighborType)selectedNeighbor, (MountainPartType)selectedpart);
            EditorGUIUtility.ExitGUI();
        }
    }
}

[CustomEditor(typeof(Mettool))]
public class MettoolEditor : Editor
{
    static int selectedNeighbor = 0;
    static int selectedpart = 0;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Box("", new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.Height(1) });
        
        if (GUILayout.Button("Shoot"))
        {
            Mettool myScript = (Mettool)target;
            myScript.Shoot();
            EditorGUIUtility.ExitGUI();
        }
        if (GUILayout.Button("Hide"))
        {
            Mettool myScript = (Mettool)target;
            myScript.Hide();
            EditorGUIUtility.ExitGUI();
        }
        if (GUILayout.Button("Move"))
        {
            Mettool myScript = (Mettool)target;
            myScript.Move();
            EditorGUIUtility.ExitGUI();
        }
    }
}
