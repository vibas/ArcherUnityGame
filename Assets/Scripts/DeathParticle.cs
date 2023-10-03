using UnityEngine;
using ToolBox.Pools;

public class DeathParticle : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;   

    void OnEnable()
    {
        audioSource.Play();
    }
    
    public void OnParticleSystemStopped()
    {
        gameObject.Release();
    }
}
