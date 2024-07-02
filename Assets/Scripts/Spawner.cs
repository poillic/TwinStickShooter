using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public float m_radius = 5f;

    public GameObject m_spawnEffect;

    [Header( "Spawn Parameters" )]
    public float m_initialSpawnInterval = 3f;
    public float m_spawnIntervalReduction = 0.1f;
    public float m_spawnIntervalMinimum = 0.3f;
    private float _currentSpawnInterval;

    private float _nextSpawnTime;

#region Unity LifeCycle
    void Awake()
    {
        
    }

    void Start()
    {
        _currentSpawnInterval = m_initialSpawnInterval;
        _nextSpawnTime = _currentSpawnInterval;
    }

    void Update()
    {
        if( Time.time >= _nextSpawnTime )
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        Vector2 spawnPosition = Random.insideUnitCircle * m_radius;
        spawnPosition.x += transform.position.x;
        spawnPosition.y += transform.position.y;

        Instantiate( m_spawnEffect, spawnPosition, Quaternion.identity );

        _currentSpawnInterval = Mathf.Max( _currentSpawnInterval - m_spawnIntervalReduction, m_spawnIntervalMinimum );
        _nextSpawnTime = Time.time + _currentSpawnInterval;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere( transform.position, m_radius );
    }
    #endregion

}
