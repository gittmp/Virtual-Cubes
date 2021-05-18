Shader "Shaders/LCA_correction"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;

            uniform half2 _RedShift;
			uniform half2 _GreenShift;
			uniform half2 _BlueShift;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float red = tex2D(_MainTex, float2(i.uv + _RedShift)).r;
                float green = tex2D(_MainTex, float2(i.uv + _GreenShift)).g;
                float blue = tex2D(_MainTex, float2(i.uv + _BlueShift)).b;
                fixed4 col = fixed4(red, green, blue, 1);

                return col;
            }
            ENDCG
        }
    }
}
