// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Unlit/matcap"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Matcap("Matcap Texture",2D) = "white" {}
		_Range("Range",Range(0,1.5)) = 1
		_Blend("Blend", Range(0, 1)) = 0.5
	}
	SubShader
	{
		
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float4 normal : TEXCOORD1;		//Phong－Shading
			};

			sampler2D _MainTex;
			sampler2D _Matcap;
			float _Range;
			float _Blend;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.normal = mul(UNITY_MATRIX_IT_MV, float4(v.normal, 1));
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float2 normalProj = normalize(i.normal.xyz) * 0.5 + 0.5;
				float4 matcap = tex2D(_Matcap, normalProj * _Range);
				float4 main = tex2D(_MainTex, i.uv);
				float4 col = lerp(main, matcap, _Blend);
				return col;
			}
			ENDCG
		}
	}
}
