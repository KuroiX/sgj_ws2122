using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderToScreen : MonoBehaviour
{
    [SerializeField] private RenderTexture texture;
    
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(texture, dest);
    }
}
