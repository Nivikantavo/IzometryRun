using UnityEngine;
using UnityEngine.Audio;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixer;

    public void ChangeMusicVolume(float volume)
    {
        _mixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 20, volume));
    }

    public void ChangeEffectsVolume(float volume)
    {
        _mixer.audioMixer.SetFloat("EffectsVolume", Mathf.Lerp(-80, 20, volume));
    }
}
