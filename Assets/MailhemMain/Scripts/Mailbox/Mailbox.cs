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
    Vector3 waypointOriginalPos;

    GameObject package;

    private void Start()
    {
        waypointOriginalPos = waypoint.transform.position;
        audioSource = GetComponent<AudioSource>();
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
                float offset = 15;
                Vector2 direction = (new Vector2(waypointOriginalPos.x, waypointOriginalPos.z) - new Vector2(mapCamera.transform.position.x, mapCamera.transform.position.z)).normalized;
                waypoint.transform.position = new Vector3(mapCamera.transform.position.x +(direction.x * offset), waypoint.transform.position.y, mapCamera.transform.position.z + (direction.y * offset));
            }
        }
    }

    public void SetPackage(GameObject pack)
    {
        package = pack;
        package.GetComponent<Package>().PickedUp += ShowWaypoint;
        package.GetComponent<Package>().Dropped += HideWaypoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == package)
        {
            if (sonidoEntrega != null && audioSource != null)
            {
                audioSource.PlayOneShot(sonidoEntrega);
            }

            audioManager.PlayVoice(playerVoice);

            package.GetComponent<Package>().PickedUp -= ShowWaypoint;
            package.GetComponent<Package>().Dropped -= HideWaypoint;

            Destroy(other.gameObject);
            package = null;
        }
    }

    void ShowWaypoint()
    {
        waypoint.SetActive(true);
    }

    void HideWaypoint()
    {
        waypoint.SetActive(false);
    }

    bool CheckIfVisibleInMap()
    {
        Vector3 viewportPos = mapCamera.WorldToViewportPoint(waypointOriginalPos);
        bool isVisible = viewportPos.x >= 0f && viewportPos.x <= 1f && viewportPos.y >= 0f && viewportPos.y <= 1f && viewportPos.z < 0f;
        return isVisible;
    }
}