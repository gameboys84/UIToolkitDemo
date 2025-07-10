using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UICubeRotate : MonoBehaviour
{
    [SerializeField] private Transform target;
    public float speed = 30f;
    private bool isRotating = false;
    private bool isOffsetting = false;
    private bool isScale = false;
    private bool isZoomIn = false;
    private bool isColor = false;
    
    private UIDocument uiDocument;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();

        if (!target)
        {
            var meshRenderer = GameObject.FindObjectOfType<MeshRenderer>();
            if (meshRenderer!= null)
            {
                target = meshRenderer.transform;
            }
        }
        
        if (!target)
        {
            // Debug.LogError("Target is not assigned.");
            target = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        }
    }

    private void Start()
    {
        // 还可以验证某个节点是否存在，Validation是自己实现的，这里只是输出一个Warning.
        // var RotateButton = uiDocument.rootVisualElement.Q<Button>("Rotate");
        // Validation.CheckQuery(RotateButton, "Rotate");
        
        // Q和Query主要差别在于，Q是简易查询，找到第1个满足条件的就返回， Query可以用于更复杂的查询
        
        uiDocument.rootVisualElement.Q<Button>("Rotate").RegisterCallback<ClickEvent>(OnClickRotate);
        uiDocument.rootVisualElement.Q<Button>("Offset").RegisterCallback<ClickEvent>(OnClickOffset);
        uiDocument.rootVisualElement.Q<Button>("Stop").RegisterCallback<ClickEvent>(OnClickStop);
        uiDocument.rootVisualElement.Q<Button>("Reset").RegisterCallback<ClickEvent>(OnClickReset);
        
        uiDocument.rootVisualElement.Q<Toggle>("Scale").RegisterCallback<ClickEvent>(OnClickScale);
        uiDocument.rootVisualElement.Q<Toggle>("Color").RegisterCallback<ClickEvent>(OnClickColor);
    }

    private void OnClickReset(ClickEvent evt)
    {
        isRotating = false;
        isOffsetting = false;
        isScale = false;
        isZoomIn = false;
        isColor = false;
        target.localPosition = Vector3.zero;
        target.localRotation = Quaternion.identity;
        target.localScale = Vector3.one;
        target.GetComponent<Renderer>().material.color = Color.white;
    }

    private void OnClickStop(ClickEvent evt)
    {
        isRotating = false;
        isOffsetting = false;
    }

    private void OnClickOffset(ClickEvent evt)
    {
        isOffsetting = !isOffsetting;
    }

    private void OnClickRotate(ClickEvent evt)
    {
        isRotating = !isRotating;
    }
    
    private void OnClickColor(ClickEvent evt)
    {
        isColor = !isColor;
    }

    private void OnClickScale(ClickEvent evt)
    {
        isScale = !isScale;
    }
    
    private void Update()
    {
        if (isRotating)
        {
            target.Rotate(new Vector3(1, 1, 0) * (Time.deltaTime * speed));
        }
        
        if (isOffsetting)
        {
            target.localPosition += Vector3.right * (Time.deltaTime * speed * 0.01f);
        }
        
        if (isScale)
        {
            if (target.localScale.x >= 2f)
            {
                isZoomIn = true;
            }
            else if (target.localScale.x <= 0.2f)
            {
                isZoomIn = false;
            }
            
            if (isZoomIn)
            {
                target.localScale -= Vector3.one * (Time.deltaTime * speed * 0.01f);
            }
            else
            {
                target.localScale += Vector3.one * (Time.deltaTime * speed * 0.01f);
            }
        }

        if (isColor)
        {
            var colorR = Mathf.PingPong(Time.time * speed * 0.01f, 1f);
            var colorG = Mathf.PingPong(Time.time * speed * 0.01f + 0.33f, 1f);
            var colorB = Mathf.PingPong(Time.time * speed * 0.01f + 0.66f, 1f);
            target.GetComponent<Renderer>().material.color = new Color(colorR, colorG, colorB);
        }
    }
}
