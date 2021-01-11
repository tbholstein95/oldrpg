using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLogDisplay : MonoBehaviour
{

    public string output = "";
    public string stack = "";
    // Start is called before the first frame update
    public void onEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    public void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        output = logString;
        stack = stackTrace;
    }

    public void OnGUI()
    {
        GUI.Label(new Rect(150, 5, 800, 60), output);
        GUI.Label(new Rect(150, 60, 800, 60), stack);
    }
}
