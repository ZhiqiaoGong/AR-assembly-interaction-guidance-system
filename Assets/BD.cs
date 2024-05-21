using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BD : MonoBehaviour
{
    AndroidJavaObject m_AndroidPluginObj;
    AndroidJavaClass _androidJC;
    AndroidJavaObject m_Android;
    public Text mRecognRes;
    public Button startASR_Btn;
    public Button stopASR_Btn;
    public GameObject mesg;
    Text message;

    void Start()
    {
        message = mesg.GetComponent<Text>();

        AndroidJavaClass _androidJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        if (_androidJC == null)
        {
            message.text = "JNI initialization failure.";
            return;
        }
        m_AndroidPluginObj = _androidJC.GetStatic<AndroidJavaObject>("currentActivity");
        startASR_Btn.onClick.AddListener(StartRecogn);
        stopASR_Btn.onClick.AddListener(StopRecogn);

        Invoke("InitAsr", 3);
    }

    public void InitAsr()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.gzq.baidu.CientBaiDuVoiceMainActivity");//包名加类名
        AndroidJavaObject m_Android = jc.CallStatic<AndroidJavaObject>("getInstance");
        if (m_Android != null)
        {
            m_Android.Call("InitRecogn", m_AndroidPluginObj);
        }
        else
            message.text = "AndroidPlugin is Null";
    }
    public void StartRecogn()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.example.baidu.CientBaiDuVoiceMainActivity");
        AndroidJavaObject m_Android = jc.CallStatic<AndroidJavaObject>("getInstance");
        if (m_Android != null)
        {
            m_Android.Call("StartRecogn");
        }
        else
            message.text = "AndroidPlugin is Null";
    }
    public void StopRecogn()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.gzq.baidu.CientBaiDuVoiceMainActivity");
        AndroidJavaObject m_Android = jc.CallStatic<AndroidJavaObject>("getInstance");
        if (m_Android != null)
        {
            m_Android.Call("StopRecogn");
        }
        else
            message.text = "AndroidPlugin is Null";
    }

    /// <summary>
    /// 百度语音识别结果反馈
    /// </summary>
    /// <param name="res"></param>
    void RecognResult(string res)
    {
        string[] ress = res.Split('&');
        JsonData jsonData = JsonMapper.ToObject(ress[1]);
        string resStr = "";
        if (jsonData["result_type"].ToString() == "partial_result")
        {
            resStr = "临时识别结果:";
        }
        else
        {
            resStr = "最终识别结果:";
        }

        resStr += jsonData["best_result"].ToString();
        mRecognRes.text = resStr;
    }
}
