// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Particles/Lightbeam" {
	Properties{
		_MainTex("Particle Texture", 2D) = "white" {}
		_MinFadeDistance("MinDistance",Float) = 0
		_MaxFadeDistance("MaxDistance",Float) = 20

	}

		Category{
			Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
			Blend SrcAlpha One
			Cull Off Lighting Off ZWrite Off

			SubShader {
				Pass {

					CGPROGRAM
					#pragma vertex vert
					#pragma fragment frag
					#pragma multi_compile_fog

					#include "UnityCG.cginc"

					sampler2D _MainTex;

					struct appdata_t {
						float4 vertex : POSITION;
						fixed4 color : COLOR;
						float2 texcoord : TEXCOORD0;
						float3 worldPos : TEXCOORD2;
						float3 normal : NORMAL;
					};

					struct v2f {
						float4 vertex : SV_POSITION;
						fixed4 color : COLOR;
						float2 texcoord : TEXCOORD0;
						float3 worldPos : TEXCOORD2;
						UNITY_FOG_COORDS(1)
					};

					float4 _MainTex_ST;
					half _MinFadeDistance;
					half _MaxFadeDistance;

					v2f vert(appdata_t v)
					{
						v2f o;
						o.vertex = UnityObjectToClipPos(v.vertex);
						o.color = v.color;
						o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
						o.worldPos = mul(unity_ObjectToWorld, v.vertex);

						float3 viewDir = normalize(ObjSpaceViewDir(v.vertex));
						float dotProduct = 1 - dot(v.normal, viewDir);
						float rimWidth = 0.7;
						o.color =  1 - smoothstep(1 - rimWidth, 1.0, dotProduct);

						UNITY_TRANSFER_FOG(o,o.vertex);
						return o;
					}

					fixed4 frag(v2f i) : SV_Target
					{
						fixed4 col = i.color * tex2D(_MainTex, i.texcoord);
						half viewDist = distance(i.worldPos, _WorldSpaceCameraPos);
						col.rgb *= saturate((viewDist - _MinFadeDistance) / (_MaxFadeDistance - _MinFadeDistance));
						UNITY_APPLY_FOG_COLOR(i.fogCoord, col, fixed4(0, 0, 0, 0));
						return col;
					}
					ENDCG
				}
			}
	}
}
