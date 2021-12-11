using UnityEngine;

[ExecuteInEditMode]

public class PostProcess : MonoBehaviour
{
    [SerializeField] private RenderTexture colorTexture;
    [SerializeField] private RenderTexture depthTexture;

    private RenderTexture _texture;
    private TextureMode _textureMode;

    private void Awake()
    {
        _texture = colorTexture;
        _textureMode = TextureMode.Color;
    }
    
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(_texture, destination);
    }

    public void SwitchTexture()
    {
        if (_textureMode == TextureMode.Color)
        {
            _texture = depthTexture;
            _textureMode = TextureMode.Depth;
        } 
        else if (_textureMode == TextureMode.Depth)
        {
            _texture = colorTexture;
            _textureMode = TextureMode.Color;
        }
    }
}
