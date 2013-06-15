
using UnityEngine;
using UnityEditor;

namespace NugBug {

  public class NugBugWindow : EditorWindow {
    string consoleBuffer = "NugBug v0.0.0";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;
    
    // Add menu named "My Window" to the Window menu
    [MenuItem ("Window/NugBug")]
    static void Init () {
      // Get existing open window or if none, make a new one:
      NugBugWindow window = (NugBugWindow)EditorWindow.GetWindow (typeof (NugBugWindow));
      HelloWorld.Main();
    }
    
    void OnGUI () {
      GUILayout.Label ("Base Settings", EditorStyles.boldLabel);
      EditorGUILayout.TextField ("Text Field", consoleBuffer);
      
      if (GUILayout.Button("Main")) { NugBug.Main(); }
      if (GUILayout.Button("Ls")) { NugBug.Ls(); }
      if (GUILayout.Button("Help")) { NugBug.Help(); }

      groupEnabled = EditorGUILayout.BeginToggleGroup ("Optional Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle ("Toggle", myBool);
        myFloat = EditorGUILayout.Slider ("Slider", myFloat, -3, 3);
      EditorGUILayout.EndToggleGroup();
    }
  }

}