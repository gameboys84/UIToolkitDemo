using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class CustomVisualElement : VisualElement
{
    private Label _label;
    private Button _button;
    
    // private string _text;

    // 如果不定义CreateProperty， 自定义属性就无法实现 DataBind
    [UxmlAttribute, CreateProperty]
    public string Text
    {
        get => _label.text;
        set
        {
            _label.text = value;
            // _label.MarkDirtyRepaint();
        }
    }

    public CustomVisualElement()
    {
        _label = new Label();
        
        _button = new Button();
        _button.text = "Refresh";
        _button.clicked += () =>
        {
            Debug.Log($"Button clicked: {_label.text}");
        };
        
        style.flexDirection = FlexDirection.Row;
        style.justifyContent = Justify.SpaceBetween;
        Add(_label);
        Add(_button);
    }
    
    
}
