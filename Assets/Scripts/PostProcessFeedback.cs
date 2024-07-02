using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(PostProcessVolume))]
public class PostProcessFeedback : MonoBehaviour
{

    private PostProcessVolume _ppVolume;

    [Header( "Vignette" )]
    [Range(0f,1f)]
    public float m_vg_intesity = 0.5f;
    public float m_vg_fadeInDuration = 0.3f;

    [Header( "Grayscale" )]
    [Range( -100, 100 )]
    public int m_cg_saturation = -80;
    public float m_cg_fadeInDuration = 0.2f;

    private Vignette vignette;
    private ColorGrading grading;

#region Unity LifeCycle
    void Awake()
    {
        _ppVolume = GetComponent<PostProcessVolume>();
    }

    void Start()
    {
        _ppVolume.sharedProfile.TryGetSettings<Vignette>( out vignette );
        _ppVolume.sharedProfile.TryGetSettings<ColorGrading>( out grading );
    }

    void Update()
    {
        
    }
#endregion

    public void OnHit()
    {
        StartCoroutine( Vignette() );
    }

    IEnumerator Vignette()
    {
        vignette.enabled.value = true;
        float timer = 0f;

        while( timer < m_vg_fadeInDuration )
        {
            vignette.intensity.value = Mathf.Lerp(0f, m_vg_intesity, timer/m_vg_fadeInDuration );
            timer += Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }
        vignette.enabled.value = false;
        vignette.intensity.value = 0f;
    }

    public void OnBulletTime()
    {
        StartCoroutine( Grayscale() );
    }

    public void ResetBulletTime()
    {
        grading.saturation.value = 0;
        grading.enabled.value = false;
    }

    IEnumerator Grayscale()
    {
        grading.enabled.value = true;
        float timer = 0f;

        while ( timer < m_cg_fadeInDuration )
        {
            grading.saturation.value = Mathf.Lerp( 0, m_cg_saturation, timer / m_cg_fadeInDuration );
            timer += Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
