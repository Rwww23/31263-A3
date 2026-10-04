using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip introMusic;
    public AudioClip ghostNormalMusic;
    float t;

    //void PlayGhostMusic(AudioClip current)
    
    void PlayGhostMusic()
    {
        //Debug.Log(Time.time);
        //audioSource.clip = current;
        audioSource.clip = ghostNormalMusic;
        audioSource.loop = true;
        audioSource.Play();
    } 
    
    void Start()
    {
        // no play button yet; just assume that means when the game is started

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = introMusic;
        audioSource.loop = false;
        audioSource.Play();
        Invoke(nameof(PlayGhostMusic), Mathf.Min(introMusic.length, 3f));
        
    }

    void Update()
    {

    }
}
