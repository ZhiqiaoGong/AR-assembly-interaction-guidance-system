using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 打印机的几种状态：
 * status0：背面，无法识别
 * status1：打开前前盖，目前没有使用
 * status2：打开上盖
 * status3：打开前盖
 * status4：什么也没打开
 * 目前需要特别检测并显示提示的是2，3状态
 */


public class ClickFunction : MonoBehaviour
{
    public static int GuideState = 1;
    public static int ModelState = 1;
    public static int RcogState = 1;

    public GameObject nextbutton;
    public GameObject lastbutton;
    public Text message;
    public GameObject pointer01;
    public GameObject front02;
    public GameObject xigu03;
    public GameObject pointer04;
    public GameObject head05;
    public GameObject paper06;

    public GameObject changehint;
    public Text changehinttext;
    public GameObject yes;
    public GameObject no;

    public GameObject myrecognize;
    MyRecognize myrecog;

    public GameObject menu;
    public bool menuopen = false;
    public GameObject buttonxigu;
    public GameObject buttonfuyin;
    public GameObject buttonqita;
    public GameObject buttonxigutext;
    public GameObject buttonfuyintext;
    public GameObject buttonqitatext;

    public GameObject tAudio;
    TestAudio myAudio;

    public GameObject tools;
    Tools myTool;

    bool foundFirst = true;

    Sprite[] sps;

    public GameObject voiceobj;
    public Text voice;

    // Start is called before the first frame update
    void Start()
    {
        changehint.SetActive(false);
        myrecog = myrecognize.GetComponent<MyRecognize>();

        myAudio = tAudio.GetComponent<TestAudio>();

        buttonxigu.SetActive(true);
        buttonfuyin.SetActive(true);
        //buttonqita.SetActive(true);
        menuopen = true;

        sps = Resources.LoadAll<Sprite>("Sprites");


    }

    // Update is called once per frame
    void Update()
    {
        if (Tools.isFound && foundFirst)
        {
            myAudio.playMusic(1);
            foundFirst = false;
        } 
    }

    public void ClickNext()
    {
        if (nextbutton.activeSelf)
        {
            if (GuideState == 1)
            {
                if (ModelState == 1)
                {
                    pointer01.SetActive(false);
                    front02.SetActive(true);
                    ModelState = 2;
                    lastbutton.SetActive(true);
                    message.text = "click next, going to open front";
                    myAudio.playMusic(2);

                    voiceobj.SetActive(false);

                }
                else if (ModelState == 2)
                {
                    front02.SetActive(false);
                    xigu03.SetActive(true);
                    message.text = "click next, going to pull xigu";
                    ModelState = 3;
                    nextbutton.SetActive(false);
                    myAudio.playMusic(3);
                }
                else
                {
                    Debug.Log("err next 1");
                }
            }

            else if (GuideState == 2)
            {
                if (ModelState == 4)
                {
                    pointer04.SetActive(false);
                    head05.SetActive(true);
                    ModelState = 5;
                    lastbutton.SetActive(true);
                    message.text = "click next, going to open head";
                    myAudio.playMusic(5);

                    voiceobj.SetActive(false);
                }
                else if (ModelState == 5)
                {
                    head05.SetActive(false);
                    paper06.SetActive(true);
                    ModelState = 6;
                    nextbutton.SetActive(false);
                    message.text = "click next, going to put paper";
                    myAudio.playMusic(6);
                }
                else
                {
                    Debug.Log("err next 2");
                }
            }
            else
            {
                Debug.Log("err next");
            }
        }
        


    }

    public void ClickLast()
    {
        if (lastbutton.activeSelf)
        {
            if (GuideState == 1)
            {
                if (ModelState == 2)
                {
                    front02.SetActive(false);
                    pointer01.SetActive(true);
                    message.text = "click last, going to click button";
                    ModelState = 1;
                    lastbutton.SetActive(false);
                    myAudio.playMusic(1);

                    voiceobj.SetActive(true);
                }
                else if (ModelState == 3)
                {
                    xigu03.SetActive(false);
                    front02.SetActive(true);
                    message.text = "click last, going to pull xigu";
                    ModelState = 2;
                    nextbutton.SetActive(true);
                    myAudio.playMusic(2);
                }
                else
                {
                    Debug.Log("err last 1");
                }
            }
            else if (GuideState == 2)
            {
                if (ModelState == 5)
                {
                    head05.SetActive(false);
                    pointer04.SetActive(true);
                    message.text = "click last, going to click button";
                    ModelState = 4;
                    lastbutton.SetActive(false);
                    myAudio.playMusic(4);

                    voiceobj.SetActive(true);
                }
                else if (ModelState == 6)
                {
                    paper06.SetActive(false);
                    head05.SetActive(true);
                    message.text = "click last, going to open head";
                    ModelState = 5;
                    nextbutton.SetActive(true);
                    myAudio.playMusic(5);
                }
                else
                {
                    Debug.Log("err last 2");
                }
            }
            else
            {
                Debug.Log("err last");
            }

        }
        


    }

