using UnityEngine;
using UnityEngine.Audio;
using System;

public class Audio : MonoBehaviour
{
    public Sound[] sounds ;
    void Awake(){
        foreach (Sound s  in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.volume = s.volume ;
            s.source.clip = s.clip;
        }
    }    
    public void play(string name){
        Sound s = Array.Find(sounds,sound => sound.name == name);
        s.source.Play();
    }
    public void play(string name , float volume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
        s.source.volume= s.volume * volume;
    }

}
