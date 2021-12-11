Shader "Unlit/Magic"
{
    Properties
    {
        _Heightmap("Heightmap", 2D) = "white" {}
        _Aether("Aether", 2D) = "white" {}
        _ScreenWidth("ScreenWidth", int) = 1920
        _ScreenHeight("ScreenHeight", int) = 1080
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            
            #include "UnityCustomRenderTexture.cginc"
            
            #pragma vertex CustomRenderTextureVertexShader
            #pragma fragment fragment_shader

            sampler2D _Aether;
            int _ScreenWidth;
            int _ScreenHeight;

            fixed4 fragment_shader(v2f_customrendertexture IN) : COLOR
            {
                return tex2D(_Aether, IN.localTexcoord);
            }
            
            ENDCG
        }
    }
}
