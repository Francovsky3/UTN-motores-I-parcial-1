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

    /*private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(20, 195, 230, 145), GUI.skin.box);
        GUILayout.Label("Clase 11: estados de mezcla");
        if (GUILayout.Button("Normal")) Normal();
        if (GUILayout.Button("Interior")) Interior();
        GUILayout.Label("Transición: " + transicion.ToString("0.0") + " s");
        transicion = GUILayout.HorizontalSlider(transicion, 0f, 3f);
        GUILayout.EndArea();
    }*/
}
