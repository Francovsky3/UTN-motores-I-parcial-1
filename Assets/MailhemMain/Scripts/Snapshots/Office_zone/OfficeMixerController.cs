using UnityEngine;
using UnityEngine.Audio;

public class OfficeMixerController : MonoBehaviour
{
    public AudioMixerSnapshot normal;
    public AudioMixerSnapshot officeZone;

    [Min(0f)] public float transicion = 1f;

    public void Normal() { if (normal != null) normal.TransitionTo(transicion); }
    public void Dialogue() { if (officeZone != null) officeZone.TransitionTo(transicion); }

    private void Start() { Normal(); }
}

