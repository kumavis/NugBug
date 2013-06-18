
using UnityEngine;
using UnityEditor;

namespace NugBug 
{

  public class NugBugWindow : EditorWindow 
  {
    
    static NugBugWindow window;

    static string versionInfo = "NugBug v0.0.0";
    static string greeting = "This is NugBug. Welcome to the nug life. Can I NuGet you something?";

    public static string consoleBuffer = "";
    static string commandPrompt = "";

    static Vector2 consoleScroll = new Vector2();

    
    // Add menu named "My Window" to the Window menu
    [MenuItem ("Window/NugBug")]
    static void Init () 
    {
      // Get existing open window or if none, make a new one:
      window = (NugBugWindow)EditorWindow.GetWindow (typeof (NugBugWindow));
      InitConsoleBuffer();
      NugBug.Help();
    }
    
    public static void InitConsoleBuffer() 
    {
      consoleBuffer = "==========\n"+versionInfo+"\n==========" + "\n\n" + greeting;
    }

    void OnGUI () 
    {
      GUILayout.Label(versionInfo, EditorStyles.boldLabel);
      consoleScroll = GUILayout.BeginScrollView(consoleScroll, GUILayout.ExpandHeight(true));
        GUILayout.TextField(consoleBuffer, GUILayout.ExpandHeight(true));
      GUILayout.EndScrollView();

      GUILayout.BeginHorizontal();        
        if (GUILayout.Button(">>", GUILayout.ExpandWidth(false))) 
        {
          NugBug.NuGetProcess(commandPrompt);
        }
        GUILayout.Label("nuget",GUILayout.ExpandWidth(false));
        commandPrompt = GUILayout.TextArea(commandPrompt, GUILayout.ExpandWidth(true));
        // Check for return key
        if (commandPrompt.Contains("\n"))
        {
          NugBug.NuGetProcess(commandPrompt);
          commandPrompt = "";
        }
      GUILayout.EndHorizontal();
      
      //if (GUILayout.Button("Main")) { NugBug.Main(); }
      if (GUILayout.Button("Help")) { NugBug.Help(); }

    }

    public static void WriteToConsole(string output) 
    {
      consoleBuffer += "\n"+output;
      // Scroll to bottom
      consoleScroll.y = 999999999;
    }

  }
}