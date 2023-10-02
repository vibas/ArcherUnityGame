using UnityEngine;
using ToolBox.Pools;

public class DestroyParticle : MonoBehaviour
{
    public void OnParticleSystemStopped()
    {
        gameObject.Release();
    }
}
