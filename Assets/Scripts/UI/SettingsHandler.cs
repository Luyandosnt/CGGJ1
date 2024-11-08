using CrazyGames;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsHandler : MonoBehaviour
{
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public AudioMixer Mixer;

    private void Start()
    {
        masterVolumeSlider.value = CrazySDK.Data.GetFloat("MasterVolume", 1);
        musicVolumeSlider.value = CrazySDK.Data.GetFloat("MusicVolume", 1);
        sfxVolumeSlider.value = CrazySDK.Data.GetFloat("SFXVolume", 1);

        SetMasterVolume(masterVolumeSlider.value);
        SetMusicVolume(musicVolumeSlider.value);
        SetSFXVolume(sfxVolumeSlider.value);
    }

    public void SetMasterVolume(float volume)
    {
        Mixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        CrazySDK.Data.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        Mixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        CrazySDK.Data.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        Mixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        CrazySDK.Data.SetFloat("SFXVolume", volume);
    }

    public void ReturnToMenu()
    {
        CrazySDK.Game.GameplayStop();
        SceneManager.LoadScene(1);
    }
}
