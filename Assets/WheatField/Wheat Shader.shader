Shader "Unlit/Wheat Shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _AlphaCutOut ("Alpha CutOut", Range(0, 1)) = 0
        _Color ("Color", Color) = (1, 1, 1, 1)
        _UVCut ("UV Cut", Range(0.1, 1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="TransparencyCutout" }
        LOD 200
        Cull off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog
            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"
  
            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
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
            float _UVCut;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                // o.uv = v.texcoord;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                half3 worldNormal = UnityObjectToWorldNormal(v.normal);
                half n = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
                o.diff = n * _LightColor0;
                o.diff.rgb += ShadeSH9(half4(worldNormal, 1));
                
                o.uv.x += sin(_Time.y * o.uv.y * 3 * worldNormal.x) * 0.3;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                col *= _Color * i.diff * 0.7;
                
                if (col.a < _AlphaCutOut)
                    discard;

                if (i.uv.y > _UVCut)
                    discard;
                
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
