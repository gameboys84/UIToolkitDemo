using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UITabs : MonoBehaviour
{
    private VisualElement root;

    private const string TabLabelSelected = "tabs-label-selected";
    private const string TabContentSelected = "tabs-desc-selected";

    private int curTabIndex = 0;
    private List<VisualElement> tabCtrls;
    private List<VisualElement> tabContents;
    
    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        tabCtrls = root.Query(className: "tabs-label").ToList();
        tabContents = root.Query(className: "tabs-desc-text").ToList();
    }

    private void Start()
    {
        for (int i = 0; i < tabCtrls.Count; i++)
        {
            tabCtrls[i].AddToClassList(TabLabelSelected);
            var i1 = i;
            tabCtrls[i].RegisterCallback<ClickEvent>(evt =>
            {
                SelectTab(i1);
            });
        }
        
        SelectTab(curTabIndex);
    }

    private void SelectTab(int index)
    {
        Debug.Log("SelectTab:" + index);
        if (index < 0 || index >= tabContents.Count)
        {
            return;
        }

        curTabIndex = index;
        for (int i = 0; i < tabContents.Count; i++)
        {
            // tabCtrls[i].ClearClassList();
            
            if (i == index)
            {
                tabCtrls[i].EnableInClassList(TabLabelSelected, true);
                tabContents[i].EnableInClassList(TabContentSelected, true);
                // tabCtrls[i].AddToClassList(TabLabelSelected);
                // tabContents[i].AddToClassList(TabContentSelected);
            }
            else
            {
                // tabCtrls[i].AddToClassList("tabs-label");
                
                tabCtrls[i].EnableInClassList(TabLabelSelected, false);
                tabContents[i].EnableInClassList(TabContentSelected, false);
                // tabCtrls[i].RemoveFromClassList(TabLabelSelected);
                // tabContents[i].RemoveFromClassList(TabContentSelected);
            }
        }
    }
}
