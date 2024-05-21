using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//如果使用windows，可以使用这一部分
//using UnityEngine.Windows.Speech;//引入命名空间  利用

/// <summary>
/// 语音识别（主要是别关键字）
/// </summary>
public class speechKey : MonoBehaviour
{
    //// 短语识别器
    //private PhraseRecognizer m_PhraseRecognizer;
    //// 关键字
    //public string[] keywords = { "上一步", "下一步", "是", "否", "复印操作引导", "更换硒鼓操作引导" };
    ////public string[] keywords = { "是"};

    //// 可信度
    //public ConfidenceLevel m_confidenceLevel = ConfidenceLevel.Low;

    public GameObject canvas;
    ClickFunction clickFunction;

    // Use this for initialization
    void Start()
    {
        //if (m_PhraseRecognizer == null)
        //{
        //    //创建一个识别器
        //    m_PhraseRecognizer = new KeywordRecognizer(keywords, m_confidenceLevel);
        //    //通过注册监听的方法
        //    m_PhraseRecognizer.OnPhraseRecognized += M_PhraseRecognizer_OnPhraseRecognized;
        //    //开启识别器
        //    m_PhraseRecognizer.Start();

        //    //Debug.Log("创建识别器成功");
        //}

        clickFunction = canvas.GetComponent<ClickFunction>();

    }

    /// <summary>
    ///  当识别到关键字时，会调用这个方法
    /// </summary>
    /// <param name="args"></param>
    //private void M_PhraseRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    //{
    //    SpeechRecognition(args.text);
    //    Debug.Log(args.text);
    //}
    private void OnDestroy()
    {
        ////判断场景中是否存在语音识别器，如果有，释放
        //if (m_PhraseRecognizer != null)
        //{
        //    //用完应该释放，否则会带来额外的开销
        //    m_PhraseRecognizer.Dispose();
        //}

    }
    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// 识别到语音的操作
    /// </summary>
    void SpeechRecognition(string args)
    {
        if (args.Equals("下一步"))
        {
            clickFunction.ClickNext();
        }
        else if (args.Equals("上一步"))
        {
            clickFunction.ClickLast();
        }
        else if (args.Equals("是"))
        {
            clickFunction.ClickYes();
        }
        else if (args.Equals("否"))
        {
            clickFunction.ClickNo();
        }
        else if (args.Equals("复印操作引导"))
        {
            clickFunction.FuyinFun();
        }
        else if (args.Equals("更换硒鼓操作引导"))
        {
            clickFunction.XiguFun();
        }
    }
}