using UnityEngine;
using UnityEngine.UIElements;

// 主要包括:
// 1. 切换主题, 通过PanelSettings 设置 theme style sheet, 而 tss 就是指定使用哪些 储存颜色变量的Style Sheet即可
// 2. 界面的出现、消失动画控制, 用classList的添加和删除来控制显隐,  动画的播放由 Transition Animation 来完成
// 3. 动态创建 VisualTreeAsset, 并设置UserData和事件绑定
// 4. 数据填充 有文本的组件,如Label和Button, 可以直接设置text属性 Lable.text
//    所有的VisualElement都带Image, 可以直接设置Image.style.backgroundImage


[RequireComponent(typeof(UIDocument))]
public class UIAnimation : MonoBehaviour
{
    private VisualElement root;
    [SerializeField] private ThemeStyleSheet lightTheme;
    [SerializeField] private ThemeStyleSheet darkTheme;
    
    private VisualElement menuContainer;
    private const string MenuHiddenClass = "menu-container-hidden";
    
    private VisualElement popupContainer;
    private const string PopupHiddenClass = "popup-container-hidden";
    private VisualElement popupBackground;
    private const string PopupBackgroundHiddenClass = "popup-background-hidden";
    
    private void Awake()
    {
        var uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;
    }

    private void Start()
    {
        // 弹出菜单按钮
        root.Q<Button>("ToggleMenu").RegisterCallback<ClickEvent>(OnToggleMenu);
        // 弹出菜单窗口， 再次点击按钮后关闭窗口
        menuContainer = root.Q<VisualElement>("MenuContainer");
        menuContainer.AddToClassList(MenuHiddenClass); // 默认隐藏菜单
        
        // 切换主题按钮
        root.Q<Button>("ToggleAppTheme").RegisterCallback<ClickEvent>(OnToggleAppTheme);
        
        
        popupContainer = root.Q<VisualElement>("PopupContainer");
        popupContainer.AddToClassList(PopupHiddenClass); // 默认隐藏Popup窗口
        // popupContainer.RegisterCallback<ClickEvent>(OnPopupContainerClick);
        
        popupBackground = root.Q<VisualElement>("PopupBackground");
        popupBackground.AddToClassList(PopupBackgroundHiddenClass); // 默认隐藏Popup背景
        popupBackground.RegisterCallback<ClickEvent>(OnPopupBackgroundClick);
        
        var tilesTopContainer = root.Q<VisualElement>("tilesTopContainer");

        // 动态创建 VisualTreeAsset
        tilesTopContainer.Clear();
        for (int i = 0; i < 4; i++)
        {
            var vtaTile = Resources.Load<VisualTreeAsset>("TileTemplate");
            vtaTile.name = "tile";
            vtaTile.CloneTree(tilesTopContainer, out var index, out var _);
            
            var tile = tilesTopContainer[index].Q<VisualElement>("tile");
            tile.userData = i;
            tile.RegisterCallback<ClickEvent>(OnTileClick);
        }
        
        // // 点击tile时，显示Popup窗口， 点击窗口以外区域，关闭Popup窗口
        // var tiles = tilesTopContainer.Query<VisualElement>("tile").ToList();
        // var count = 0;
        // foreach (var tile in tiles)
        // {
        //     tile.userData = count++;
        //     tile.RegisterCallback<ClickEvent>(OnTileClick);
        // }
        
    }

    private void OnToggleAppTheme(ClickEvent evt)
    {
        var themeSettings = GetComponent<UIDocument>().panelSettings;
        var isLightTheme = themeSettings.themeStyleSheet == lightTheme;
        themeSettings.themeStyleSheet = isLightTheme ? darkTheme : lightTheme;
        Debug.Log("NOW Theme: " + (isLightTheme ? "Dark" : "Light"));
    }

    private void OnToggleMenu(ClickEvent evt)
    {
        var isHidden = menuContainer.ClassListContains(MenuHiddenClass);
        Debug.Log("Toggle Menu: " + (isHidden ? "Show" : "Hide"));
        if (isHidden)
        {
            menuContainer.RemoveFromClassList(MenuHiddenClass);
        }
        else
        {
            menuContainer.AddToClassList(MenuHiddenClass);
        }

    }
    
    private void OnTileClick(ClickEvent evt)
    {
        var tile = (VisualElement)evt.target;
        popupContainer.RemoveFromClassList(PopupHiddenClass);
        popupBackground.RemoveFromClassList(PopupBackgroundHiddenClass);
        
        Debug.Log("Show Popup: " + tile.name + " userData: " + tile.FindAncestorUserData());
    }
    
    private void OnPopupBackgroundClick(ClickEvent evt)
    {
        Debug.Log("Hide Popup");
        
        popupContainer.AddToClassList(PopupHiddenClass);
        popupBackground.AddToClassList(PopupBackgroundHiddenClass);
    }
}
