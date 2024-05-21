using UnityEngine;
using UnityEngine.UI;

//这是一个UI适配的类，它继承了MonoBehaviour
public class UIAdapter : MonoBehaviour
{

    public RectTransform uiElement;

    //目标分辨率
    public Vector2 targetResolution = new Vector2(1920, 1080);

    //缩放因子的变量，根据当前屏幕分辨率和目标分辨率来计算
    private float scaleFactor;

    //锚点
    public Anchor anchor = Anchor.MiddleCenter;

    //锚点的枚举类型
    public enum Anchor
    {
        TopLeft,
        TopCenter,
        TopRight,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }

    //动态适配的锚点的变量
    public DynamicAnchor dynamicAnchor = DynamicAnchor.Left;

    //动态适配的锚点的枚举类型，它有四种选项
    public enum DynamicAnchor
    {
        Left,
        Right,
        Top,
        Bottom
    }

    //原始颜色
    public Color originalColor = Color.white;

    //目标颜色
    public Color targetColor = Color.red;

    //切换状态，记录按钮是否被点击过
    private bool isToggled = false;


    private void Start()
    {
        ////获取当前屏幕分辨率
        //Vector2 currentResolution = new Vector2(Screen.width, Screen.height);

        ////计算缩放因子，它是当前分辨率和目标分辨率的比值中较小的那个
        //scaleFactor = Mathf.Min(currentResolution.x / targetResolution.x, currentResolution.y / targetResolution.y);

        ////调用适配方法
        //Adapt();

        ////调用对齐方法
        //Align();
    }

    //这是一个更新的方法，它会在每一帧调用一次
    private void Update()
    {
        ////调用动态适配方法
        //DynamicAdapt();
    }

    //这是一个适配的方法，它会根据缩放因子来调整UI元素的大小和位置
    private void Adapt()
    {
        //获取UI元素的原始大小和位置
        Vector2 originalSize = uiElement.sizeDelta;
        Vector2 originalPosition = uiElement.anchoredPosition;

        //根据缩放因子来计算新的大小和位置
        Vector2 newSize = originalSize * scaleFactor;
        Vector2 newPosition = originalPosition * scaleFactor;

        //设置UI元素的新大小和位置
        uiElement.sizeDelta = newSize;
        uiElement.anchoredPosition = newPosition;
    }

    //这是一个对齐的方法，它会根据锚点来对齐UI元素
    private void Align()
    {
        //获取当前屏幕分辨率和目标分辨率的差值
        Vector2 deltaResolution = new Vector2(Screen.width - targetResolution.x * scaleFactor, Screen.height - targetResolution.y * scaleFactor);

        //根据锚点的值来计算偏移量
        Vector2 offset = Vector2.zero;
        switch (anchor)
        {
            case Anchor.TopLeft:
                offset = new Vector2(0, -deltaResolution.y);
                break;
            case Anchor.TopCenter:
                offset = new Vector2(deltaResolution.x / 2, -deltaResolution.y);
                break;
            case Anchor.TopRight:
                offset = new Vector2(deltaResolution.x, -deltaResolution.y);
                break;
            case Anchor.MiddleLeft:
                offset = new Vector2(0, -deltaResolution.y / 2);
                break;
            case Anchor.MiddleCenter:
                offset = new Vector2(deltaResolution.x / 2, -deltaResolution.y / 2);
                break;
            case Anchor.MiddleRight:
                offset = new Vector2(deltaResolution.x, -deltaResolution.y / 2);
                break;
            case Anchor.BottomLeft:
                offset = new Vector2(0, 0);
                break;
            case Anchor.BottomCenter:
                offset = new Vector2(deltaResolution.x / 2, 0);
                break;
            case Anchor.BottomRight:
                offset = new Vector2(deltaResolution.x, 0);
                break;
        }

        //获取UI元素的当前位置
        Vector2 currentPosition = uiElement.anchoredPosition;

        //根据偏移量来计算新的位置
        Vector2 newPosition = currentPosition + offset;

        //设置UI元素的新位置
        uiElement.anchoredPosition = newPosition;
    }

    //这是一个动态适配的方法，它会根据屏幕的大小变化来动态调整UI元素的大小和位置
    private void DynamicAdapt()
    {
        //获取当前屏幕分辨率和目标分辨率的比值
        float ratioX = Screen.width / targetResolution.x;
        float ratioY = Screen.height / targetResolution.y;

        //根据动态适配的锚点的值来计算缩放因子
        float dynamicScaleFactor = 1f;
        switch (dynamicAnchor)
        {
            case DynamicAnchor.Left:
            case DynamicAnchor.Right:
                dynamicScaleFactor = ratioX;
                break;
            case DynamicAnchor.Top:
            case DynamicAnchor.Bottom:
                dynamicScaleFactor = ratioY;
                break;
        }

        //获取UI元素的原始大小和位置
        Vector2 originalSize = uiElement.sizeDelta;
        Vector2 originalPosition = uiElement.anchoredPosition;

        //根据缩放因子和动态缩放因子来计算新的大小和位置
        Vector2 newSize = originalSize * scaleFactor * dynamicScaleFactor;
        Vector2 newPosition = originalPosition * scaleFactor * dynamicScaleFactor;

        //设置UI元素的新大小和位置
        uiElement.sizeDelta = newSize;
        uiElement.anchoredPosition = newPosition;
    }

    //这是一个切换颜色的方法，它会实现按钮在点击后变色，再次点击后变成原来的颜色
    public void ToggleColor()
    {
        //获取按钮的Image组件
        Image image = uiElement.GetComponent<Image>();

        //如果按钮没有被点击过，就把颜色设置为目标颜色，并把切换状态设为真
        if (!isToggled)
        {
            image.color = targetColor;
            isToggled = true;
        }
        //如果按钮已经被点击过，就把颜色设置为原始颜色，并把切换状态设为假
        else
        {
            image.color = originalColor;
            isToggled = false;
        }
    }
}