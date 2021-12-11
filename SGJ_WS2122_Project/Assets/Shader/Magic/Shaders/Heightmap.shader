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
                /*float whiteness = IN.pos.z * 10.f;
                if (whiteness < 0.f)
                    whiteness = 0.f;
                return fixed4(whiteness, 0, 0, 1);*/
                //float whiteness = (1 - Linear01Depth(IN.pos.z)) * _ProjectionParams.z / (_ProjectionParams.z - _ProjectionParams.y);
                //float whiteness = IN.pos.z * _ProjectionParams.z / (_ProjectionParams.z - _ProjectionParams.y);
                float whiteness = IN.pos.z * 10.f;
                if (whiteness < 0.f)
                    whiteness = 0.f;
                return fixed4(whiteness, whiteness, whiteness, 1);
            }
            
            ENDCG
        }
    }
}
