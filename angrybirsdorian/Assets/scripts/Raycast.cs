using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Raycast : MonoBehaviour

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
        misControles.PajarosVScerdos.Presionado.started += Presiono;//Suscripción
        misControles.PajarosVScerdos.Presionado.canceled += Solto;
    }

    private void Presiono(InputAction.CallbackContext handler)
    {
        Vector2 pixelesACoord = Camera.main.ScreenToWorldPoint(misControles.PajarosVScerdos.Posicion.ReadValue<Vector2>());
        RaycastHit2D golpeo = Physics2D.Raycast(pixelesACoord, pixelesACoord);
        if (golpeo)
        {
            print("Le pegue a algo, Yohoo");
            piezaSelecconada = golpeo.collider.gameObject;
        }
    }

    private void Solto(InputAction.CallbackContext handler)
    {
        piezaSelecconada = null;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
