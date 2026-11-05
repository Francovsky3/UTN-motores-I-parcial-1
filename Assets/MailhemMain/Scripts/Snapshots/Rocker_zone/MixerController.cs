using UnityEngine;
using UnityEngine.Audio;

public class MixerController : MonoBehaviour
{
    public AudioMixerSnapshot normal;
    public AudioMixerSnapshot rockerZone;
    [Min(0f)] public float transicion = 1f;

    public void Normal() { if (normal != null) normal.TransitionTo(transicion); }
    public void RockerZone() { if (rockerZone != null) rockerZone.TransitionTo(transicion); }

    private void Start() { Normal(); }
}
