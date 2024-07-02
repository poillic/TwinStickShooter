using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Ajout de la lib pour utiliser les �v�nements
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    // Vie de d�part de l'entit�
    public int m_startLife = 1;
    
    // Permet de pr�ciser quel tag peut infliger des d�gats
    public string m_damageSourceTag = "";

    // Ev�nement � d�clencher quand l'entit� meurt
    public UnityEvent OnDeath = new UnityEvent();

    // Ev�nement � d�clencher quand l'entit� se fait toucher
    public UnityEvent OnHit = new UnityEvent();

    // Vie courante
    private int _currentLife = 0;

    #region Unity LifeCycle

    void Awake()
    {
        // Au d�but la vie courante est �gale � la vie de d�part
        _currentLife = m_startLife;
    }

    #endregion

    // Fonction � appeler dans l'entit� doit perdre de la vie
    public void Hit( int damage )
    {
        // Les d�gats sont d�duits de la vie courante
        _currentLife -= damage;
        
        // Si la vie courante est inf�rieure ou �gal � 0 alors on d�clenche l'�v�nement
        if( _currentLife <= 0 )
        {
            OnDeath.Invoke();
        }
        else
        {
            OnHit.Invoke();
        }
    }

    private void OnCollisionEnter2D( Collision2D collision )
    {
        if ( collision.collider.CompareTag( m_damageSourceTag ) )
        {
            Hit( 1 );
        }
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if ( collision.CompareTag( m_damageSourceTag ) )
        {
            Hit( 1 );
        }
    }
}
