using UnityEngine;
using UnityEngine.InputSystem;

public class PieceHandler : MonoBehaviour

{

    private Controles misControles;
    private GameObject piezaSelecconada;

    private void Awake()

    {
        misControles = new Controles();
    }

    private void OnEnable()

    {

        misControles.PajarosVScerdos.Enable();
        misControles.PajarosVScerdos.Presionado.started += Presiono;
        misControles.PajarosVScerdos.Presionado.canceled += Solto;
    }

    private void Presiono(InputAction.CallbackContext handler)

    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(

            misControles.PajarosVScerdos.Posicion.ReadValue<Vector2>()
        );
        RaycastHit2D golpeo = Physics2D.Raycast(mousePos, Vector2.zero);

        if (golpeo)

        {

            piezaSelecconada = golpeo.collider.gameObject;
        }
    }

    private void Solto(InputAction.CallbackContext handler)

    {
        piezaSelecconada = null; 
    }

    void Update()

    {
        if (piezaSelecconada != null)

        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(
                misControles.PajarosVScerdos.Posicion.ReadValue<Vector2>()
            );
            piezaSelecconada.transform.position = mousePos; 

        }

    }

}