using UnityEngine;

public class PlayerConfiguration : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] float moveSpeed;
    public float MOVESPEED => moveSpeed;
}
