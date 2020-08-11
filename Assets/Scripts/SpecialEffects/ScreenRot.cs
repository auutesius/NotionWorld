using UnityEngine;
using System.Collections;
 
public class ScreenRot : MonoBehaviour 
{
    public Material mtl;
 
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit (src, dest,mtl);
    }
}