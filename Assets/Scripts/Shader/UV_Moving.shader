Shader "Custom/UV_Moving"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Texture", 2D) = "white" {}
        _SinFrequency("SinFrequency", float) = 4.0 // Faster, more sine curves
        _SinInterval("SinInterval", float) = 10.0 // Between which values moves the sine
    }
        SubShader
        {
             Tags
            {
                "RenderType" = "Transparent"
                "Queue" = "Transparent" 
            }

            Blend SrcAlpha OneMinusSrcAlpha

            ZWrite off
            Cull off

            pass
            {
                CGPROGRAM //------------BEGIN SHADER CODE
                #include "UnityCG.cginc"
                
                #pragma vertex VS
                #pragma fragment PS

                uniform float4 _Color;
                uniform sampler2D _MainTex;
                uniform float4 _MainTex_ST;

                uniform float _SinFrequency;
                uniform float _SinInterval;

                struct VIn
                {
                    float4 pos : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct VOut
                {
                    float4 pos : SV_POSITION;
                    float2 uv : TEXCOORD0;
                };

                VOut VS(VIn input)
                {
                    VOut output;

                    output.pos = UnityObjectToClipPos(input.pos);
                    output.uv = TRANSFORM_TEX(input.uv, _MainTex);

                    return output;
                }

                float4 PS(VOut input) : SV_TARGET
                {
                    fixed4 col = tex2D(_MainTex, float2(input.uv.x + _SinTime.y / 6 * 9, input.uv.y));
                    _Color.rgb += sin(_Time * _SinFrequency) / _SinInterval;

                    col *= _Color;

                    return col;
                }
                ENDCG
            }

        }
}
