using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UIToolbar : MonoBehaviour
{
    private VisualElement root;
    
    private void Awake()
    {
        var uiDoc = GetComponent<UIDocument>();
        root = uiDoc.rootVisualElement;
    }

    private void Start()
    {
        var button = new Button(() => Debug.Log("Button clicked!")) { text = "Click me!" };
        button.style.alignSelf = Align.Center;
        button.style.width = 300;
        button.style.height = 60;
        
        root.Add(button);
    }
}
