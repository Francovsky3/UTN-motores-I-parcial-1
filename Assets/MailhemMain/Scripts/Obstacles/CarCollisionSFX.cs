using UnityEngine;

public class CarCollisionsSFX : MonoBehaviour
{
    [Header("Configuración de Distancia")]
    [SerializeField] private float hornDistance = 9.5f;

    [SerializeField] private AudioClip[] hornClips;
    [SerializeField] private AudioClip[] hitClips;

    [SerializeField] private AudioSource myAudioSource;
    private Transform playerTransform;
    private bool yaSonoBocina = false;
    private bool yaChocoGolpe = false;

    void Start()
    {
        myAudioSource = GetComponentInChildren<AudioSource>();

        if (myAudioSource == null)
        {
            Debug.LogWarning("No se encontró ningún AudioSource en los hijos del auto.");
        }


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
            if (myAudioSource != null && hornClips.Length > 0)
            {
                yaSonoBocina = true;
                AudioClip clipBocina = hornClips[Random.Range(0, hornClips.Length)];
                myAudioSource.PlayOneShot(clipBocina, 2.5f);
                Debug.Log("Bocina activada por proximidad.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") && !yaChocoGolpe)
        {
            if (myAudioSource != null && hitClips.Length > 0)
            {
                yaChocoGolpe = true;
                AudioClip clipGolpe = hitClips[Random.Range(0, hitClips.Length)];
                myAudioSource.PlayOneShot(clipGolpe, 2f);
                Debug.Log("Impacto con el auto reproducido correctamente.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            yaChocoGolpe = false;
        }
    }
}
