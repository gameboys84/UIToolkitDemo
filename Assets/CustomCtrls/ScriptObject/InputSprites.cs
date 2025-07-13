using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputSprites", menuName = "Scriptable Objects/InputSprites")]
public class InputSprites : ScriptableObject
{
    private static InputSprites _instance;

    public static InputSprites Instance
    {
        get
        {
            if (_instance)
                return _instance;

            //TODO: Replace with Addressables
            _instance = Resources.Load<InputSprites>("InputSprites");
            return _instance;
        }
    }

    [Serializable]
    public class ReferenceToSprite
    {
        public InputActionReference reference;
        public Sprite sprite;
    }

    public ReferenceToSprite[] Mappings;

    public Sprite GetSprite(InputActionReference reference)
    {
        return Mappings.FirstOrDefault(m => m.reference == reference)?.sprite;
    }
}
