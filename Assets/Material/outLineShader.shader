Shader "Custom/Test0"
{
    Properties
    {
        _MainColor("����ɫ", Color) = (0, 0, 0, 1)


        _HaloColor("�������ɫ", Color) = (1, 0, 0, 0.5)
        _HaloArea("����η�Χ", Range(0, 2)) = 1
        _HaloPow("����εȼ�", Range(0, 3)) = 1
        _HaloStrength("�����ǿ��", Range(0, 4)) = 1
    }
    SubShader
    {
        // ��һ��Passʵ���ڷ���, ����˵��Ե����
        Pass
        {
            Tags
            {
                "LightMode" = "ForwardBase"
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            # include "UnityCG.cginc"
            # include "UnityLightingCommon.cginc"
            fixed4 _MainColor


            struct a2v
            {
                float4 vertex: POSITION
                float3 normal: NORMAL
            }

            struct v2f
            {
                float4 pos: SV_POSITION
                float3 worldNormal: COLOR
            }

            v2f vert(a2v v)
            {
                v2f o

                o.pos = UnityObjectToClipPos(v.vertex)

                o.worldNormal = UnityObjectToWorldNormal(v.normal)

                return o
            }

            fixed4 frag(v2f i): SV_Target
            {
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.rgb

                fixed3 worldNormal = normalize(i.worldNormal)

                fixed3 worldLight = normalize(_WorldSpaceLightPos0.xyz)

                fixed3 diffuse = _LightColor0.rgb * _MainColor.rgb *
                (dot(worldLight, worldNormal) * 0.5 + 0.5)
                return fixed4(ambient + diffuse, 1)
            }
            ENDCG
        }
        Pass
        {
            // �޳����棬��ֹ���Ե��סԭģ�ͣ����������еĶ���Ҫ�޳����棬��������ʲô��
            // �Ǹ���Ҫ�������ģ��ʲô�ģ���ʱ��ûѧ��
            Cull Front
            // ������ڿ������Եǿ��
            Blend SrcAlpha OneMinusSrcAlpha
            // ZWrite off  ����������Ե���Ӻ������������ʱ���ܻ������͸���߽������
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            # include "UnityCG.cginc"
            fixed4 _HaloColor
            fixed _HaloPow
            fixed _HaloArea
            fixed _HaloStrength


            struct a2v
            {
                float4 vertex: POSITION
                float3 normal: NORMAL
            }

            struct v2f
            {
                float4 pos: SV_POSITION
                float3 normal: TEXCOORD0
                float4 worldPos: TEXCOORD1
            }


            v2f vert(a2v v)
            {
                v2f o
                o.normal = UnityObjectToWorldNormal(v.normal)
                v.vertex.xyz += v.normal * _HaloArea
                o.pos = UnityObjectToClipPos(v.vertex)
                o.worldPos = mul(unity_ObjectToWorld, v.vertex)
                return o
            }

            fixed4 frag(v2f i): SV_Target
            {
                i.normal = normalize(i.normal)
                // ��Passһ��һ����ԭ��������Ҫ�ѱ��浱������Ⱦ�������ӽǷ���Ҫ�෴
                float3 viewDir = normalize(i.worldPos.xyz - _WorldSpaceCameraPos.xyz)
                // ��Ե��Ȼʹ�÷������жϣ�ֻ�Ǻͼ�ⷽʽ�ڱ�Ե��ͬ
                float fresnel = pow(saturate(dot(i.normal, viewDir)), _HaloPow) * _HaloStrength
                return fixed4(_HaloColor.rgb, fresnel)
            }
            ENDCG
        }
    }
}
