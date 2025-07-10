using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class QuickTool : EditorWindow
{
    // CTRL + SHIFT + T (or Cmd + Shift + T on a Mac).
    [MenuItem("Demo/QuickTool/Open _%#T")]
    public static void ShowWindow()
    {
        QuickTool wnd = GetWindow<QuickTool>();
        wnd.titleContent = new GUIContent("QuickTool");
        wnd.minSize = new Vector2(285, 55);
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;
        
        // // 1. Create an element directly in C# 
        //
        // // VisualElements objects can contain other VisualElement following a tree hierarchy.
        // VisualElement label = new Label("Hello World! From C#");
        // root.Add(label);
        //
        // var button = new Button(() => Debug.Log("Button clicked!")) { text = "Click me!" };
        // button.style.width = 160;
        // button.style.height = 30;
        // root.Add(button);
        
        
        // 2. Create elements using the UI Builder
        root.styleSheets.Add(Resources.Load<StyleSheet>("QuickTool_Style"));
        
        var quickToolVisualTree = Resources.Load<VisualTreeAsset>("QuickTool_Main");
        quickToolVisualTree.CloneTree(root);

        var toolButtons = root.Query(className: "quicktool-button");
        toolButtons.ForEach(SetupButton);
    }

    private void SetupButton(VisualElement button)
    {
        var buttonIcon = button.Q(className: "quicktool-button-icon");
        var iconPath = $"Icons/{button.parent.name}_icon";
        // Debug.Log($"iconPath: {iconPath}");
        var iconAsset = Resources.Load<Texture2D>(iconPath);
        buttonIcon.style.backgroundImage = iconAsset;
        
        button.RegisterCallback<PointerUpEvent, string>(CreateObject, button.parent.name);
        button.tooltip = $"Create a {button.parent.name} object";
    }

    private void CreateObject(PointerUpEvent evt, string primitiveTypeName)
    {
        var pt = Enum.Parse<PrimitiveType>(primitiveTypeName, true);
        var go = ObjectFactory.CreatePrimitive(pt);
        go.transform.position = Vector3.zero;
        Selection.activeObject = go;
    }
}
