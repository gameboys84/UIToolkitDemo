using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class DrawVE : VisualElement
{
    // 自定义样式属性
    private static readonly CustomStyleProperty<Color> SadColorId = new CustomStyleProperty<Color>("--sad-color");
    private static readonly CustomStyleProperty<Color> HappyColorId = new CustomStyleProperty<Color>("--happy-color");
    
    private Color _sadColor = Color.magenta;
    private Color _happyColor = Color.magenta;
    
    private float _happiness;
    
    [UxmlAttribute, CreateProperty, Range(0, 1)]
    public float Happiness
    {
        get => _happiness;
        set
        {
            _happiness = value;
            MarkDirtyRepaint();
        }
    }

    public DrawVE()
    {
        AddToClassList("draw-ve");
        
        RegisterCallback<CustomStyleResolvedEvent>(OnCustomStyleResolved);
        generateVisualContent += OnGenerateVisualContent;
    }

    private void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
    {
        this.customStyle.TryGetValue(SadColorId, out _sadColor);
        this.customStyle.TryGetValue(HappyColorId, out _happyColor);
        
        MarkDirtyRepaint();
    }

    private void OnGenerateVisualContent(MeshGenerationContext ctx)
    {
        var halfHeight = contentRect.height / 2f;
        var halfWidth = contentRect.width / 2f;
        
        ctx.painter2D.BeginPath();
        ctx.painter2D.strokeColor = Color.Lerp(_sadColor, _happyColor, _happiness);
        ctx.painter2D.MoveTo(new Vector2(0, halfHeight));
        ctx.painter2D.QuadraticCurveTo(
            new Vector2(halfWidth, contentRect.height * _happiness * 2),
            new Vector2(contentRect.width, halfHeight));
        ctx.painter2D.Stroke();
    }
}
