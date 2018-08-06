Shader "Custom/waterRippleShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		//mycode
		_Scale ("Scale", float) = 1
		_Frequency ("Frequency", float) = 1
		_Speed ("Speed", float) = 1
		_Scale2 ("Scale2", float) = 1
		_Frequency2 ("Frequency2", float) = 1
		_Speed2 ("Speed2", float) = 1

		_WaveAmp1 ("_WaveAmp1", float) = 0
		_WaveAmp2 ("_WaveAmp2", float) = 0
		_WaveAmp3 ("_WaveAmp3", float) = 0
		_WaveAmp4 ("_WaveAmp4", float) = 0
		_WaveAmp5 ("_WaveAmp5", float) = 0
		_WaveAmp6 ("_WaveAmp6", float) = 0
		_WaveAmp7 ("_WaveAmp7", float) = 0
		_WaveAmp8 ("_WaveAmp8", float) = 0

	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		//mycode
		float _Scale, _Speed, _Frequency;
		float _Scale2, _Speed2, _Frequency2;
		half4 _Color;
		float _WaveAmp1, _WaveAmp2, _WaveAmp3, _WaveAmp4, _WaveAmp5, _WaveAmp6, _WaveAmp7, _WaveAmp8;
		float _OffsetX1, _OffsetZ1, _OffsetX2, _OffsetZ2, _OffsetX3, _OffsetZ3, _OffsetX4, _OffsetZ4, _OffsetX5, _OffsetZ5, _OffsetX6, _OffsetZ6, _OffsetX7, _OffsetZ7, _OffsetX8, _OffsetZ8;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;

		//mycode
		void vert( inout appdata_full v)
		{
			//wave
			half offsetvert2 = ((v.vertex.x) + (v.vertex.z));
			half value = _Scale2 * sin(_Time.x * _Speed2 + offsetvert2 * _Frequency2);
			v.vertex.y += value;
			v.normal.y += value;
			//ripple
			half offsetvert = ((v.vertex.x * v.vertex.x) + (v.vertex.z * v.vertex.z));
			half value1 = _Scale * sin(_Time.x * _Speed * _Frequency + offsetvert + (v.vertex.x * _OffsetX1) + (v.vertex.z * _OffsetZ1));
			half value2 = _Scale * sin(_Time.x * _Speed * _Frequency + offsetvert + (v.vertex.x * _OffsetX2) + (v.vertex.z * _OffsetZ2));
			half value3 = _Scale * sin(_Time.x * _Speed * _Frequency + offsetvert + (v.vertex.x * _OffsetX3) + (v.vertex.z * _OffsetZ3));
			half value4 = _Scale * sin(_Time.x * _Speed * _Frequency + offsetvert + (v.vertex.x * _OffsetX4) + (v.vertex.z * _OffsetZ4));
			half value5 = _Scale * sin(_Time.x * _Speed * _Frequency + offsetvert + (v.vertex.x * _OffsetX5) + (v.vertex.z * _OffsetZ5));
			half value6 = _Scale * sin(_Time.x * _Speed * _Frequency + offsetvert + (v.vertex.x * _OffsetX6) + (v.vertex.z * _OffsetZ6));
			half value7 = _Scale * sin(_Time.x * _Speed * _Frequency + offsetvert + (v.vertex.x * _OffsetX7) + (v.vertex.z * _OffsetZ7));
			half value8 = _Scale * sin(_Time.x * _Speed * _Frequency + offsetvert + (v.vertex.x * _OffsetX8) + (v.vertex.z * _OffsetZ8));


			v.vertex.y += value1 * _WaveAmp1;
			v.normal.y += value1 * _WaveAmp1; // to tell unity to detect light maps changes

			v.vertex.y += value2 * _WaveAmp2;
			v.normal.y += value2 * _WaveAmp2;

			v.vertex.y += value3 * _WaveAmp3;
			v.normal.y += value3 * _WaveAmp3;

			v.vertex.y += value4 * _WaveAmp4;
			v.normal.y += value4 * _WaveAmp4;

			v.vertex.y += value5 * _WaveAmp5;
			v.normal.y += value5 * _WaveAmp5;

			v.vertex.y += value6 * _WaveAmp6;
			v.normal.y += value6 * _WaveAmp6;

			v.vertex.y += value7 * _WaveAmp7;
			v.normal.y += value7 * _WaveAmp7;

			v.vertex.y += value8 * _WaveAmp8;
			v.normal.y += value8 * _WaveAmp8;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = _Color.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
