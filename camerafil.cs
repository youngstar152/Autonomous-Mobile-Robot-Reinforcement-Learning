using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafil : MonoBehaviour
{
    // Start is called before the first frame update
     [SerializeField] private Material filter;

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src,dest,filter);
    }
}
