Shader "Unlit/Magic"
{
    Properties
    {
        _CurrentColumn("CurrentColumn", int) = 0
        _Heightmap("Heightmap", 2D) = "white" {}
        _Aether("Aether", 2D) = "white" {}
        _ScreenWidth("ScreenWidth", int) = 1920
        _ScreenHeight("ScreenHeight", int) = 1080
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

            int _CurrentColumn;
            sampler2D _Heightmap;
            sampler2D _Aether;
            int _PatternWidth;
            int _PatternHeight;
            int _ScreenWidth;
            int _ScreenHeight;

            bool float_comparison(float first, float second, float accuracy)
            {
                 return abs(first - second) <= accuracy;
            }
            
            bool empty_lookup(float4 lookup)
            {
                return all(lookup.xy < float2(0.f, 0.f));
            }

            fixed4 fix_limbo(float2 pos, float2 uv, int layers)
            {
                float height_value = tex2D(_Heightmap, float2(uv.x - _PatternWidth / (float) _ScreenWidth, uv.y)).r;
                if (float_comparison(height_value, 0, 1.f / layers))
                {
                    float2 previous_column_pixel = float2(uv.x - _PatternWidth / (float) _ScreenWidth, uv.y);
                    return tex2D(_Aether, previous_column_pixel);  // case: no limbo
                }
                float2 limbo_replacer_pixel = float2(pos.x % _PatternWidth / _ScreenWidth, (pos.y + _PatternHeight / 8.f * _CurrentColumn) % _PatternHeight / _ScreenHeight);
                return tex2D(_Aether, limbo_replacer_pixel);    // case: limbo 
            }

            float4 find_lookup(float2 pos, int layers, int layer_distance)
            {
                for (int i = layers; i > 0; i--)
                {
                    int shift_candidate = layer_distance * i;
                    float2 lookup_screen = float2(pos.x - _PatternWidth + shift_candidate, pos.y);
                    float2 lookup_norm = float2(lookup_screen.x / (float) _ScreenWidth, lookup_screen.y / (float) _ScreenHeight);
                    int shift_actual = round(tex2D(_Heightmap, lookup_norm).x * layers) * layer_distance;
                    bool candiate_shift_is_actual_shift = float_comparison(lookup_screen.x + _PatternWidth - shift_actual, pos.x, 0.5f / _ScreenWidth);
                    if (candiate_shift_is_actual_shift)
                        return float4(lookup_screen.xy, lookup_norm.xy);
                }
                return float4(-1.f, -1.f, -1.f, -1.f);
            }
            
            fixed4 fragment_shader(v2f_customrendertexture IN) : COLOR
            {
                bool not_in_this_column = IN.vertex.x < _PatternWidth * _CurrentColumn || IN.vertex.x >= _PatternWidth * (_CurrentColumn+1);
                if (not_in_this_column)
                    return tex2D(_Aether, IN.localTexcoord);

                int max_depth = 100;
                int layers = 100;
                int layer_distance = max_depth / layers;
                
                float4 first_lookup = find_lookup(IN.vertex.xy, layers, layer_distance);
                if (empty_lookup(first_lookup))
                    return fix_limbo(IN.vertex.xy, IN.localTexcoord.xy, layers);

                bool lookup_is_in_previous_column = first_lookup.x < _PatternWidth * _CurrentColumn;
                if (lookup_is_in_previous_column)
                    return tex2D(_Aether, first_lookup.zw);

                float4 second_lookup = find_lookup(first_lookup.xy, layers, layer_distance);
                if (empty_lookup(second_lookup))
                    return fix_limbo(first_lookup.xy, first_lookup.zw, layers);

                return tex2D(_Aether, second_lookup.zw);
            }
            
            ENDCG
        }
    }
}
