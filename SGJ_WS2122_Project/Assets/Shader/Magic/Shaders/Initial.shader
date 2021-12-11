Shader "Unlit/Initial"
{
    Properties
    {
        _Pattern("Pattern", 2D) = "white" {}
        _PatternWidth("PatternWidth", int) = 240
        _PatternHeight("PatternWidth", int) = 240
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            
            #include "UnityCustomRenderTexture.cginc"
            
            #pragma vertex CustomRenderTextureVertexShader
            #pragma fragment fragment_shader

            sampler2D _Pattern;
            int _PatternWidth;
            int _PatternHeight;

            fixed4 fragment_shader(v2f_customrendertexture IN) : COLOR
            {
                return tex2D(_Pattern, float2(IN.vertex.x % _PatternWidth / _PatternWidth, IN.vertex.y % _PatternHeight / _PatternHeight));
            }
            
            ENDCG
        }
    }
}
