using UnityEngine;
using Random = UnityEngine.Random;

public class Music : MonoBehaviour
{
    AudioSource myAudio;
    public AudioClip[] myAnonymousMusic;
 
    void Start()
    {
        myAudio = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        myAudio.volume = PlayerPrefs.GetFloat("MusicVolume",0.6f);
        playRandomMyAnonymousMusic();
    }

    private static Music instance = null;

    public static Music Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
       // if (!myAudio.isPlaying) playRandomMyAnonymousMusic();
       if (!myAudio.isPlaying) // Remove when more songs
       {
           myAudio.clip = myAnonymousMusic[0];
           myAudio.Play();
       }
        if (Input.GetKeyDown(KeyCode.M)) playRandomMyAnonymousMusic();
        
    }
 
    void playRandomMyAnonymousMusic()
    {
        do {
            myAudio.clip = myAnonymousMusic[Random.Range(0, myAnonymousMusic.Length)];
        } while (myAudio.isPlaying == myAudio.clip);
        myAudio.Play();
    }
}
