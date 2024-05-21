Shader "Custom/RandomColorWave"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Speed ("Speed", Range(0,10)) = 1
        _Scale ("Scale", Range(0,10)) = 1
        _Intensity ("Intensity", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        float _Speed;
        float _Scale;
        float _Intensity;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = 0.0;
            o.Smoothness = 0.5;
            o.Alpha = c.a;
            // Add some random color wave effect based on world position and time
            float f = frac(sin(dot(IN.worldPos.xyz + _Time * _Speed, float3(12.9898, 78.233, 98.7654))) * 43758.5453);
            fixed4 color = fixed4(f,f,f,1);
            o.Albedo += color * _Intensity * sin(IN.worldPos.x * _Scale + _Time * _Speed);
        }
        ENDCG
    }
    FallBack "Diffuse"
}