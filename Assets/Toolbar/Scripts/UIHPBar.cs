using System;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

[RequireComponent(typeof(UIDocument))]
public class UIHPBar : MonoBehaviour
{
    private VisualElement root;
    
    private VisualElement hpBar;
    private Label hpBarText;

    [SerializeField] private int maxHp = 100; 
    [SerializeField] private int hp = 100; 
    
    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        hpBar = root.Q<VisualElement>("HealthBarMask");
        hpBarText = root.Q<Label>("HealthLabel");
    }

    private void Start()
    {
        var btnAddHp = root.Q<Button>("AddHp");
        var btnSubHp = root.Q<Button>("SubHp");

        btnAddHp.clicked += () =>
        {
            var value = Random.Range(5, 30);
            OnHPChanged(value);
        };
        btnSubHp.clicked += () =>
        {
            var value = Random.Range(5, 30);
            OnHPChanged(-value);
        };
        
        OnHPChanged(0);
    }

    private void OnHPChanged(int value)
    {
        hp += value;
        if (hp > maxHp)
        {
            hp = maxHp;
        }
        else if (hp < 0)
        {
            hp = 0;
        }

        var percent = (float)hp / maxHp;
        hpBar.style.width = Length.Percent(Mathf.Lerp(8, 88, percent));
        hpBarText.text = $"{hp}/{maxHp}";
        Debug.Log($"HP changed: {value}, {hp}/{maxHp}, percent:{percent}");
    }
}
