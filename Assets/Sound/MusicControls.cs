using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicControls : MonoBehaviour
{
    public MusicManager musicManager;
    public Button playNextButton;
    public Button muteButton;
    public Slider volumeSlider;
    public TextMeshProUGUI trackNameText;

    private void Start()
    {
        if (MusicManager.Instance != null)
        {
            musicManager = MusicManager.Instance;
        }

        // Assign button listeners
        playNextButton.onClick.AddListener(PlayNext);
        muteButton.onClick.AddListener(ToggleMute);
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // Initialize the volume slider to match the current volume
        volumeSlider.value = musicManager.audioSource.volume;
        volumeSlider.onValueChanged.AddListener(delegate { SetVolume(volumeSlider.value); });

        // Update track name initially
        UpdateTrackName();

        // Set initial mute state and volume
        UpdateMuteState();
        UpdateVolume();
    }

    // Public methods for button actions
    public void PlayNext()
    {
        musicManager.PlayNextTrack();
        UpdateTrackName();
    }

    public void ToggleMute()
    {
        musicManager.ToggleMute();
        UpdateMuteState();
    }

    public void SetVolume(float volume)
    {
        musicManager.SetVolume(volume);
        UpdateVolume();
    }

    private void UpdateTrackName()
    {
        if (trackNameText != null)
        {
            string trackName = musicManager.GetCurrentTrackName();
            trackNameText.text = trackName;

            // Log track name to the console
            Debug.Log($"Currently Playing: {trackName}");
        }
    }

    private void UpdateMuteState()
    {
        // Update UI or any visual indicators related to mute state here if needed
        Debug.Log($"Muted: {musicManager.audioSource.mute}");
    }

    private void UpdateVolume()
    {
        // Update UI or any visual indicators related to volume here if needed
        Debug.Log($"Volume: {musicManager.audioSource.volume}");
    }
}
