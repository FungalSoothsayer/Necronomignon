using UnityEngine.Audio;
using System;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    //THIS ARRAY HOLDS ALL THE SOUNDS IN THE GAME
    public Sound[] sounds;


    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);


        foreach (Sound s in sounds)
        {
           s.source = gameObject.AddComponent<AudioSource>();
           s.source.clip = s.clip;

           s.source.volume = s.volume;
           s.source.pitch = s.pitch;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Play(string name)
    {
        print(name + "THIS IS THE STRING NAME");
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            
            s.source.Play();
        }
    }
    
}
