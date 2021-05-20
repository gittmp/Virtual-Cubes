Shader "Shaders/mesh_distortion"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_c1("constant1", Float) = 0.3
		_c2("constant2", Float) = -0.4
		_scaling("scaling factor", Float) = 23.0
	}
	SubShader
	{
		pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			float _c1;
			float _c2;
			float _scaling;

			sampler2D _MainTex;
			fixed4 _MainTex_ST;

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

				float2 h = v.uv.xy - float2(0.5, 0.5);
				float r_d_2 = pow(h.x, 2.0) + pow(h.y, 2.0);

				float num = _c1 * r_d_2 + _c2 * pow(r_d_2, 2.0) + pow(_c1, 2.0) * pow(r_d_2, 2.0) + pow(_c2, 2.0) * pow(r_d_2, 4.0) + 2.0 * _c1 * _c2 * pow(r_d_2, 3.0);
				float den = 1.0 + 4.0 * _c1 * r_d_2 + 6.0 * _c2 * pow(r_d_2, 2.0);
				float f = num / den;

				v.uv = f * _scaling * h + 0.5;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

			fixed4 frag(v2f i) :COLOR
			{
				fixed4 col = tex2D(_MainTex, i.uv);	
				return col;
			}
			ENDCG
		}
	}
}