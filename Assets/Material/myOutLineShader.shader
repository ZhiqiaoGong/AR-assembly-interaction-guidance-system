// A complete version of the previous shader
Shader "Custom/Outline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineWidth ("Outline Width", Float) = 1.0
        _OutlineColor ("Outline Color", Color) = (1,0,0,1)
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 100

        // Pass to draw the object normally
        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float depth : TEXCOORD1; // Added this to pass the depth value
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.depth = o.vertex.z; // Added this to calculate the depth value
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // Apply the alpha of the texture to the color
                col.a *= col.a;
                return col;
            }
            ENDCG
        }

        // Pass to draw the outline
        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float depth : TEXCOORD1; // Added this to pass the depth value
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _OutlineWidth;
            fixed4 _OutlineColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.depth = o.vertex.z; // Added this to calculate the depth value
                o.screenPos = ComputeScreenPos(o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Calculate the screen space offset based on the outline width and the screen resolution
                float2 offset = _OutlineWidth * 2.0 / i.screenPos.w * _ScreenParams.xy;

                // Sample the texture four times with the offset and compare the depth values with the current pixel's depth value
                float depth1 = tex2D(_MainTex, i.uv + offset * float2(-1,-1)).a * tex2D(_CameraDepthTexture, i.uv + offset * float2(-1,-1)).r;
                float depth2 = tex2D(_MainTex, i.uv + offset * float2( 1,-1)).a * tex2D(_CameraDepthTexture, i.uv + offset * float2( 1,-1)).r;
                float depth3 = tex2D(_MainTex, i.uv + offset * float2(-1, 1)).a * tex2D(_CameraDepthTexture, i.uv + offset * float2(-1, 1)).r;
                float depth4 = tex2D(_MainTex, i.uv + offset * float2( 1, 1)).a * tex2D(_CameraDepthTexture, i.uv + offset * float2( 1, 1)).r;
                                // If any of the samples has a larger depth difference than a threshold, draw the outline color
                if (abs(depth1 - i.depth) > 0.01 ||
                    abs(depth2 - i.depth) > 0.01 ||
                    abs(depth3 - i.depth) > 0.01 ||
                    abs(depth4 - i.depth) > 0.01)
                {
                    return _OutlineColor;
                }
                
                // Otherwise, discard the pixel
                clip(-1);
                
                return fixed4(0,0,0,0);
                
             }
             ENDCG
         }
     }
 }