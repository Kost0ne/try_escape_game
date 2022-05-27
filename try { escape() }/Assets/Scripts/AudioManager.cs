using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{

    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

    [Range(0f, 0.5f)]
    public float randomVolume = 0.1f;
    [Range(0f, 0.5f)]
    public float randomPitch = 0.1f;

    public bool loop = false;

    private AudioSource source;

    public void SetSource(AudioSource source, AudioMixerGroup mixer)
    {
        this.source = source;
        this.source.outputAudioMixerGroup = mixer;
        this.source.clip = clip;
        this.source.loop = loop;
    }

    public void Play()
    {
        source.volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }

}

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public AudioMixerGroup Mixer;

    [SerializeField]
    Sound[] sounds;

    void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        for (var i = 0; i < sounds.Length; i++)
        {
            var gameObj = new GameObject("Sound_" + i + "_" + sounds[i].name);
            gameObj.transform.SetParent(this.transform);
            sounds[i].SetSource(gameObj.AddComponent<AudioSource>(), Mixer);
        }

        PlaySound("Music");
    }

    public void PlaySound(string name)
    {
        foreach (var sound in sounds)
        {
            if (sound.name != name) continue;
            sound.Play();
            return;
        }
        
        Debug.LogWarning("AudioManager: Sound not found in list, " + name);
    }

    public void StopSound(string name)
    {
        foreach (var sound in sounds)
        {
            if (sound.name != name) continue;
            sound.Stop();
            return;
        }

        Debug.LogWarning("AudioManager: Sound not found in list, " + name);
    }

}