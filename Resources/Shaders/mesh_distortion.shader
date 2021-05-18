// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Shaders/mesh_distortion"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        // _c1 ("c1", Range(-3, 10)) = 1.37
        // _c2 ("c2", Range(-3, 10)) = 0.21
		// _scaling("scaling factor", range(0.0, 25.0)) = 1.0
	}
	SubShader
	{
		pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			// float _c1;
			// float _c2;
			// float _scaling;

			uniform half3 _Parameters;

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

            // float getDistortedRadiusFromVertex(float4 vertex) 
            // {
            //     float radius = sqrt(vertex.x * vertex.x + vertex.y * vertex.y);
            //     float distoredRadius = radius + _c1 * pow(radius, 3) + _c2 * pow(radius, 5);
            //     return distoredRadius;
            // }

            // float4 brownTransformation(float4 vertex) 
            // {
			// 	float r = getDistortedRadiusFromVertex(vertex);
            //     float numerator = _c1 * pow(r, 2) + _c2 * pow(r, 4) + pow(_c1, 2) * pow(r, 4) + pow(_c2, 2) * pow(r, 8) + 2 * _c1 * _c2 * pow(r, 6);
            //     float denominator = 1 + 4 * _c1 * pow(r, 2) + 6 * _c2 * pow(r, 4);
            //     vertex.x = _scaling * max(min(vertex.x * numerator / denominator, 1), -1);
            //     vertex.y = _scaling * max(min(vertex.y * numerator / denominator, 1), -1);
			// 	return vertex;
            // }

            v2f vert (appdata v)
            {
                v2f o;
				// o.uv = v.uv;
				o.vertex = UnityObjectToClipPos(v.vertex);

				// float r = sqrt(pow(o.uv.x, 2.0) + pow(o.uv.y, 2.0));
				// float r_u = r + _c1 * pow(r, 3.0) + _c2 * pow(r, 5.0);

				// float num = _c1 * pow(r, 2) + _c2 * pow(r, 4) + pow(_c1, 2) * pow(r, 4) + pow(_c2, 2) * pow(r, 8) + 2 * _c1 * _c2 * pow(r, 6);
                // float den = 1 + 4 * _c1 * pow(r, 2) + 6 * _c2 * pow(r, 4);

				// o.uv.x = num / den * _scaling * o.uv.x;
				// o.uv.y = num / den * _scaling * o.uv.y;

				// o.uv.x = _scaling * max(min(o.uv.x * num / den, 1), -1);
                // o.uv.y = _scaling * max(min(o.uv.y * num / den, 1), -1);

				// float r = sqrt(pow(o.vertex.x, 2.0) + pow(o.vertex.y, 2.0));
				// float r_u = r + _c1 * pow(r, 3.0) + _c2 * pow(r, 5.0);

				// float num = _c1 * pow(r, 2) + _c2 * pow(r, 4) + pow(_c1, 2) * pow(r, 4) + pow(_c2, 2) * pow(r, 8) + 2 * _c1 * _c2 * pow(r, 6);
                // float den = 1 + 4 * _c1 * pow(r, 2) + 6 * _c2 * pow(r, 4);

				// o.vertex.x = num / den * _scaling * o.vertex.x;
				// o.vertex.y = num / den * _scaling * o.vertex.y;
				// o.vertex.x = _scaling * max(min(o.vertex.x * num / den, 1), -1);
                // o.vertex.y = _scaling * max(min(o.vertex.y * num / den, 1), -1);

				
				float _c1 = _Parameters[0];
				float _c2 = _Parameters[1];
				float _scaling = _Parameters[2];

				float2 h = v.uv.xy - float2(0.5, 0.5);
				float r_d_2 = pow(h.x, 2.0) + pow(h.y, 2.0);

				float num = _c1 * r_d_2 + _c2 * pow(r_d_2, 2.0) + pow(_c1, 2.0) * pow(r_d_2, 2.0) + pow(_c2, 2.0) * pow(r_d_2, 4.0) + 2.0 * _c1 * _c2 * pow(r_d_2, 3.0);
				float den = 1.0 + 4.0 * _c1 * r_d_2 + 6.0 * _c2 * pow(r_d_2, 2.0);
				float f = num / den;

				v.uv = f * _scaling * h + 0.5;

                o.vertex = UnityObjectToClipPos(v.vertex);
                // o.vertex = brownTransformation(o.vertex);
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