using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    // Vitesse de déplacement de la balle
    public float m_moveSpeed = 10f;

    #region Unity LifeCycle

    void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Quand la balle est crée on lui donne sa vitesse pour qu'elle se déplace
        _rb2d.velocity = transform.up * m_moveSpeed;

        // La balle sera détruite dans 5 secondes
        Destroy( gameObject, 5f );
    }

    private void OnCollisionEnter2D( Collision2D collision )
    {
        Destroy( gameObject );
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        Destroy( gameObject );
    }

    #endregion

    private Rigidbody2D _rb2d;
}
