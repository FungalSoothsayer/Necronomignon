using UnityEngine.Audio;
using UnityEngine;
// Since we removed MonoBehavior, this is important so Unity Recognize the script
[System.Serializable]
public class Sound 
{
    //This class  hold the attributes of a Sound object 

    public string name;

    //This is the Mp3 clip 
    public AudioClip clip;

    // These are to control volume and pitch of the Clip
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;
    
}
