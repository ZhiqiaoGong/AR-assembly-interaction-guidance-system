using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//���ʹ��windows������ʹ����һ����
//using UnityEngine.Windows.Speech;//���������ռ�  ����

/// <summary>
/// ����ʶ����Ҫ�Ǳ�ؼ��֣�
/// </summary>
public class speechKey : MonoBehaviour
{
    //// ����ʶ����
    //private PhraseRecognizer m_PhraseRecognizer;
    //// �ؼ���
    //public string[] keywords = { "��һ��", "��һ��", "��", "��", "��ӡ��������", "�������Ĳ�������" };
    ////public string[] keywords = { "��"};

    //// ���Ŷ�
    //public ConfidenceLevel m_confidenceLevel = ConfidenceLevel.Low;

    public GameObject canvas;
    ClickFunction clickFunction;

    // Use this for initialization
    void Start()
    {
        //if (m_PhraseRecognizer == null)
        //{
        //    //����һ��ʶ����
        //    m_PhraseRecognizer = new KeywordRecognizer(keywords, m_confidenceLevel);
        //    //ͨ��ע������ķ���
        //    m_PhraseRecognizer.OnPhraseRecognized += M_PhraseRecognizer_OnPhraseRecognized;
        //    //����ʶ����
        //    m_PhraseRecognizer.Start();

        //    //Debug.Log("����ʶ�����ɹ�");
        //}

        clickFunction = canvas.GetComponent<ClickFunction>();

    }

    /// <summary>
    ///  ��ʶ�𵽹ؼ���ʱ��������������
    /// </summary>
    /// <param name="args"></param>
    //private void M_PhraseRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    //{
    //    SpeechRecognition(args.text);
    //    Debug.Log(args.text);
    //}
    private void OnDestroy()
    {
        ////�жϳ������Ƿ��������ʶ����������У��ͷ�
        //if (m_PhraseRecognizer != null)
        //{
        //    //����Ӧ���ͷţ�������������Ŀ���
        //    m_PhraseRecognizer.Dispose();
        //}

    }
    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// ʶ�������Ĳ���
    /// </summary>
    void SpeechRecognition(string args)
    {
        if (args.Equals("��һ��"))
        {
            clickFunction.ClickNext();
        }
        else if (args.Equals("��һ��"))
        {
            clickFunction.ClickLast();
        }
        else if (args.Equals("��"))
        {
            clickFunction.ClickYes();
        }
        else if (args.Equals("��"))
        {
            clickFunction.ClickNo();
        }
        else if (args.Equals("��ӡ��������"))
        {
            clickFunction.FuyinFun();
        }
        else if (args.Equals("�������Ĳ�������"))
        {
            clickFunction.XiguFun();
        }
    }
}