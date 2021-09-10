using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Simple example where EditorApplication.isPlaying is watched to
// report whether game is played or not.

public class EditorPlaying : EditorWindow
{
    public bool justPlayed;
    public GameObject scriptRef;
    [MenuItem("Buttplug.io/ButplugCrashPreventor")]
    static void Init()
    {
        EditorWindow window = GetWindow(typeof(EditorPlaying));
        window.position = new Rect(100, 100, 150, 50);
        window.Show();
    }

    void OnGUI()
    {
        scriptRef = EditorGUILayout.ObjectField(scriptRef, typeof(GameObject), true) as GameObject;
        if (EditorApplication.isPlaying)
        {
            EditorGUILayout.LabelField("Playing");
            justPlayed = true;
            if (GUILayout.Button("Kill Buttplug Server"))
            {
                killServerProcess();
            }

        }
        else
        {
        EditorGUILayout.LabelField("Not playing");
        if(scriptRef != null && justPlayed)
        {
            killServerProcess();
        }
        else
        {
            EditorGUILayout.LabelField("No Script Set");
        }

        if (GUILayout.Button("Start Buttplug Server"))
        {
            EditorApplication.isPlaying = true;
        }
        }
    }

    void killServerProcess()
    {

        StartServerProcessAndScan scriptTarget = scriptRef.gameObject.GetComponent(typeof(StartServerProcessAndScan)) as StartServerProcessAndScan;
        scriptTarget.closePlayMode();
        EditorApplication.isPlaying = false;
        justPlayed = false;
    }
}
