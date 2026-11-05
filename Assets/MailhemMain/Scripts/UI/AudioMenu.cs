using UnityEngine;
using UnityEngine.Audio;

public class AudioMenu : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] GameObject pauseMenu;

    public void OnMasterSliderChange(float value)
    {
        mixer.SetFloat("MasterVol", Mathf.Log10(value)* 20);
    }
    public void OnMusicSliderChange(float value)
    {
        mixer.SetFloat("MusicaVol", Mathf.Log10(value)* 20);
    }
    public void OnSFXSliderChange(float value)
    {
        mixer.SetFloat("SFXVol", Mathf.Log10(value)* 20);
    }
    public void OnVoicesSliderChange(float value)
    {
        mixer.SetFloat("VozVol", Mathf.Log10(value)* 20);
    }
    public void OnAmbianceSliderChange(float value)
    {
        mixer.SetFloat("AmbienteVol", Mathf.Log10(value)* 20);
    }

    public void OnCloseMenuButton()
    {
        pauseMenu.SetActive(true);
        gameObject.SetActive(false);
    } 
}
