Shader "pincushion_correction" 
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		_c1("constant1", range(-3, 3)) = 0.4
		_c2("constant2", range(0, 3)) = -0.7
		_scaling("scaling factor", range(0.0, 25.0)) = 25.0
	}
	SubShader
	{
		pass
		{
			Tags{ "LightMode" = "ForwardBase" }
			Cull Off ZWrite Off ZTest Always
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 4.0 
			#include "UnityCG.cginc"

			float _c1;
			float _c2;
			float _scaling;

			sampler2D _MainTex;
			fixed4 _MainTex_ST;

			struct v2f 
			{
				fixed4 vertex : SV_POSITION;
				fixed2 uv : TEXCOORD0;
			};

			v2f vert(appdata_full v) 
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}

			fixed4 frag(v2f i) :COLOR
			{
				float2 h = i.uv.xy - float2(0.5, 0.5);
				float r_d_2 = pow(h.x, 2.0) + pow(h.y, 2.0);

				float num = _c1 * r_d_2 + _c2 * pow(r_d_2, 2.0) + pow(_c1, 2.0) * pow(r_d_2, 2.0) + pow(_c2, 2.0) * pow(r_d_2, 4.0) + 2.0 * _c1 * _c2 * pow(r_d_2, 3.0);
				float den = 1.0 + 4.0 * _c1 * r_d_2 + 6.0 * _c2 * pow(r_d_2, 2.0);
				float f = num / den;

				i.uv = f * _scaling * h + 0.5;
				fixed4 col = tex2D(_MainTex, i.uv);
				
				return col;
			}
			ENDCG
		}
	}
}