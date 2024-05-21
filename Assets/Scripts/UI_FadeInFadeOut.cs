using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// UI的渐入渐出
/// </summary>
public class UI_FadeInFadeOut : MonoBehaviour
{
    private float UI_Alpha = 1;             //初始化时让UI显示
    public float alphaSpeed = 0;          //渐隐渐显的速度
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
        canvasGroup.blocksRaycasts = true;      //可以和该对象交互
        Debug.Log("fade in");
    }

    public void UI_FadeOut_Event(float speed)
    {
        alphaSpeed = speed;
        UI_Alpha = 0;
        canvasGroup.blocksRaycasts = false;     //不可以和该对象交互
        Debug.Log("fade out");
    }

    public void ShowNew()
    {
        UI_Alpha = 0;
        canvasGroup.alpha = 0;
    }

    // 一个方法来改变canvas group的透明度
    public void ChangeAlpha(float alpha)
    {
        // 限制alpha值在0到1之间
        alpha = Mathf.Clamp01(alpha);
        // 设置canvas group的alpha属性
        canvasGroup.alpha = alpha;
    }

    // 一个方法来改变canvas group的交互性
    public void ChangeInteractable(bool interactable)
    {
        // 设置canvas group的interactable属性
        canvasGroup.interactable = interactable;
    }

    // 一个方法来改变canvas group的射线检测
    public void ChangeBlockRaycasts(bool blockRaycasts)
    {
        // 设置canvas group的blockRaycasts属性
        canvasGroup.blocksRaycasts = blockRaycasts;
    }
}