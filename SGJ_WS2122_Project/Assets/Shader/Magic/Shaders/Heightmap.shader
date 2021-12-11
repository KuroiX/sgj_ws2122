Shader "Unlit/Heightmap"
{
    Properties
    {
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            
            #include "UnityCG.cginc"
            
            #pragma vertex vert_img
            #pragma fragment fragment_shader

            int scaler = 1;
            int limit = 1;
            
            fixed4 fragment_shader(v2f_img IN) : SV_Target
            {
                float whiteness = (1 - Linear01Depth(IN.pos.z)) * 14.f/12.f;
                if (whiteness > 1.f)
                    whiteness = 1.f;
                return fixed4(whiteness, whiteness, whiteness, 1);
            }
            
            ENDCG
        }
    }
}
