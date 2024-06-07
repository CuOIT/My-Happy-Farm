Shader "LolTap/GroundFakeDepth" {
	Properties {
		_Emssion ("Emission", Range(0, 1)) = 0.2
		_MainColor ("MainColor", Vector) = (1,1,1,1)
		_FoamColor ("FoamColor", Vector) = (1,1,1,1)
		_ClearColor ("ClearColor", Vector) = (1,1,1,1)
		_FoamLine ("FoamLine", Float) = 0
		_FoamEnd ("DepthEnd", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
	Fallback "Diffuse"
}