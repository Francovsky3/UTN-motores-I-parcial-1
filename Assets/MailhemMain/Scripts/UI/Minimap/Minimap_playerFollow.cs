using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private float heightAbovePlayer;
    public Transform player;

    private void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, player.position.y + heightAbovePlayer, player.position.z);
    }

}
