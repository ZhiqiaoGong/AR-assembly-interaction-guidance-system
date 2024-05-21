using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// UI�Ľ��뽥��
/// </summary>
public class UI_FadeInFadeOut : MonoBehaviour
{
    private float UI_Alpha = 1;             //��ʼ��ʱ��UI��ʾ
    public float alphaSpeed = 0;          //�������Ե��ٶ�
    private CanvasGroup canvasGroup;
    public GameObject buttonhint;

    // Use this for initialization
    void Start()
    {
        canvasGroup = buttonhint.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canvasGroup == null)
        {
            Debug.Log("null canvasgroup");
            return;
        }

        if (UI_Alpha != canvasGroup.alpha)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, UI_Alpha, alphaSpeed * Time.deltaTime);
            if (Mathf.Abs(UI_Alpha - canvasGroup.alpha) <= 0.01f)
            {
                canvasGroup.alpha = UI_Alpha;
            }
            //Debug.Log("update alpha");
        }
    }

    public void UI_FadeIn_Event(float speed)
    {
        alphaSpeed = speed;
        UI_Alpha = 1;
        canvasGroup.blocksRaycasts = true;      //���Ժ͸ö��󽻻�
        Debug.Log("fade in");
    }

    public void UI_FadeOut_Event(float speed)
    {
        alphaSpeed = speed;
        UI_Alpha = 0;
        canvasGroup.blocksRaycasts = false;     //�����Ժ͸ö��󽻻�
        Debug.Log("fade out");
    }

    public void ShowNew()
    {
        UI_Alpha = 0;
        canvasGroup.alpha = 0;
    }

    // һ���������ı�canvas group��͸����
    public void ChangeAlpha(float alpha)
    {
        // ����alphaֵ��0��1֮��
        alpha = Mathf.Clamp01(alpha);
        // ����canvas group��alpha����
        canvasGroup.alpha = alpha;
    }

    // һ���������ı�canvas group�Ľ�����
    public void ChangeInteractable(bool interactable)
    {
        // ����canvas group��interactable����
        canvasGroup.interactable = interactable;
    }

    // һ���������ı�canvas group�����߼��
    public void ChangeBlockRaycasts(bool blockRaycasts)
    {
        // ����canvas group��blockRaycasts����
        canvasGroup.blocksRaycasts = blockRaycasts;
    }
}