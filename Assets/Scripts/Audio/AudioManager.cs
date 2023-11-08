using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;

    [SerializeField] private AudioMixer mainMixer;


    public const string MUSIC_KEY = "MusicVolume";
    public const string AMB_KEY = "AmbienceVolume";
    public const string SFX_KEY = "EffectsVolume";

    private void Awake()
    {
       CreateSingleton();
        LoadVolume();
    }
    void CreateSingleton()
    {
        if (audioManager == null)
        {
            audioManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadVolume()
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float effectsVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);
        float ambienceVolume = PlayerPrefs.GetFloat(AMB_KEY, 1f);

;       mainMixer.SetFloat(SliderSettings.MIXER_SFX, Mathf.Log10(musicVolume) * 20);
        mainMixer.SetFloat(SliderSettings.MIXER_MUSIC, Mathf.Log10(effectsVolume) * 20);
        mainMixer.SetFloat(SliderSettings.MIXER_AMB, Mathf.Log10(ambienceVolume) * 20);
    }
}
