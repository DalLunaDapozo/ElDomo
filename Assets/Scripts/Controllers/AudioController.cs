using UnityEngine.Audio;
using UnityEngine;
using System;


public class AudioController : MonoBehaviour
{
    public static AudioController instance = null;
    [SerializeField] private AudioSource trackSource = null;
    [SerializeField] private AudioSource sfxSource = null;
    [SerializeField] private AudioLowPassFilter lowPassFilter = null;

    [SerializeField] private float volumeTransitionTime = 1.0f;
    [SerializeField] private float pitchTransitionTime = 1.0f;
    [SerializeField] private AudioClip[] tracks = null;


    [SerializeField] private float[] modeVolumes = null;
    [SerializeField] private float[] pitchVolumes = null;

    public enum VOLUME_MODE { MINIMAL, NORMAL, HIGH }
    public enum PITCH_MODE { MINIMAL, NORMAL, HIGH }

    public enum TRACK { MENU, GAMEPLAY }

    private FloatLerper volumeLerper = null;
    private FloatLerper pitchLerper = null;

    #region UNITY_CALLS
    private void Awake()
    {
        instance = this;
        volumeLerper = new FloatLerper(volumeTransitionTime, AbstractLerper<float>.SMOOTH_TYPE.STEP_SMOOTHER);
        pitchLerper = new FloatLerper(pitchTransitionTime, AbstractLerper<float>.SMOOTH_TYPE.STEP_SMOOTHER);
    }

    private void Start()
    {
        SetTrack(0);
        trackSource.volume = 0.05f;
    }

    private void Update()
    {

        sfxSource.volume = 0.1f;
    }
    #endregion

    #region PUBLIC_METHODS
    public void SetVolumeInstant(int mode)
    {
        trackSource.volume = modeVolumes[mode];
    }

    public void SetVolumeMode(VOLUME_MODE mode)
    {
        volumeLerper.SetValues(trackSource.volume, modeVolumes[(int)mode], true);
    }

    public void SetVolumeMode(int mode)
    {
        volumeLerper.SetValues(trackSource.volume, modeVolumes[mode], true);
    }

    public void SetPitchInstant(int mode)
    {
        lowPassFilter.cutoffFrequency = pitchVolumes[mode];
    }

    public void SetPitchMode(int mode)
    {
        pitchLerper.SetValues(lowPassFilter.cutoffFrequency, pitchVolumes[mode], true);
    }

    public void SetTrack(int track)
    {
        trackSource.clip = tracks[track];
        trackSource.Play();
    }

    public void PlaySound(AudioClip clip, float volume = 1.0f, float pitch = 1.0f)
    {   
        sfxSource.volume = volume;
        sfxSource.pitch = pitch;

        sfxSource.PlayOneShot(clip);
    }
    #endregion

    #region PRIVATE_METHODS
    private void Initialize()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StopMusic()
    {
        trackSource.Stop();
    }

    public void SwitchMusic(bool status)
    {
        if (status)
        {
            trackSource.Play();
        }
        else
        {
            if (trackSource.isPlaying)
            {
                trackSource.Pause();
            }
            else
            {
                trackSource.Stop();
            }
        }

       
    }
    #endregion
}

