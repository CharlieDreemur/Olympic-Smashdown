Shader "Unlit/TransitionShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Radius ("Radius", Float) = 0.0
        _CenterX ("CenterX", Float) = 0.0
        _CenterY ("CenterY", Float) = 0.0
        _RefScreenSizeX ("Reference Screen Size X", Float) = 1920.0
        _RefScreenSizeY ("Reference Screen Size Y", Float) = 1080.0
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        LOD 100
     
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha 

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Radius;
            float _CenterX;
            float _CenterY;
            float _RefScreenSizeX;
            float _RefScreenSizeY;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = fixed4(0.0, 0.0, 0.0, 1.0);
                float2 diff = float2(_CenterX - i.uv.x * _RefScreenSizeX, _CenterY - i.uv.y * _RefScreenSizeY);
                float dist2 = diff.x * diff.x + diff.y * diff.y;
                if (dist2 < _Radius * _Radius)
                {
                    col = fixed4(0.0, 0.0, 0.0, 0.0);
                }
                return col;
            }
            ENDCG
        }
    }
}
