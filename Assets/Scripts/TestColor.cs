//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class TestColor : MonoBehaviour
//{
//    // Start is called before the first frame update

//    MeshRenderer render;
//    float deltime1 = 0;
//    float deltime2 = 0;
//    Color mytrans = new Color(0 / 255f, 0 / 255f, 0 / 255f, 0 / 255f);
//    Color myblue = new Color(106 / 255f, 209 / 255f, 201 / 255f, 255 / 255f);

//    //Color color;
//    private void Start()
//    {
//        //事先直接获取color进行修改，不会对物体属性产生影响
//        //color = transform.GetComponent<MeshRenderer>().material.color;

//        //应获取物体本身属性，对属性进行修改
//        render = this.GetComponent<MeshRenderer>();
//    }

//    private void Update()
//    {
//        if (deltime1 <= 1.0f)
//        {
//            deltime1 += Time.deltaTime;
//            Debug.Log(deltime1);

//            //color = Color.Lerp( Color.red,Color.white,deltime/20.0f);
//            render.material.color = Color.Lerp(mytrans, myblue, deltime1 / 1.0f);
//        }
//        else if (deltime2 <= 3.0f)
//        {
//            deltime2 += Time.deltaTime;
//            Debug.Log(deltime2);

//            //color = Color.Lerp( Color.red,Color.white,deltime/20.0f);
//            render.material.color = Color.Lerp(myblue, mytrans, deltime2 / 4.0f);
//        }
//    }

//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestColor : MonoBehaviour
{
    // Start is called before the first frame update
    MeshRenderer render;
    Color mytrans = new Color(0 / 255f, 0 / 255f, 0 / 255f, 0 / 255f);
    Color myblue = new Color(106 / 255f, 209 / 255f, 201 / 255f, 255 / 255f);

    private void Start()
    {
        //render = this.GetComponent<MeshRenderer>();
        //StartCoroutine(LerpColor(render, mytrans, myblue, 3.0f, 2)); //开始协程，渐变三次
    }

    public IEnumerator LerpColor(MeshRenderer mesh, Color fromColor, Color toColor, float duration, int repetitions)
    {
        for (int i = 0; i < repetitions; i++) //重复次数
        {
            float counter = 0;
            while (counter < duration) //渐变时间
            {
                counter += Time.deltaTime;
                float colorTime = counter / duration;
                Debug.Log(colorTime + "i");
                //Change color
                mesh.material.color = Color.Lerp(fromColor, toColor, colorTime); //使用colorTime而不是counter/duration
                                                                                 //Wait for a frame
                yield return null;
            }
            //交换颜色
            Color tmp = fromColor;
            fromColor = toColor;
            toColor = tmp;
            //Wait for a delay
            yield return new WaitForSeconds(2f); //等待一秒
        }
        yield return null;

    }

    public void LerpColor_ni(MeshRenderer mesh, int nummesh, Color fromColor, Color toColor, float duration, int repetitions)
    {
        Debug.Log("LerpColor_ni"+mesh.name);
        for (int i = 0; i < repetitions; i++) //重复次数
        {
            float counter = 0;
            while (counter < duration) //渐变时间
            {
                counter += Time.deltaTime;
                float colorTime = counter / duration;
                Debug.Log(colorTime + "ni");
                //Change color
                mesh.material.color = Color.Lerp(fromColor, toColor, colorTime); //使用colorTime而不是counter/duration
                                                                                 //Wait for a frame
            }
            //交换颜色
            Color tmp = fromColor;
            fromColor = toColor;
            toColor = tmp;
            //Wait for a delay
            Invoke(nameof(WaitForSec1), 1.0f)
;        }
        ChooseModel.ing[nummesh] = false;
    }

    void WaitForSec1()
    {
        Debug.Log("WaitForSec1");
    }

    public void ForceStop(MeshRenderer mesh)
    {
        mesh.material.color = mytrans;
        Debug.Log("ForceStop" + mesh.name);
    }
}
