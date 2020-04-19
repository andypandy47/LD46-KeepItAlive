using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [FMODUnity.EventRef]
    public string musicEvent;
    public FMOD.Studio.EventInstance musicEventInstance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        musicEventInstance = FMODUnity.RuntimeManager.CreateInstance(musicEvent);
    }
    public void StartMusic()
    {
        musicEventInstance.start();
    }

    public void StopMusic()
    {
        musicEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void SetMusicState(float value)
    {
        musicEventInstance.setParameterByName("MusicState", value);
    }

    public void SetPause(bool value)
    {
        musicEventInstance.setPaused(value);
    }
}
