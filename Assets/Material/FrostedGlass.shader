Shader "Custom/Depth" {  
    Properties
    {
        //��ͼ���
        _MainTex ("Texture", 2D) = "white" {}
        _AlphaValue("_AlphaValue",Range(0.01,1)) = 1
        //�߶���ɫ
        _Color("Color",COLOR) = (0,1,0,1)
    }
    SubShader {  
        // �ر����д�� ����һЩģ�����������
        ZWrite Off
        Cull Off
        Blend SrcAlpha OneMinusSrcAlpha
 
        // ͸������
        Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent"}
 
        Pass{  
            CGPROGRAM  
            // ���嶥�㡢ƬԪ��Ⱦ����������shader��صĺ�
            #pragma vertex vert  
            #pragma fragment frag  
            #include "UnityCG.cginc"  
            sampler2D _MainTex;
            fixed4 _Color;
            float _AlphaValue;
            // ƬԪ��Ⱦ��������ṹ 
            struct v2f {  
                // ���ص����ꡢ�ӿڷ������編��
                float4 pos : SV_POSITION;  
                float3 viewDir:TEXCOORD0;
                float3 worldNormal:NORMAL;
                float2 uv:TEXCOORD1;
            };  
              
            //������Ⱦ��
            v2f vert (appdata_base v){  
                v2f o;  
                // ����������ģ�Ϳռ�תΪ�ü��ռ�
                o.pos = UnityObjectToClipPos (v.vertex);  
                // �����ߴ�ģ�Ϳռ�תΪ����ռ�
                o.worldNormal =  UnityObjectToWorldNormal(v.normal);
                //�ȰѶ���תΪ����ռ�
                float4 worldPos = mul(unity_ObjectToWorld, v.vertex);
                //��ȡ������� �� ���綥������ �ķ���
                o.viewDir = normalize(UnityWorldSpaceViewDir(worldPos));
                o.uv = v.texcoord;
               return o;  
            }  
              
            //ƬԪ��Ⱦ��
            half4 frag (v2f i) : COLOR{  
                half4 a = tex2D(_MainTex, i.uv);
                //���� ����ռ��еķ��� �� �����е��������������ķ���� �Ƕ�
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