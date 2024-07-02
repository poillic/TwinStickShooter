using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class PlayerShoot : MonoBehaviour
{
    // Le prefab qui sera utilis� pour les balles
    public GameObject m_bulletPrefab;
    // Interval de temps entre chaque tir de balle
    public float m_fireRateInterval = 0.1f;

    //AudioSource pour jouer le son des tirs
    private AudioSource _audioSource;

    #region Unity LifeCycle

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // On v�rifie si le player � le droit de tirer
        if( _isShooting && _nextTimeToShoot <= Time.time )
        {
            // La nouvelle balle est cr�e � la position du joueur et avec la m�me rotation
            Instantiate( m_bulletPrefab, transform.position, transform.rotation );
            // Le nouveau temps auquel la prochaine balle sera cr�e est calcul�
            _nextTimeToShoot = Time.time + m_fireRateInterval;

            // Le son du tir est jou�
            PlaySound();
        }
    }
    #endregion

    private void PlaySound()
    {
        // Pour chaque tir le volume et me pitch sont l�g�rement modifi�s pour �viter la monotonie de jouer le m�me
        // effet sonore � chaque fois
        _audioSource.volume = 0.8f + Random.Range( -0.2f, 0.2f );
        _audioSource.pitch = 0.8f + Random.Range( -0.2f, 0.2f );
        _audioSource.Play();
    }

    public void Shoot( InputAction.CallbackContext ctx )
    {
        switch( ctx.action.phase )
        {
            case InputActionPhase.Started:
                // Si l'action vient de commencer alors le joueur commence � tirer
                _isShooting = true;
                break;
            case InputActionPhase.Canceled:
                // L'action est termine le joueur ne tire plus
                _isShooting = false;
                break;
        }
    }

    // Bool�en pour savoir si le joueur est en train de tirer
    private bool _isShooting = false;
    // Variable qui va contenir le temps auquel le joueur pourra tirer
    private float _nextTimeToShoot = 0f;
}
