using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip prepTimeMusic;
    public AudioClip combatMusic;
    public AudioClip bossMusic;
    public AudioClip winMusic;
    public AudioClip loseMusic;
    public AudioClip waveClearMusic;
    public AudioClip waveStartMusic;
    public AudioClip waveBossStartMusic;

    public AudioSource secondarySource;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayMusic(AudioClip music, bool loop)
    {
        audioSource.Stop();
        audioSource.clip = music;
        audioSource.loop = loop;
        audioSource.Play();
    }

    public void PlaySecondaryMusic(AudioClip music, bool loop)
    {
        secondarySource.Stop();
        secondarySource.clip = music;
        secondarySource.loop = loop;
        secondarySource.Play();
    }

    public void StopAll()
    {
        audioSource.Stop();
        secondarySource.Stop();
    }

    public void PlayPrepTimeMusic()
    {
        PlayMusic(prepTimeMusic, true);
    }

    public void PlayCombatMusic()
    {
        PlayMusic(combatMusic, true);
    }

    public void PlayBossMusic()
    {
        PlayMusic(bossMusic, true);
    }

    public void PlayWaveClearMusic()
    {
        PlaySecondaryMusic(waveClearMusic, false);
    }

    public void PlayWaveStartMusic()
    {
        PlaySecondaryMusic(waveStartMusic, false);
    }

    public void PlayWaveBossStartMusic()
    {
        PlaySecondaryMusic(waveBossStartMusic, false);
    }

    public void PlayWinMusic()
    {
        PlayMusic(winMusic, false);
    }

    public void PlayLoseMusic()
    {
        PlayMusic(loseMusic, false);
    }
}
