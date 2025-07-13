using UnityEngine;

[CreateAssetMenu(fileName = "DataBinding", menuName = "Scriptable Objects/DataBinding")]
public class DataBinding : ScriptableObject
{
    public string Description;
    [Range(1, 6)]
    public int MaxStars;
    [Range(0, 12)]
    public int HalfStars;
    [Range(0, 1)]
    public float Happiness;
}
