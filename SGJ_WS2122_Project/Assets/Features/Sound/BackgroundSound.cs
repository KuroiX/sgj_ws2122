using UnityEngine;

public class BackgroundSound : MonoBehaviour
{

    [SerializeField] private AudioClip endMusic;
    
    [SerializeField] private AudioSource shatteredAudioSource;
    [SerializeField] private AudioSource coloredAudioSource;
    
    private bool _coloredLayer = true;

    public void SwitchBackground()
    {
        _coloredLayer = !_coloredLayer;
        if (_coloredLayer)
        {
            coloredAudioSource.volume = 1f;
            shatteredAudioSource.volume = 0f;
        }
        else 
        {
            coloredAudioSource.volume = 0f;
            shatteredAudioSource.volume = 1f;
        }
    }

    public void EndMusic()
    {
        shatteredAudioSource.Stop();
        coloredAudioSource.Stop();
        shatteredAudioSource.volume = 0f;
        coloredAudioSource.volume = 1f;
        coloredAudioSource.clip = endMusic;
        coloredAudioSource.Play();
    }

}
