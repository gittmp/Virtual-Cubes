Shader "Shaders/cubes"
{
    Properties
    {
        [NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Pass
        {
            Tags {"LightMode"="ForwardBase"}
        
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"

            sampler2D _MainTex;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                fixed4 diff : COLOR0;
                float4 screenPos : TEXCOORD1;
            };

            v2f vert (appdata v, float3 normal : NORMAL)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                half3 norm = UnityObjectToWorldNormal(normal);
                o.diff = max(0, dot(norm, _WorldSpaceLightPos0.xyz)) * _LightColor0;
                
                // lighting = diffuse lighting (main light)
                //          + illumination from ambient (evaluate via ShadeSH9 from UnityCG.cginc function using world space normal)
                o.diff.rgb += ShadeSH9(half4(norm, 1));
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * i.diff;
                return col;
            }
            ENDCG
        }
    }
}
