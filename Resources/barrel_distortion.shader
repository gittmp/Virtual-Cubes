Shader "barrel_distortion" 
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		_distortion("distortion", range(-3, 3)) = -0.7
		_cubicDistortion("cubicDistortion", range(0, 3)) = 0.4
		_scale("scale", range(0, 3)) = 1
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

			float _distortion;
			float _cubicDistortion;
			float _scale;

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
				float r2 = h.x * h.x + h.y * h.y;
				float f = 1.0 + r2 * (_distortion + _cubicDistortion * sqrt(r2));

				i.uv = f * _scale * h + 0.5;

				fixed4 col = tex2D(_MainTex, i.uv);
				return col;
			}
			ENDCG
		}
	}
}