using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LineCalculator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string folderPath = @"E:\unityProject\final\VuforiaTest01\Assets\Scripts"; // 替换为您的文件夹路径
        int totalLines = 0;

        foreach (string file in Directory.GetFiles(folderPath, "*.cs", SearchOption.AllDirectories))
        {
            using (StreamReader reader = new StreamReader(file))
            {
                while (reader.ReadLine() != null)
                {
                    totalLines++;
                }
            }
        }

        Debug.Log("Total number of lines in all .cs files: " + totalLines);

        string folderPath2 = @"E:\unityProject\final\VuforiaTest01\Assets\Material"; 
        int totalLines2 = 0;

        foreach (string file in Directory.GetFiles(folderPath2, "*.shader", SearchOption.AllDirectories))
        {
            using (StreamReader reader = new StreamReader(file))
            {
                while (reader.ReadLine() != null)
                {
                    totalLines2++;
                }
            }
        }

        Debug.Log("Total number of lines in all .shader files: " + totalLines2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