    public void ShowHintChange(int rcogstate)
    {
        RcogState = rcogstate;
        string s = " ";
        if (rcogstate == 3)
        {
            s = "检测到您已经打开打印机前盖 是否进行下一步？";
            ShowYesNo();
        }
        //else if (rcogstate == 3)
        //{
        //    s = "检测到您已经拉出硒鼓 恭喜您完成引导 ！";
        //    ShowYes();
        //}
        else if(rcogstate ==  2)
        {
            s = "检测到您已经打开打印机上盖 是否进行下一步？";
            ShowYesNo();
        }

        changehinttext.text = s;
        changehint.SetActive(true);
        ChangeState.waitForReaction = true;
    }

    public void HideHintChange()
    {
        changehint.SetActive(false);
        ChangeState.waitForReaction = false;
    }

    public void HideYesNo()
    {
        yes.SetActive(false);
        no.SetActive(false);
    }
    public void ShowYesNo()
    {
        yes.SetActive(true);
        no.SetActive(true);
    }
    public void ShowYes()
    {
        yes.SetActive(true);
        no.SetActive(false);
    }

    public void ClickYes()
    {
        if (yes.activeSelf)
        {
            if (ModelState == 1)
            {
                //ModelState = 2;
                ClickNext();
            }
            else if (ModelState == 2)
            {
                //ModelState = 3;
                ClickNext();
            }
            else if (ModelState == 3)
            {
                //HideHintChange();
                //Invoke("WaitForSeconds", 3.0f);
            }
            else if (ModelState == 5)
            {
                ClickNext();
            }
            HideHintChange();
            Invoke("WaitForSeconds", 5.0f);
            ChangeState.waitForReaction = false;
            HideHintChange();
        }
        
    }

    public void ClickNo()
    {
        if (no.activeSelf)
        {
            HideHintChange();
            Invoke("WaitForSeconds", 3.0f);
            ChangeState.waitForReaction = false;
            myrecog.SetSetState();
            Invoke(nameof(ResetinMyRecog), 2.0f);
        }
        
    }

    private void ResetinMyRecog()
    {
        myrecog.ResetSetState();
    }

    public void WaitForSeconds()
    {
        Debug.Log("Wait3");
    }

    public void MenuFun()
    {
        if (menuopen)
        {
            buttonxigu.SetActive(false);
            buttonfuyin.SetActive(false);
            //buttonqita.SetActive(false);
            menuopen = false;

            menu.GetComponent<Image>().sprite = sps[8];
        }
        else
        {
            buttonxigu.SetActive(true);
            buttonfuyin.SetActive(true);
            //buttonqita.SetActive(true); 
            menuopen = true;

            menu.GetComponent<Image>().sprite = sps[9];

        }
    }

    public void XiguFun()
    {
        if(GuideState != 1)
        {
            ChangeGuideState(1);

            buttonxigu.GetComponent<Image>().sprite = sps[1];
            buttonxigutext.GetComponent<Image>().sprite = sps[7];

            buttonfuyin.GetComponent<Image>().sprite = sps[2];
            buttonfuyintext.GetComponent<Image>().sprite = sps[6];
            //buttonqita.GetComponent<Image>().sprite = sps[4];
            //buttonqitatext.GetComponent<Image>().sprite = sps[6];
        }
        
    }

    public void FuyinFun()
    {
        if(GuideState != 2)
        {
            ChangeGuideState(2);

            buttonfuyin.GetComponent<Image>().sprite = sps[3];
            buttonfuyintext.GetComponent<Image>().sprite = sps[7];

            buttonxigu.GetComponent<Image>().sprite = sps[0];
            buttonxigutext.GetComponent<Image>().sprite = sps[6];
            //buttonqita.GetComponent<Image>().sprite = sps[4];
            //buttonqitatext.GetComponent<Image>().sprite = sps[6];
        }

        
    }

    public void QitaFun()
    {
        if(GuideState != 3)
        {
            ChangeGuideState(3);

            //buttonqita.GetComponent<Image>().sprite = sps[5];
            //buttonqitatext.GetComponent<Image>().sprite = sps[7];

            buttonxigu.GetComponent<Image>().sprite = sps[0];
            buttonxigutext.GetComponent<Image>().sprite = sps[6];
            buttonfuyin.GetComponent<Image>().sprite = sps[2];
            buttonfuyintext.GetComponent<Image>().sprite = sps[6];
        }
    }

    public void ChangeGuideState(int state)
    {
        if (state == 1)
        {
            GuideState = 1;
            ModelState = 1;

            pointer01.SetActive(true);
            front02.SetActive(false);
            xigu03.SetActive(false);
            pointer04.SetActive(false);
            head05.SetActive(false);
            paper06.SetActive(false);

            lastbutton.SetActive(false);
            nextbutton.SetActive(true);

            myAudio.playMusic(1);
        }
        else if (state == 2)
        {
            GuideState = 2;
            ModelState = 4;

            pointer01.SetActive(false);
            front02.SetActive(false);
            xigu03.SetActive(false);
            pointer04.SetActive(true);
            head05.SetActive(false);
            paper06.SetActive(false);

            lastbutton.SetActive(false);
            nextbutton.SetActive(true);

            myAudio.playMusic(4);
        }
        else if(state == 3)
        {

        }
    }
}
