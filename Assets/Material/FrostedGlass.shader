Shader "Custom/Depth" {  
    Properties
    {
        //贴图入口
        _MainTex ("Texture", 2D) = "white" {}
        _AlphaValue("_AlphaValue",Range(0.01,1)) = 1
        //线段颜色
        _Color("Color",COLOR) = (0,1,0,1)
    }
    SubShader {  
        // 关闭深度写入 会解决一些模型破面的问题
        ZWrite Off
        Cull Off
        Blend SrcAlpha OneMinusSrcAlpha
 
        // 透明队列
        Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent"}
 
        Pass{  
            CGPROGRAM  
            // 定义顶点、片元渲染器，并引入shader相关的宏
            #pragma vertex vert  
            #pragma fragment frag  
            #include "UnityCG.cginc"  
            sampler2D _MainTex;
            fixed4 _Color;
            float _AlphaValue;
            // 片元渲染器的输入结构 
            struct v2f {  
                // 像素的坐标、视口方向、世界法线
                float4 pos : SV_POSITION;  
                float3 viewDir:TEXCOORD0;
                float3 worldNormal:NORMAL;
                float2 uv:TEXCOORD1;
            };  
              
            //顶点渲染器
            v2f vert (appdata_base v){  
                v2f o;  
                // 将顶点坐标模型空间转为裁剪空间
                o.pos = UnityObjectToClipPos (v.vertex);  
                // 将法线从模型空间转为世界空间
                o.worldNormal =  UnityObjectToWorldNormal(v.normal);
                //先把顶点转为世界空间
                float4 worldPos = mul(unity_ObjectToWorld, v.vertex);
                //获取从摄像机 到 世界顶点坐标 的方向
                o.viewDir = normalize(UnityWorldSpaceViewDir(worldPos));
                o.uv = v.texcoord;
               return o;  
            }  
              
            //片元渲染器
            half4 frag (v2f i) : COLOR{  
                half4 a = tex2D(_MainTex, i.uv);
                //计算 世界空间中的法线 与 世界中的相机到像素坐标的方向的 角度
                half normalAngle = 1 - abs(dot(i.worldNormal, i.viewDir));
                if(normalAngle < -0.9){
                    normalAngle = -0.9;}
                else if(normalAngle > 0.9){
                    normalAngle =  0.9;}
                else{                
                       
                }
                return _Color * normalAngle*a;
            }  
            ENDCG  
        }  
    }  
}  