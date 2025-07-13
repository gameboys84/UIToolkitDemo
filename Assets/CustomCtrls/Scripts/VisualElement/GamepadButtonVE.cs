using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[UxmlElement]
public partial class GamepadButtonVE : Button
{
    [UxmlAttribute]
    private InputActionReference _actionReference;

    public GamepadButtonVE()
    {
        RegisterCallback<AttachToPanelEvent>(OnAttachedToPanel);
        RegisterCallback<DetachFromPanelEvent>(OnDetachedFromPanel);
    }

    private void OnAttachedToPanel(AttachToPanelEvent evt)
    {
        var icon = InputSprites.Instance.GetSprite(_actionReference);

        iconImage = Background.FromSprite(icon);
        text = _actionReference?.action.name;
        
        if (_actionReference)
        {
            _actionReference.action.performed += OnActionPerformed;
        }
    }

    private void OnDetachedFromPanel(DetachFromPanelEvent evt)
    {
        iconImage = null;

        if (_actionReference)
        {
            _actionReference.action.performed -= OnActionPerformed;
        }
    }
    

    private void OnActionPerformed(InputAction.CallbackContext obj)
    {
        SendEvent(new NavigationSubmitEvent { target = this });
    }
}
