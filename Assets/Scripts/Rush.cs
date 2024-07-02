using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody2D))]
public class Rush : MonoBehaviour
{
    // Vitesse de déplacement
    public float m_moveSpeed = 3f;

    //Nombre de points que rapporte le monstre
    public int m_scorePoints = 500;

    [Header( "Audio Clips" )]
    public AudioClip m_deathSound;
    // Cible vers laquelle l'enemy va aller
    private GameObject _target;
    // Rigibody2D de l'enemey
    private Rigidbody2D _rb2d;
    // Son du monstre
    private AudioSource _audioSource;

    #region Unity LifeCycle
    void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        _target = GameObject.Find( "Player" );
    }

    void Update()
    {
        
    }

    private void OnDestroy()
    {
        GameManager._instance.EnemyDeath( m_scorePoints );
    }

    private void FixedUpdate()
    {
        if( _target != null )
        {
            Vector2 direction = _target.transform.position - transform.position;
            _rb2d.velocity = direction.normalized * m_moveSpeed;
        }
    }
    #endregion

    public void Death()
    {
        // Le Rigidbody est désactivé pour que le monstre ne soit plus physique
        _rb2d.simulated = false;
        _audioSource.PlayOneShot( m_deathSound );
        // Le GameObject est détruit à la fin de l'AudioClip
        Destroy( gameObject, m_deathSound.length );
    }

}
