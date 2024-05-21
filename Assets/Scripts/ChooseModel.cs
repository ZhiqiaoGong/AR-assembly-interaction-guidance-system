using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseModel : MonoBehaviour
{
    public GameObject front, head, button, back;
    MeshRenderer frontmr, headmr, buttonmr, backmr;
    Coroutine frontcr, headcr, buttoncr, backcr;
    public static bool []ing;
    //public static bool heading = false;
    //public static bool buttoning = false;
    //public static bool backing = false;

    public GameObject hintobj;
    public Transform hint;
    public Text hinttext;
    public string collideobj, tmpcollide;
    public GameObject fade;
    UI_FadeInFadeOut uifade;
    TestColor testColor;

    Color mytrans = new Color(0 / 255f, 0 / 255f, 0 / 255f, 0 / 255f);
    Color myblue = new Color(106 / 255f, 209 / 255f, 201 / 255f, 255 / 255f);


    // Start is called before the first frame update
    void Start()
    {
        uifade = this.GetComponent<UI_FadeInFadeOut>();
        IEnumerator enumerator = CorFun();
        // ���ص�Coroutine���󱣴�����������ֹͣЭ��
        Coroutine coroutine = StartCoroutine(enumerator);

        testColor = this.GetComponent<TestColor>();
        frontmr = front.GetComponent<MeshRenderer>();
        headmr = head.GetComponent<MeshRenderer>();
        buttonmr = button.GetComponent<MeshRenderer>();
        backmr = back.GetComponent<MeshRenderer>();

        ing = new bool[4];
        ing[0] = false;
        ing[1] = false;
        ing[2] = false;
        ing[3] = false;
    }

    // Update is called once per frame
    void Update()
    {
        //SetCollideHint();
        /*if (MousePick() != null)
        {
            collideobj = MousePick();
            hinttext.text = "����"+ collideobj;
            Vector2 pos = Input.mousePosition;
            Debug.Log("screen w:" + Screen.width + ", h:" + Screen.height);
            Debug.Log("click pos x:" + pos.x + ",pos y:" + pos.y);

            float X = Input.mousePosition.x - Screen.width / 2f;
            float Y = Input.mousePosition.y - Screen.height / 2f;
            Vector2 tranPos = new Vector2(X, Y);
            hint.localPosition = tranPos;

            //�õ������ĳߴ�
            Vector2 uisize = this.GetComponent<RectTransform>().sizeDelta;
            Debug.Log("sizeDelta w:" + uisize.x + ", h:" + uisize.y);

            Vector2 finalPos = new Vector2(X * (uisize.x / Screen.width), Y * (uisize.y / Screen.height));
            hint.localPosition = finalPos;
            hintobj.SetActive(true);
        }*/
    }

    IEnumerator CorFun()
    {
        while (hintobj.activeSelf)
        {
            SetCollideHint();
            yield return null;

        }
        
    }


    string MobilePick()
    {
        if (Input.touchCount != 1)
            return null;

        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.name);
                //Debug.Log(hit.transform.tag);
                return hit.transform.name;

            }
        }
        return null;
    }

    string MousePick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("hit"+hit.transform.name);
                //Debug.Log(hit.transform.tag);
                return hit.transform.name;

            }
        }
        return null;
    }

    void SetCollideHint()
    {
        collideobj = "";
        if (MousePick() != null)
        {
            uifade.ShowNew();

            tmpcollide = MousePick();
            if(tmpcollide.Equals("�ϸ�") || tmpcollide.Equals("ǰ��") || tmpcollide.Equals("���") || tmpcollide.Equals("��ť"))
            {
                collideobj = tmpcollide;
                hinttext.text = "����" + collideobj;
                Vector2 pos = Input.mousePosition;
                //Debug.Log("screen w:" + Screen.width + ", h:" + Screen.height);
                //Debug.Log("click pos x:" + pos.x + ",pos y:" + pos.y);

                float X = Input.mousePosition.x - Screen.width / 2f;
                float Y = Input.mousePosition.y - Screen.height / 2f;
                Vector2 tranPos = new Vector2(X, Y);
                hint.localPosition = tranPos;

                //�õ������ĳߴ�
                Vector2 uisize = this.GetComponent<RectTransform>().sizeDelta;
                //Debug.Log("sizeDelta w:" + uisize.x + ", h:" + uisize.y);

                Vector2 finalPos = new Vector2(X * (uisize.x / Screen.width), Y * (uisize.y / Screen.height));
                hint.localPosition = finalPos;

                //hintobj.SetActive(true);
                uifade.UI_FadeIn_Event(1.0f);

                Invoke(nameof(HideCollideHint), 2.0f);
            }
            
        }
        else if(MobilePick() != null)
        {
            uifade.ShowNew();

            tmpcollide = MobilePick();
            if (tmpcollide.Equals("�ϸ�") || tmpcollide.Equals("ǰ��") || tmpcollide.Equals("���") || tmpcollide.Equals("��ť"))
            {
                collideobj = tmpcollide;
                hinttext.text = "����" + collideobj;
                Vector2 pos = Input.GetTouch(0).position;
                Debug.Log("screen w:" + Screen.width + ", h:" + Screen.height);
                Debug.Log("click pos x:" + pos.x + ",pos y:" + pos.y);

                float X = Input.mousePosition.x - Screen.width / 2f;
                float Y = Input.mousePosition.y - Screen.height / 2f;
                Vector2 tranPos = new Vector2(X, Y);
                hint.localPosition = tranPos;

                //�õ������ĳߴ�
                Vector2 uisize = this.GetComponent<RectTransform>().sizeDelta;
                Debug.Log("sizeDelta w:" + uisize.x + ", h:" + uisize.y);

                Vector2 finalPos = new Vector2(X * (uisize.x / Screen.width), Y * (uisize.y / Screen.height));
                hint.localPosition = finalPos;

                uifade.UI_FadeIn_Event(1.0f);

                Invoke("HideCollideHint", 2.0f);

                //hintobj.SetActive(true);
            }
            
        }
        //uifade.UI_FadeIn_Event();
        //Invoke("HideCollideHint", 3.0f);

        if (collideobj.Equals("ǰ��"))
        {
            //ֱ�����ÿ����ر�����
            //Debug.Log("ǰ��");
            //front.SetActive(true);
            //Invoke("HideCollideHint", 2.0f);
            //front.SetActive(false);

            //Э��
            try
            {
                StopCoroutine(frontcr);
                //StopCoroutine(headcr);
                //StopCoroutine(buttoncr);
                //StopCoroutine(backcr);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            frontcr = StartCoroutine(testColor.LerpColor(frontmr, mytrans, myblue, 1.0f, 2));

            ////��Э��
            //if (ing[0])
            //{
            //    testColor.ForceStop(frontmr);
            //}
            //testColor.LerpColor_ni(frontmr, 0, mytrans, myblue, 2.0f, 2);
            //ing[0] = true;
        }
        else if (collideobj.Equals("�ϸ�"))
        {
            //ֱ�����ÿ����ر�����
            //Debug.Log("�ϸ�");
            //head.SetActive(true);
            //Invoke("HideCollideHint", 2.0f);
            //head.SetActive(false);

            //Э��
            try
            {
                //StopCoroutine(frontcr);
                StopCoroutine(headcr);
                //StopCoroutine(buttoncr);
                //StopCoroutine(backcr);
            }
            catch (Exception e)
            {
                Debug.LogError(e);

            }
            headcr = StartCoroutine(testColor.LerpColor(headmr, mytrans, myblue, 1.0f, 2));

            ////��Э��
            //if (ing[1])
            //{
            //    testColor.ForceStop(headmr);
            //}
            //testColor.LerpColor_ni(headmr, 1, mytrans, myblue, 2.0f, 2);
            //ing[1] = true;
        }
        else if (collideobj.Equals("��ť"))
        {
            //ֱ�����ÿ����ر�����
            //Debug.Log("��ť");
            //button.SetActive(true);
            //Invoke("HideCollideHint", 2.0f);
            //button.SetActive(false);

            //Э��
            try
            {
                //StopCoroutine(frontcr);
                //StopCoroutine(headcr);
                StopCoroutine(buttoncr);
                //StopCoroutine(backcr);
            }
            catch (Exception e)
            {
                Debug.LogError(e);

            }
            buttoncr = StartCoroutine(testColor.LerpColor(buttonmr, mytrans, myblue, 1.0f, 2));

            ////��Э��
            //if (ing[2])
            //{
            //    testColor.ForceStop(buttonmr);
            //}
            //testColor.LerpColor_ni(buttonmr, 2, mytrans, myblue, 2.0f, 2);
            //ing[2] = true;
        }
        else if (collideobj.Equals("���"))
        {
            //ֱ�����ÿ����ر�����
            //Debug.Log("���");
            //back.SetActive(true);
            //Invoke("HideCollideHint", 2.0f);
            //back.SetActive(false);

            //Э��
            try
            {
                //StopCoroutine(frontcr);
                //StopCoroutine(headcr);
                //StopCoroutine(buttoncr);
                StopCoroutine(backcr);
            }
            catch (Exception e)
            {
                Debug.LogError(e);

            }
            backcr = StartCoroutine(testColor.LerpColor(backmr, mytrans, myblue, 1.0f, 2));

            ////��Э��
            //if (ing[3])
            //{
            //    testColor.ForceStop(backmr);
            //}
            //testColor.LerpColor_ni(backmr, 3, mytrans, myblue, 2.0f, 2);
            //ing[3] = true;
        }



    }

    void HideCollideHint()
    {
        uifade.UI_FadeOut_Event(2.0f);
        //hintobj.SetActive(false);
        //if (collideobj.Equals("ǰ��"))
        //{
        //    Debug.Log("����ǰ��");
        //    front.SetActive(false);
        //    //Invoke("HideCollideHint", 2.0f);
        //}
        //else if (collideobj.Equals("�ϸ�"))
        //{
        //    Debug.Log("�����ϸ�");
        //    head.SetActive(false);
        //    //Invoke("HideCollideHint", 2.0f);
        //}
        //else if (collideobj.Equals("��ť"))
        //{
        //    Debug.Log("���ذ�ť");
        //    button.SetActive(false);
        //    //Invoke("HideCollideHint", 2.0f);
        //}
        //else if (collideobj.Equals("���"))
        //{
        //    Debug.Log("���غ��");
        //    back.SetActive(false);
        //    //Invoke("HideCollideHint", 2.0f);
        //}
    }
}
