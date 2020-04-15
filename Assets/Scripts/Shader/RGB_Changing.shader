Shader "Custom/RGB_Changing"
{
    Properties
    {
        _Color("Tint", Color) = (0,0,0,1)
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
                    fixed4 color : COLOR;
                };

                struct VOut
                {
                    float4 pos : SV_POSITION;
                    float2 uv : TEXCOORD0;
                    fixed4 color : COLOR;
                };

                VOut VS(VIn input)
                {
                    VOut output;
                    output.pos = UnityObjectToClipPos(input.pos);
                    output.uv = TRANSFORM_TEX(input.uv, _MainTex);
                    output.color = input.pos + sin(_Time * _SinFrequency) / _SinInterval;

                    return output;
                }

                float4 PS(VOut input) : SV_TARGET
                {
                    fixed4 col = tex2D(_MainTex, input.uv);

                    col *= _Color;
                    col *= input.color;

                    return col;
                }
                ENDCG
            }

        }
}
