using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Events;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager _instance = null;

    [Range(0.1f, 1f)]
    public float m_bulletTimeScale = 0.5f;
    public int m_score = 0;

    public UnityEvent OnStartBulletTime = new UnityEvent();
    public UnityEvent OnStopBulletTime = new UnityEvent();
    public AudioMixer m_audioMixer;

    [Header("UI Elements")]
    public GameObject m_PausePanel;
    public TextMeshProUGUI m_scoreLabel;
    public TextMeshProUGUI m_loseScreenScoreLabel;

#region Unity LifeCycle
    void Awake()
    {
        if( _instance != null )
        {
            Destroy( gameObject );
        }
        else
        {
            _instance = this;
        }

    }

    #endregion

    public void EnemyDeath(int score)
    {
        m_score += score;
        m_scoreLabel.text = m_score.ToString();
        m_loseScreenScoreLabel.text = m_score.ToString();
    }

    public void Pause()
    {
        // Si le game est en pause on le réactive
        // Si le game est actif on le met en pause
        if( _isGamePaused )
        {
            Time.timeScale = _timeScaleTmp;
            _isGamePaused = false;
            m_PausePanel.SetActive( false );
        }
        else
        {
            m_PausePanel.SetActive( true );
            _timeScaleTmp = Time.timeScale;
            Time.timeScale = 0f;
            _isGamePaused = true;
        }
    }

    private void BulletTime()
    {
        // Si le jeu est en pause on ne fait rien
        if ( _isGamePaused )
            return;

        if( _isBulletTimeActive )
        {
            _timeScaleTmp = Time.timeScale;
            Time.timeScale = m_bulletTimeScale;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void BulletTimeInput( InputAction.CallbackContext ctx )
    {
        switch( ctx.action.phase )
        {
            case InputActionPhase.Started:
                _isBulletTimeActive = true;
                BulletTime();
                OnStartBulletTime.Invoke();
                break;
            case InputActionPhase.Canceled:
                _isBulletTimeActive = false;
                OnStopBulletTime.Invoke();
                BulletTime();
                break;
        }
    }
    

    public void SetMainMixerPitch( float value )
    {
        value = Mathf.Clamp01( value );
        m_audioMixer.SetFloat( "MainPitch", value );
    }

    private float _timeScaleTmp = 0f;
    private bool _isGamePaused = false;
    private bool _isBulletTimeActive = false;
}
