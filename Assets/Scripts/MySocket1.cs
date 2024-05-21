using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;



public class MySocket1 : MonoBehaviour
{
    [SerializeField]
    Camera mCam;
    [SerializeField]
    int width = 1080;
    [SerializeField]
    int height = 720;
    [SerializeField]
    private string _serverIP = "192.168.35.106";//服务器ip地址
    [SerializeField]
    private int _serverPort = 8005;//端口号
    [SerializeField]
    private string _state = "0";//状态



    private Socket ws;
    private Rect CutRect = new Rect(0, 0, 1, 1);
    bool flag = true;

    Coroutine rec;


    void Start()
    {
        Connect();
    }

    //尝试连接服务器
    void Connect()
    {
        ws = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        ws.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, 1);
        // Connect to server 
        IPAddress iPAddress = IPAddress.Parse(_serverIP);
        EndPoint endPoint = new IPEndPoint(iPAddress, _serverPort);
        ws.Connect(endPoint);
        //InvokeRepeating("Capture", 1, 0.2f);
        //InvokeRepeating("recevive", 1, 0.2f);
        rec = StartCoroutine(recevive());
    }

    string temp = "";
    public IEnumerator recevive()
    {
        while (true)
        {
            //接收服务器的消息
            byte[] buffer = new byte[1024];
            int length = ws.Receive(buffer);
            string message = Encoding.UTF8.GetString(buffer, 0, length);
            message = temp + message;
            temp = "";

            //分包
            string[] messages = message.Split('\n');
            Debug.Log("客户端接收到服务器消息：" + message);

            //处理分包
            //bool flag = false;
            for (int i = 0; i < messages.Length; i++)
            {
                //判断messages[i]长度是否为100
                if (messages[i].Length != 100 && messages[i].Length != 0)
                {
                    if (i == messages.Length - 1 && messages[i].Length < 100)
                    {
                        temp = messages[i];
                    }
                    else
                    {
                        Debug.LogError("错误数据报");
                        Debug.LogError(messages[i]);
                    }
                }

                //判断是否能够视为整数
                try
                {
                    int.Parse(messages[i]);
                }
                catch (System.FormatException)
                {
                    continue;
                }

                // 读取整数 如果非负数 则为状态码
                int state = int.Parse(messages[i]);
                if (state >= 0)
                {
                    if (_state != state.ToString())
                    {
                        _state = state.ToString();
                        Debug.Log("客户端接收到服务器状态更新：" + state);
                    }
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
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
        // Debug.Log("客户端向服务器发送图片");
    }

    public void CaptureFromTexture(Texture2D imgtex)
    {
        byte[] bytes = imgtex.EncodeToJPG();
        ws.Send(bytes);
        Debug.Log("客户端向服务器发送从材质获得的图片");
    }


    //允许用户输入服务器ip地址和端口号
    void OnGUI()
    {
        ////在界面中显示state
        //GUI.Label(new Rect(10, 70, 100, 20), "State:" + _state);
        //if (flag)
        //{
        //    _serverIP = GUI.TextField(new Rect(10, 10, 100, 20), _serverIP);
        //    _serverPort = int.Parse(GUI.TextField(new Rect(10, 30, 100, 20), _serverPort.ToString()));
        //    // 如果用户修改了ip地址和端口号，那么重新连接服务器
        //    if (GUI.Button(new Rect(10, 50, 100, 20), "Connect"))
        //    {
        //        Connect();
        //    }
        //}
    }

    public int GetRCNNState()
    {
        try
        {
            return int.Parse(_state);

        }
        catch (Exception e)
        {
            Debug.Log("parse field");
        }
        return -1;
    }

    private void OnDestroy()
    {
        StopCoroutine(rec);
    }
}
