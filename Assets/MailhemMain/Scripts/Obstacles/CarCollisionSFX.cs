using UnityEngine;

public class CarCollisionSFX : MonoBehaviour
{
    [SerializeField] private float hornDistance = 9.5f; 
    [SerializeField] private AudioClip[] hornClips;
    [SerializeField] private AudioClip[] hitClips;  

    private bool yaSonoBocina = false;
    private bool yaChocoGolpe = false;
    private Transform playerTransform;

    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
    }

    void Update()
    {
        if (playerTransform == null || yaSonoBocina) return;

        float distancia = Vector3.Distance(transform.position, playerTransform.position);

 
        if (distancia <= hornDistance)
        {
            AudioManager playerSFX = playerTransform.GetComponentInChildren<AudioManager>();
            if (playerSFX != null && hornClips != null && hornClips.Length > 0)
            {
                yaSonoBocina = true;
                playerSFX.PlaySFX(hornClips);
                Debug.Log("CarCollisionSFX: Bocina activada por proximidad.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !yaChocoGolpe)
        {
            AudioManager playerSFX = other.GetComponentInChildren<AudioManager>();
            if (playerSFX != null && hitClips != null && hitClips.Length > 0)
            {
                yaChocoGolpe = true;
                playerSFX.PlaySFX(hitClips);
                Debug.Log("CarCollisionSFX: �Impacto! El personaje reprodujo el golpe.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
 
        if (other.CompareTag("Player"))
        {
            yaSonoBocina = false;
            yaChocoGolpe = false;
        }
    }
}
