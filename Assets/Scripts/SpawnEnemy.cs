using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    public List<GameObject> m_enemies = new List<GameObject>();
    #region Unity LifeCycle

    private void OnParticleSystemStopped()
    {
        if ( m_enemies.Count == 0 )
            return;

        GameObject enemy = m_enemies[ Random.Range( 0, m_enemies.Count ) ];
        Instantiate( enemy, transform.position, Quaternion.identity );
    }
    #endregion

}
