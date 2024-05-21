using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Barracuda;
using UnityEngine.UI;
using System.IO;

public class Inference : MonoBehaviour
{
    public NNModel onnxAsset;
    public Texture2D[] imageToRecognize;
    public Text message;
    private Model m_RuntimeModel;
    private IWorker worker;
    Tensor output;
    float[] outputnorm;
    float highestProbability;
    int indexWithHighestProbability;
    int indexLast = 0;

    MySocket1 mysocket;

    void Start()
    {
        m_RuntimeModel = ModelLoader.Load(onnxAsset);
        //worker = onnxAsset.CreateWorker();
        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.Auto, m_RuntimeModel);

        //inference();


        //using socket
        mysocket = this.GetComponent<MySocket1>();
    }
       
    public void inference()
    {
        foreach (var imageToRecognise in imageToRecognize)
        {
            Texture2D tmp = MyResize(imageToRecognise, 224, 224);

            using Tensor input = new Tensor(tmp, channels: 3);

            worker.Execute(input);
            output = worker.PeekOutput();

            indexWithHighestProbability = output.ArgMax()[0];

            outputnorm = Softmax(output.AsFloats());
            highestProbability = outputnorm[indexWithHighestProbability];

            if (highestProbability > 0.7)
            {
                //Debug.Log(outputnorm[0] + " " + outputnorm[1] + " " + outputnorm[2] + " " + outputnorm[3]);
                Debug.Log("预测结果：" + indexWithHighestProbability + " 可信度：" + highestProbability);

                indexLast = indexWithHighestProbability;
            }
            else
            {
                Debug.Log("预测结果：" + indexWithHighestProbability + " 可信度：" + highestProbability + "较低，放弃结果");
            }
        }
    }

    public int Recognize(Texture2D img)
    {
        Texture2D tmp = MyResize(img, 224, 224);

        //img = Rotate180(img);

        //用barracuda
        using Tensor input = new Tensor(tmp, channels: 3);

        worker.Execute(input);
        output = worker.PeekOutput();

        Debug.Log(output);

        indexWithHighestProbability = output.ArgMax()[0];

        outputnorm = Softmax(output.AsFloats());
        highestProbability = outputnorm[indexWithHighestProbability];

        if (highestProbability > 0.7)
        {
            //Debug.Log(outputnorm[0] + " " + outputnorm[1] + " " + outputnorm[2] + " " + outputnorm[3]);
            Debug.Log("预测结果：" + indexWithHighestProbability + " 可信度：" + highestProbability);

            indexLast = indexWithHighestProbability;
            return indexWithHighestProbability;
        }
        else
        {
            Debug.Log("预测结果：" + indexWithHighestProbability + " 可信度：" + highestProbability + "较低，放弃结果");
            return indexLast;
        }

        //用socket
        //Debug.Log("prepare send img");

        //mysocket.CaptureFromTexture(img);
        //Debug.Log("send img");

        //return mysocket.GetRCNNState();

    }

    void OnDisable()
    {
        worker.Dispose();
    }

    Texture2D MyResize(Texture2D texture2D, int targetX, int targetY)
    {
        RenderTexture rt = new RenderTexture(targetX, targetY, 24);
        RenderTexture.active = rt;
        Graphics.Blit(texture2D, rt);
        Texture2D result = new Texture2D(targetX, targetY);
        result.ReadPixels(new Rect(0, 0, targetX, targetY), 0, 0);
        result.Apply();
        return result;
    }

    public static float[] Softmax(float[] input)
    {
        float[] result = new float[input.Length];
        float sum = 0f;
        for (int i = 0; i < input.Length; i++)
        {
            sum += Mathf.Exp(input[i]);
        }
        for (int i = 0; i < input.Length; i++)
        {
            result[i] = Mathf.Exp(input[i]) / sum;
        }
        return result;
    }


    public static int MyMaxIndex(float[] arr)
    {
        float max = arr[0];
        int index = 0;//把假设的最大值索引赋值非index
        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] > max)
            {
                max = arr[i];
                index = i;//把较大值的索引赋值非index
            }
        }
        return index;
    }

    public Texture2D Rotate180(Texture2D original)
    {
        // Get the width and height of the original texture
        int width = original.width;
        int height = original.height;

        // Create a new Texture2D object with the same dimensions as the original
        Texture2D rotated = new Texture2D(width, height);

        // Loop through the pixels of the original texture and copy them to the new texture in a rotated order
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Get the pixel color at the current position
                Color color = original.GetPixel(x, y);

                // Calculate the new position after rotation
                int newX = width - 1 - x;
                int newY = height - 1 - y;

                // Set the pixel color at the new position
                rotated.SetPixel(newX, newY, color);
            }
        }

        // Apply the changes to the new texture
        rotated.Apply();

        // Return the new texture
        return rotated;
    }


}
