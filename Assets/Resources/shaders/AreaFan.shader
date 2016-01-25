Shader "Custom/Area Fan"
{
	Properties
	{
		_Color ("Main Color", Color) = (1,1,1,1)
		_Angle ("Angle", Float) = 360.0
	}
SubShader
{
	Tags
	{ 
		"Queue" = "Transparent"
		"IgnoreProjector" = "True"
		"RenderType" = "Transparent"
	}
    Pass {
		Lighting Off
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
	CGPROGRAM
	#pragma vertex vert
	#pragma fragment frag
	#pragma fragmentoption ARB_fog_exp2
	#include "UnityCG.cginc"

	float4 _Color;
	float _Angle;
	struct v2f {
	    float4 pos : SV_POSITION;
	    float2 texcoord		: TEXCOORD0;
	};
	
	v2f vert (appdata_base v)
	{
	    v2f o;
	    o.pos		= mul (UNITY_MATRIX_MVP, v.vertex);
	    o.texcoord	= v.texcoord;
	    return o;
	}	

	float4 frag (v2f i) : COLOR
	{
		float2 tex_pos = i.texcoord - float2(0.5,0.5);
		float rad = atan2( tex_pos.x,tex_pos.y );
		float alpha = _Color.a;
		if ( rad > radians( _Angle - 180 ) )
		{
			alpha = 0;
		}
		else
		{
			float area_length = tex_pos.x * tex_pos.x + tex_pos.y * tex_pos.y;
			alpha =  area_length > 0.25 ? 0 : alpha * area_length / 0.25;
		}
		return float4( _Color.rgb, alpha );
	}
	
ENDCG

    }
}

Fallback "VertexLit"
}

