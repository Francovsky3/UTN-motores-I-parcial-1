using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class AudioOfficeZone : MonoBehaviour
{
    //public AudioSource fuente;
    public OfficeMixerController mezcla;
    private bool dentro;

    private void Reset() { GetComponent<BoxCollider>().isTrigger = true; }

    private void OnTriggerEnter(Collider otro)
    {
        if (!otro.CompareTag("Player") || dentro) return;
        dentro = true;
        //if (fuente != null && !fuente.isPlaying) fuente.Play();
        if (mezcla != null) mezcla.Dialogue();
        Debug.Log("Jugador: entrada a ZonaSonora", this);
    }

    private void OnTriggerExit(Collider otro)
    {
        if (!otro.CompareTag("Player") || !dentro) return;
        dentro = false;
        //if (fuente != null) fuente.Stop();
        if (mezcla != null) mezcla.Normal();
        Debug.Log("Jugador: salida de ZonaSonora", this);
    }

    private void OnDisable()
    {
        //if (fuente != null) fuente.Stop();
        if (dentro && mezcla != null && mezcla.isActiveAndEnabled) mezcla.Normal();
        dentro = false;
    }
}
