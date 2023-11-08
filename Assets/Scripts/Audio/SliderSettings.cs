using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mainMixer; // Asigna el mezclador en el Inspector
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider ambSlider;

    public const string MIXER_MUSIC = "MusicVolume";
    public const string MIXER_AMB = "AmbienceVolume";
    public const string MIXER_SFX = "EffectsVolume";

    private void Awake()
    {
        sfxSlider.onValueChanged.AddListener(SetEffectsVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        ambSlider.onValueChanged.AddListener(SetAmbienceVolume);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.SFX_KEY, sfxSlider.value);
        PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, musicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.AMB_KEY, ambSlider.value);
    }

    public void SetMusicVolume(float volume)
    {
        mainMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }

    public void SetEffectsVolume(float volume)
    {
        mainMixer.SetFloat("EffectsVolume", Mathf.Log10(volume) * 20);
    }

    public void SetAmbienceVolume(float volume)
    {
        mainMixer.SetFloat("AmbienceVolume", Mathf.Log10(volume) * 20);
    }
}