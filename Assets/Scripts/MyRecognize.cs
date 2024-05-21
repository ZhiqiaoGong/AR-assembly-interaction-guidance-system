using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

//获取相机texture 用于打印机状态识别
public class MyRecognize : MonoBehaviour
{
    public float timer = 2.0f; // 定时2秒
    public int framecount = 0; //序列计数
    public Camera arcamera;
    Inference ifer;
    Material m;
    GameObject video;
    Texture t;
    public bool StopSetState = false;
    public Texture2D VideoBackground_Texture2d;

    private void Start()
    {
        ifer = gameObject.GetComponent<Inference>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            try
            {
                video = arcamera.transform.Find("VideoBackground").gameObject;
                //Debug.Log("find object/");

                if (video != null && video.activeSelf)
                {
                    //Debug.Log("object active");
                    m = video.GetComponent<MeshRenderer>().material;
                    
                    
                    if (m != null)
                    {
                        //Debug.Log("find material");
                        t = m.mainTexture;
                        VideoBackground_Texture2d = t as Texture2D;
                        //byte[] bytes = texture2d.EncodeToPNG();
                        //File.WriteAllBytes(Application.dataPath + "/" + framecount + ".png", bytes);

                        if (!StopSetState)
                        {
                            ChangeState.SetState(ifer.Recognize(VideoBackground_Texture2d));

                        }
                    }
                    else
                    {
                        //Debug.Log("not find material");
                    }
                }
                
                
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }

            timer = 2.0f;
            framecount++;
        }

    }

    public void SetSetState()
    {
        StopSetState = true;
    }

    public void ResetSetState()
    {
        StopSetState = false;
    }

}