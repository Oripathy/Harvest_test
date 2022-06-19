Shader "Unlit/Wheat Shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _AlphaCutOut ("Alpha CutOut", Range(0, 1)) = 0
        _Color ("Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Cull off
        Tags { "RenderType"="TransparencyCutout" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                fixed4 diff : COLOR0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _AlphaCutOut;
            float4 _Color;

            v2f vert (appdata_base v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                half3 worldNormal = UnityObjectToWorldNormal(v.normal);
                half n = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
                o.diff = n * _LightColor0;
                o.diff.rgb += ShadeSH9(half4(worldNormal, 1));
                //o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv.x += (sin(_Time.y * o.uv.y * 0.5) + 0.5) * 0.5;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                col *= _Color * i.diff * 0.8;
                
                if (col.a < _AlphaCutOut)
                    discard;
                
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
