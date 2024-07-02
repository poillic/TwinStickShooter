using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public InputActionAsset actions;
    public float m_moveSpeed = 5f;

    #region Unity LifeCycle
    void Awake()
    {
        // Le Rigidbody2D est stock� dans la variable
        _rb2d = GetComponent<Rigidbody2D>();
        // Le gravityScale est mis � 0, il est aussi possible de le faire via l'Editor
        _rb2d.gravityScale = 0;

    }

    private void FixedUpdate()
    {
        // La v�locit� du player est cacul�e en fonction de la direction et de la vitesse
        _rb2d.velocity = _direction * m_moveSpeed;

        // Si la magnitude du vecteur _orientation est � 0 �a veut que le joueur ne touche pas le stick
        // Donc on regarde si le joueur touche pour effectuer la rotation
        if ( _orientation.magnitude > 0f )
        {
            // On oriente le joueur pour qu'il regarde au bon endroit
            Quaternion rotation = Quaternion.LookRotation( _orientation, Vector3.back );
            _rb2d.SetRotation( rotation );
        }
    }
    #endregion

    // Fonction utilis�e dans le component Player Input pour r�cup�rer les informations du mouvement
    public void GetMoveInput( InputAction.CallbackContext ctx )
    {
        _direction = ctx.ReadValue<Vector2>();
    }

    // Fonction utilis�e dans le component Player Input pour r�cup�rer les informations de l'orientation
    public void GetOrientationInput( InputAction.CallbackContext ctx )
    {
        _orientation = ctx.action.ReadValue<Vector2>();
    }

    // Variable utilis�e pour stocker la direction dans laquelle se d�place le joueur
    private Vector2 _direction = new Vector2();
    // Variable utilis�e pour stocker le point vers lequel le joueur regardes
    private Vector2 _orientation = new Vector2();
    private Rigidbody2D _rb2d;
}
