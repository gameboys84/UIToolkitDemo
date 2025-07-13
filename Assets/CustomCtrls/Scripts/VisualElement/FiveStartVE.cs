using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class FiveStartVE : VisualElement
{
    private VisualElement onStar;
    private VisualElement offStar;

    public FiveStartVE()
    {
        // onStar = this.Q<VisualElement>("ForeStar");
        // offStar = this.Q<VisualElement>("BackStar");
        
        onStar = new VisualElement
        {
            name = "onStar",
        };

        offStar = new VisualElement
        {
            name = "offStar"
        };
        
        AddToClassList("five-stars");
        onStar.AddToClassList("on-stars");
        offStar.AddToClassList("off-stars");
        
        Add(onStar);
        Add(offStar);
    }

    private int _maxStars = 5;

    [UxmlAttribute, CreateProperty, Range(1, 6)]
    public int MaxStars
    {
        get => _maxStars;
        set
        {
            _maxStars = value;
            this.MarkDirtyRepaint();
        }
    }

    private int _halfStars_on; // 激活的半星数量, 0-12
    [UxmlAttribute, CreateProperty, Range(0, 12)]
    public int HalfStars
    {
        get => _halfStars_on;
        set
        {
            _halfStars_on = value;

            var onStarPercent = Mathf.Min(0.5f * _halfStars_on / _maxStars, 1);
            var offStarPercent = 1 - onStarPercent;

            onStar.style.width = new Length(onStarPercent * 200, LengthUnit.Percent);
            offStar.style.width = new Length(offStarPercent * 200, LengthUnit.Percent);
            
            // 半星的背景大小， backgroundSize越大，repeat的次数就越少，也就是半星越少， 反之， backgroundSize越小，repeat的次数就越多，也就是半星越多。
            // 当满星时, 为了显示满 _maxStars 个星，每个星占的比例 backgroundSize = 2 / _maxStars(分子的2是因为onStar和offStar各为100%大小， 半星数量 _halfStars_on = _maxStars * 2
            // 当只有一个半星时, 每个星占的比例 backgroundSize = 2 / _halfStars_on
            onStar.style.backgroundSize = new BackgroundSize(
                _halfStars_on > 0 ? new Length(200f / Mathf.Min(_halfStars_on, _maxStars * 2), LengthUnit.Percent) : 0,
                new Length(100, LengthUnit.Percent));
            
            offStar.style.backgroundSize = new BackgroundSize(
                _halfStars_on < (_maxStars * 2) ? new Length(200f /(_maxStars * 2 - _halfStars_on), LengthUnit.Percent) : 0,
                new Length(100, LengthUnit.Percent));
        }
    }
    
}
