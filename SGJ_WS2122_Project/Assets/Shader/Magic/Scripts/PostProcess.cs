using UnityEngine;

[ExecuteInEditMode]

public class PostProcess : MonoBehaviour
{

    [SerializeField] private RenderTexture texture;
    
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(texture, destination);
    }
    
}
