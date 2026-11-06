using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip interiorMusic;
    [SerializeField] AudioClip exteriorMusic;
    [SerializeField] AudioClip packMusic;
    [SerializeField] AudioSource musicSource;

    public enum MusicState {interior, exterior, pack}

    public MusicState musicState = MusicState.interior;

    public void SwitchMusicState(MusicState state)
    {
        musicState = state;
        switch (musicState)
        {
            case MusicState.interior:
                musicSource.clip = interiorMusic;
                return;
            case MusicState.exterior:
                musicSource.clip = exteriorMusic;
                return;
            case MusicState.pack:
                musicSource.clip = packMusic;
                return;
        }
    }
}
