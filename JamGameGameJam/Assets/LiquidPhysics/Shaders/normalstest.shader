Shader "Unlit/normalstest"
{
	Properties
	{
		_Texture("Main Texture", 2D) = "defaulttexture" {}
		_Color("Main Color", Color) = (0,0,0,0)
		_Intensity("Intensity", Range(0, 1)) = 0.5
	}
		SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// include file that contains UnityObjectToWorldNormal helper function
			#include "UnityCG.cginc"

			sampler2D _Texture;
			fixed4 _Color;
			float _Intensity;

			struct v2f 
			{
				// we'll output world space normal as one of regular ("texcoord") interpolators
				half3 worldNormal : TEXCOORD0;
				float2 uv : TEXCOORD1;
				float4 pos : SV_POSITION;
			};

			// vertex shader: takes object space normal as input too
			v2f vert(float4 vertex : POSITION, float2 uv : TEXCOORD1, float3 normal : NORMAL)
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, vertex);
				o.uv = uv;
				// UnityCG.cginc file contains function to transform
				// normal from object to world space, use that
				o.worldNormal = UnityObjectToWorldNormal(normal);
				return o;
			}
			
			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 c = 0;
				fixed4 t = tex2D(_Texture, i.uv);
				// normal is a 3D vector with xyz components; in -1..1
				// range. To display it as color, bring the range into 0..1
				// and put into red, green, blue components
				c.rgb = i.worldNormal * _Intensity + 0.5;
				float mag = (c.r + c.g + c.b) / 3.0;
				c.rgb = _Color.rgb * mag;
				c.a = _Color.a;
				c.rgb = c.rgb * t.rgb;
				return c;
			}
			ENDCG
		}
	}
}
