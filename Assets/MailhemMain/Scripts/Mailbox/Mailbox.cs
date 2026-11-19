using UnityEngine;

public class Mailbox : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] AudioClip sonidoEntrega;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioManager audioManager;
    [SerializeField] AudioClip playerVoice;
    [Header("Minimap")]
    [SerializeField] GameObject waypoint;
    [SerializeField] Camera mapCamera;
    [Header("Iluminacion")]
    [SerializeField] Light lampPostLight;
    Vector3 waypointOriginalPos;

    GameObject package;

    ParticleInstatiator instatiator;

    public delegate void Mail();
    public static event Mail Delivered; 
    private void Start()
    {
        waypointOriginalPos = waypoint.transform.position;
        audioSource = GetComponent<AudioSource>();
        instatiator = GetComponentInChildren<ParticleInstatiator>();
    }

    void Update()
    {
        if(waypoint.activeSelf)
        {
            if(CheckIfVisibleInMap())
            {
                waypoint.transform.position = waypointOriginalPos;
            }
            else
            {
                float offset = 12;
                Vector2 direction = (new Vector2(waypointOriginalPos.x, waypointOriginalPos.z) - new Vector2(mapCamera.transform.position.x, mapCamera.transform.position.z)).normalized;
                waypoint.transform.position = new Vector3(mapCamera.transform.position.x +(direction.x * offset), waypoint.transform.position.y, mapCamera.transform.position.z + (direction.y * offset));
            }
        }
    }

    public void SetPackage(GameObject pack)
    {
        package = pack;
        package.GetComponent<Package>().PickedUp += ActivateComponents;
        package.GetComponent<Package>().Dropped += DeactivateComponents;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == package)
        {
            if (sonidoEntrega != null && audioSource != null)
            {
                audioSource.PlayOneShot(sonidoEntrega);
            }
            instatiator.Emit();

            audioManager.PlayVoice(playerVoice);

            package.GetComponent<Package>().PickedUp -= ActivateComponents;
            package.GetComponent<Package>().Dropped -= DeactivateComponents;

            Destroy(other.gameObject);
            package = null;
            Delivered();
        }
    }

    void ActivateComponents()
    {
        lampPostLight.enabled = true;
        waypoint.SetActive(true);
    }

    void DeactivateComponents()
    {
        lampPostLight.enabled = false;
        waypoint.SetActive(false);
    }

    bool CheckIfVisibleInMap()
    {
        Vector3 viewportPos = mapCamera.WorldToViewportPoint(waypointOriginalPos);
        bool isVisible = viewportPos.x >= 0f && viewportPos.x <= 1f && viewportPos.y >= 0f && viewportPos.y <= 1f && viewportPos.z < 0f;
        return isVisible;
    }
}