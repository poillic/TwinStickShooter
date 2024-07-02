using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Ajout de la lib pour utiliser les évènements
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    // Vie de départ de l'entité
    public int m_startLife = 1;
    
    // Permet de préciser quel tag peut infliger des dégats
    public string m_damageSourceTag = "";

    // Evènement à déclencher quand l'entité meurt
    public UnityEvent OnDeath = new UnityEvent();

    // Evénement à déclencher quand l'entité se fait toucher
    public UnityEvent OnHit = new UnityEvent();

    // Vie courante
    private int _currentLife = 0;

    #region Unity LifeCycle

    void Awake()
    {
        // Au début la vie courante est égale à la vie de départ
        _currentLife = m_startLife;
    }

    #endregion

    // Fonction à appeler dans l'entité doit perdre de la vie
    public void Hit( int damage )
    {
        // Les dégats sont déduits de la vie courante
        _currentLife -= damage;
        
        // Si la vie courante est inférieure ou égal à 0 alors on déclenche l'évènement
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
