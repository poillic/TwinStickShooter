using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

#region Unity LifeCycle
    void Awake()
    {
        
    }

    void Start()
    {
    }

    void Update()
    {
        
    }
#endregion

    public void ReloadScene()
    {
        SceneManager.LoadScene( SceneManager.GetActiveScene().name );
    }

    public void GoToScene( string sceneName )
    {
        SceneManager.LoadScene( sceneName );
    }

    public void Quit()
    {
        Application.Quit();
    }
}
