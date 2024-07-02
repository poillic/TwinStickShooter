using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSaver : MonoBehaviour
{
    public string m_propertyName = "";
    public float m_defaultValue = 0.75f;
    public AudioMixer m_audioMixer;
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.minValue = 0.0001f;
    }

    void Start()
    {
        if( PlayerPrefs.HasKey(m_propertyName) )
        {
            Load();
        }
        else
        {
            Save( m_defaultValue );
        }
    }

    public void Load() 
    {
        float value = PlayerPrefs.GetFloat( m_propertyName );
        _slider.value = value;
    }

    public void Save( float value )
    {
        m_audioMixer.SetFloat( m_propertyName, Mathf.Log10( value ) * 20f );
        PlayerPrefs.SetFloat( m_propertyName, value );
    }
}
