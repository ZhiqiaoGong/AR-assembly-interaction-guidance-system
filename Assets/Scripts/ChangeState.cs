using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeState : MonoBehaviour
{
    bool isFirst = true;
    public static int preRState = 1;
    public static int RState = 1;
    public int modelState;
    public Text message;
    public GameObject canvas;
    ClickFunction hintf;
    public static bool waitForReaction = false;

    public float timer = 2.0f; // ��ʱ2��


    // Start is called before the first frame update
    void Start()
    {
        hintf = canvas.GetComponent<ClickFunction>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            modelState = ClickFunction.ModelState;

            if (!waitForReaction)
            {
                //�ѵ���������İ�ť��׼����ǰ��
                if (RState == 3 && modelState == 2)
                {
                    hintf.ShowHintChange(RState);
                    Debug.Log("1 recognize change state!!!!!!!!!!!!!");
                }
                //�޷������������
                //else if (RState == 3 && modelState == 3)
                //{
                //    hintf.ShowHintChange(RState);
                //    Debug.Log("2 recognize change state!!!!!!!!!!!!!");
                //}
                //�Ѿ������ӡ��ť��׼���򿪴�ӡ���ϸ�
                else if(RState == 2 && modelState == 5)
                {
                    hintf.ShowHintChange(RState);
                    Debug.Log("3 recognize change state!!!!!!!!!!!!!");
                }
                //if( RState == modelState + 1)
                //{            
                //    if(preRState == modelState)
                //    {
                //        hintf.ShowHintChange(RState);
                //        message.text = "1 recognize change state!!!!!!!!!!!!!";
                //    }
                //    else if(preRState == RState)
                //    {
                //        hintf.ShowHintChange(RState);
                //        message.text = "2 recognize change state!!!!!!!!!!!!!";
                //    }

                //}

                message.text = RState.ToString();

            }
            else
            {
                Debug.Log("wait");
            }
            timer = 2.0f;
        }
    }

    public static void SetState(int s)
    {
        if (s > 0 && s < 5)
        {
            preRState = RState;
            RState = s - 1;
            Debug.Log("SetState" + RState);
        }

    }

}
