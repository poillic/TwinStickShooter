using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] m_musics;

    private AudioSource _audioSource;

    private int _currentMusic = 0;

    public static MusicPlayer _instance;

    #region Unity LifeCycle
    void Awake()
    {
        if(_instance != null )
        {
            Destroy( gameObject );
        }
        else
        {
            _instance = this;
        }

        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        DontDestroyOnLoad( gameObject );
    }

    void Start()
    {

        if ( m_musics.Length == 0 )
        {
            Debug.LogError( "Il n'y a pas de musiques à jouer" );
            return;
        }

        PlayNextMusic();
    }

    void Update()
    {
        if( !_audioSource.isPlaying )
        {
            PlayNextMusic();
        }
    }

    public void PlayNextMusic()
    {
        _audioSource.PlayOneShot( m_musics[ _currentMusic ] );
        _currentMusic++;

        _currentMusic = _currentMusic % m_musics.Length;
    }
    #endregion

    
}
