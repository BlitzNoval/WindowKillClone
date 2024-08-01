using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    public AudioSource audioSource;
    public List<AudioClip> playlist = new List<AudioClip>();
    private int currentTrackIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Ensure this GameObject persists across scenes
            LoadSettings();
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate instances
        }
    }

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (playlist.Count > 0)
        {
            PlayTrack(currentTrackIndex);
        }
    }

    public void PlayTrack(int index)
    {
        if (index >= 0 && index < playlist.Count)
        {
            audioSource.clip = playlist[index];
            audioSource.Play();
            currentTrackIndex = index;
            SaveSettings();
        }
    }

    public void PlayNextTrack()
    {
        currentTrackIndex = (currentTrackIndex + 1) % playlist.Count;
        PlayTrack(currentTrackIndex);
    }

    public void ToggleMute()
    {
        audioSource.mute = !audioSource.mute;
        SaveSettings();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        SaveSettings();
    }

    public string GetCurrentTrackName()
    {
        if (currentTrackIndex >= 0 && currentTrackIndex < playlist.Count)
        {
            return playlist[currentTrackIndex].name;
        }
        return "No Track";
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetInt("CurrentTrackIndex", currentTrackIndex);
        PlayerPrefs.SetInt("IsMuted", audioSource.mute ? 1 : 0);
        PlayerPrefs.SetFloat("Volume", audioSource.volume);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        currentTrackIndex = PlayerPrefs.GetInt("CurrentTrackIndex", 0);
        audioSource.mute = PlayerPrefs.GetInt("IsMuted", 0) == 1;
        audioSource.volume = PlayerPrefs.GetFloat("Volume", 1f);
    }
}
