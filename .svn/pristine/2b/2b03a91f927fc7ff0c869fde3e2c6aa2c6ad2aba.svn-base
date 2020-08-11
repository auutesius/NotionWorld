Shader "Custom/Dissolved"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Noise ("噪音图", 2D) = "white" {}
        _EdgeColor("边缘颜色",Color)=(1,1,1,1)
        _EdgeWidth("边缘宽度",Range(0,1))=0.2
        _Cut("溶解程度", Range(0,1))=0.5
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed4 _Color;
            sampler2D _MainTex;
            sampler2D _Noise;
            fixed4 _EdgeColor;
            float _EdgeWidth;
            float _Cut;

            struct a2v
            {
                fixed4 vertex:POSITION;
                fixed3 normal:NORMAL;
                fixed4 texcoord:TEXCOORD0;
            };
            struct v2f
            {
                fixed4 pos:SV_POSITION;
                fixed2 uv:TEXCOORD0;
                fixed3 worldNormal:TEXCOORD1;
                fixed3 worldPos:TEXCOORD2;
            };

            v2f vert(a2v v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.uv = v.texcoord.xy;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }
            fixed4 frag(v2f i):SV_Target
            {
                float r = tex2D(_Noise, i.uv).r;
                clip(r-_Cut);
                if(r-_Cut<_EdgeWidth)
                {
                    return _EdgeColor;
                }
                fixed3 albedo = tex2D(_MainTex, i.uv).rgb * _Color.rgb;
                

                fixed3 ambient = albedo*UNITY_LIGHTMODEL_AMBIENT.rgb;

                fixed3 l = normalize(_WorldSpaceLightPos0.xyz - _WorldSpaceLightPos0.w*i.worldPos);
                float NdotL = max(0,dot(l, i.worldNormal));

                fixed3 diffuse = albedo*NdotL;

                return fixed4(ambient + diffuse, 1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}