using UnityEngine;
using UnityEngine.UI;

//����һ��UI������࣬���̳���MonoBehaviour
public class UIAdapter : MonoBehaviour
{

    public RectTransform uiElement;

    //Ŀ��ֱ���
    public Vector2 targetResolution = new Vector2(1920, 1080);

    //�������ӵı��������ݵ�ǰ��Ļ�ֱ��ʺ�Ŀ��ֱ���������
    private float scaleFactor;

    //ê��
    public Anchor anchor = Anchor.MiddleCenter;

    //ê���ö������
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

    //��̬�����ê��ı���
    public DynamicAnchor dynamicAnchor = DynamicAnchor.Left;

    //��̬�����ê���ö�����ͣ���������ѡ��
    public enum DynamicAnchor
    {
        Left,
        Right,
        Top,
        Bottom
    }

    //ԭʼ��ɫ
    public Color originalColor = Color.white;

    //Ŀ����ɫ
    public Color targetColor = Color.red;

    //�л�״̬����¼��ť�Ƿ񱻵����
    private bool isToggled = false;


    private void Start()
    {
        ////��ȡ��ǰ��Ļ�ֱ���
        //Vector2 currentResolution = new Vector2(Screen.width, Screen.height);

        ////�����������ӣ����ǵ�ǰ�ֱ��ʺ�Ŀ��ֱ��ʵı�ֵ�н�С���Ǹ�
        //scaleFactor = Mathf.Min(currentResolution.x / targetResolution.x, currentResolution.y / targetResolution.y);

        ////�������䷽��
        //Adapt();

        ////���ö��뷽��
        //Align();
    }

    //����һ�����µķ�����������ÿһ֡����һ��
    private void Update()
    {
        ////���ö�̬���䷽��
        //DynamicAdapt();
    }

    //����һ������ķ��������������������������UIԪ�صĴ�С��λ��
    private void Adapt()
    {
        //��ȡUIԪ�ص�ԭʼ��С��λ��
        Vector2 originalSize = uiElement.sizeDelta;
        Vector2 originalPosition = uiElement.anchoredPosition;

        //�������������������µĴ�С��λ��
        Vector2 newSize = originalSize * scaleFactor;
        Vector2 newPosition = originalPosition * scaleFactor;

        //����UIԪ�ص��´�С��λ��
        uiElement.sizeDelta = newSize;
        uiElement.anchoredPosition = newPosition;
    }

    //����һ������ķ������������ê��������UIԪ��
    private void Align()
    {
        //��ȡ��ǰ��Ļ�ֱ��ʺ�Ŀ��ֱ��ʵĲ�ֵ
        Vector2 deltaResolution = new Vector2(Screen.width - targetResolution.x * scaleFactor, Screen.height - targetResolution.y * scaleFactor);

        //����ê���ֵ������ƫ����
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

        //��ȡUIԪ�صĵ�ǰλ��
        Vector2 currentPosition = uiElement.anchoredPosition;

        //����ƫ�����������µ�λ��
        Vector2 newPosition = currentPosition + offset;

        //����UIԪ�ص���λ��
        uiElement.anchoredPosition = newPosition;
    }

    //����һ����̬����ķ��������������Ļ�Ĵ�С�仯����̬����UIԪ�صĴ�С��λ��
    private void DynamicAdapt()
    {
        //��ȡ��ǰ��Ļ�ֱ��ʺ�Ŀ��ֱ��ʵı�ֵ
        float ratioX = Screen.width / targetResolution.x;
        float ratioY = Screen.height / targetResolution.y;

        //���ݶ�̬�����ê���ֵ��������������
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

        //��ȡUIԪ�ص�ԭʼ��С��λ��
        Vector2 originalSize = uiElement.sizeDelta;
        Vector2 originalPosition = uiElement.anchoredPosition;

        //�����������ӺͶ�̬���������������µĴ�С��λ��
        Vector2 newSize = originalSize * scaleFactor * dynamicScaleFactor;
        Vector2 newPosition = originalPosition * scaleFactor * dynamicScaleFactor;

        //����UIԪ�ص��´�С��λ��
        uiElement.sizeDelta = newSize;
        uiElement.anchoredPosition = newPosition;
    }

    //����һ���л���ɫ�ķ���������ʵ�ְ�ť�ڵ�����ɫ���ٴε������ԭ������ɫ
    public void ToggleColor()
    {
        //��ȡ��ť��Image���
        Image image = uiElement.GetComponent<Image>();

        //�����ťû�б���������Ͱ���ɫ����ΪĿ����ɫ�������л�״̬��Ϊ��
        if (!isToggled)
        {
            image.color = targetColor;
            isToggled = true;
        }
        //�����ť�Ѿ�����������Ͱ���ɫ����Ϊԭʼ��ɫ�������л�״̬��Ϊ��
        else
        {
            image.color = originalColor;
            isToggled = false;
        }
    }
}