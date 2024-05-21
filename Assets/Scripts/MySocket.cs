using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;



public class MySocket : MonoBehaviour
{
    [SerializeField]
    Camera mCam;
    [SerializeField]
    int width = 1080;
    [SerializeField]
    int height = 720;
    [SerializeField]
    private string _serverIP = "127.0.0.1";//������ip��ַ
    [SerializeField]
    private int _serverPort = 8003;//�˿ں�
    [SerializeField]
    private string _state = "0";//״̬

    //MyRecognize myrecgnize;



    private Socket ws;
    private Rect CutRect = new Rect(0, 0, 1, 1);
    bool flag = true;


    void Start()
    {
        Connect();
        //myrecgnize = this.GetComponent<MyRecognize>();
    }

    //�������ӷ�����
    void Connect()
    {
        ws = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        // Connect to server 
        IPAddress iPAddress = IPAddress.Parse(_serverIP);
        EndPoint endPoint = new IPEndPoint(iPAddress, _serverPort);
        ws.Connect(endPoint);
        //InvokeRepeating("Capture", 1, 0.2f);
        InvokeRepeating("receive", 1, 0.2f);
    }

    public void receive()
    {
        //���շ���������Ϣ
        byte[] buffer = new byte[1024];
        int length = ws.Receive(buffer);
        string message = Encoding.UTF8.GetString(buffer, 0, length);

        //���ݷ���������Ϣ���ı�state��ֵ
        _state = message;
        
    }

    public void Capture()
    {
        RenderTexture rt = new RenderTexture(width, height, 2);
        mCam.pixelRect = new Rect(0, 0, Screen.width, Screen.height);
        mCam.targetTexture = rt;
        Texture2D screenShot = new Texture2D((int)(width * CutRect.width), (int)(height * CutRect.height),
                                                 TextureFormat.RGB24, false);
        mCam.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(width * CutRect.x, width * CutRect.y, width * CutRect.width, height * CutRect.height), 0, 0);
        mCam.targetTexture = null;
        RenderTexture.active = null;
        UnityEngine.Object.Destroy(rt);
        byte[] bytes = screenShot.EncodeToJPG();
        //string filename = Application.dataPath + "/Imgs/Img"
          //                  + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
        //System.IO.File.WriteAllBytes(filename, bytes);
        ws.Send(bytes);
        Debug.Log("�ͻ��������������ͼƬ");
    }

    public void CaptureFromTexture(Texture2D imgtex)
    {
        byte[] bytes = imgtex.EncodeToJPG();
        ws.Send(bytes);
        Debug.Log("�ͻ�������������ʹӲ��ʻ�õ�ͼƬ");
    }

    //�����û����������ip��ַ�Ͷ˿ں�
    void OnGUI()
    {
        //�ڽ�������ʾstate
        GUI.Label(new Rect(10, 70, 100, 20), "State:" + _state);
        if (flag)
        {
            _serverIP = GUI.TextField(new Rect(10, 10, 100, 20), _serverIP);
            _serverPort = int.Parse(GUI.TextField(new Rect(10, 30, 100, 20), _serverPort.ToString()));
            // ����û��޸���ip��ַ�Ͷ˿ںţ���ô�������ӷ�����
            if (GUI.Button(new Rect(10, 50, 100, 20), "Connect"))
            {
                Connect();
            }
        }
    }

    public int GetRCNNState()
    {
        try
        {
            return int.Parse(_state);

        }catch(Exception e)
        {
            Debug.Log("parse field");
        }
        return -1;
    }
}
