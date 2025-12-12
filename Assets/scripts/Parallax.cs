using System.IO;
using UnityEngine;

public class Parallax : MonoBehaviour
{
   private MeshRenderer MeshRenderer;
    public float animationspeed = 1f;   

    private void Awake()
    {
        MeshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        MeshRenderer.material.mainTextureOffset += new Vector2(animationspeed * Time.deltaTime, 0);
    }

    private void OnEnable()
    {
        Debug.Log("? Parallex ENABLED!");
    }

    private void OnDisable()
    {
        Debug.Log("? Parallex DISABLED!");
    }
}


