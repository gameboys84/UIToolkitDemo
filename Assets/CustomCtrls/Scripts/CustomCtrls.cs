using System;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class CustomCtrls : MonoBehaviour
{
    private VisualElement root;
    [Range(1, 6)]
    [SerializeField] private int fullStar = 5;
    [Range(0, 12)]
    [SerializeField] private int halfStar = 1;
    
    private int _halfStar;
    private int _fullStar;
    
    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        _halfStar = fullStar;
        _fullStar = fullStar;
    }

    private void Start()
    {
        var customVE = root.Q<CustomVisualElement>("CustomVE");
        customVE.Text = "Test VisualElement, this is set at runtime";
    }

    private void Update()
    {
        var isChanged = false;
        if (_halfStar != halfStar)
        {
            isChanged = true;
            _halfStar = halfStar;
        }
        if (_fullStar != fullStar)
        {
            isChanged = true;
            _fullStar = fullStar;
        }
        
        if (isChanged)
        {
            var fiveStartVE = root.Q<FiveStartVE>(className: "five-stars");
            fiveStartVE.MaxStars = fullStar;
            fiveStartVE.HalfStars = Mathf.Min(halfStar, fullStar * 2);
        }
    }
}
