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
            
            fixed4 fragment_shader(v2f_img IN) : SV_Target
            {
                float whiteness = (1 - Linear01Depth(IN.pos.z)) * _ProjectionParams.z / (_ProjectionParams.z - _ProjectionParams.y);
                if (whiteness > 1.f)
                    whiteness = 1.f;
                return fixed4(whiteness, whiteness, whiteness, 1);
            }
            
            ENDCG
        }
    }
}
