using UnityEngine;
using UnityEngine.Audio;

public class CarSFXTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip horn;
    private AudioSource audiosource;
    private bool playerOnCollision;
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }


    void Update()
    {
        
    }
}
