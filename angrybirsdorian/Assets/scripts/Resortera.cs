using UnityEngine;

public class Resortera : MonoBehaviour
{
    public Transform puntoAnclaje;
    public float maxDistancia = 1f;
    public float fuerza = 10f;

    private Rigidbody2D rb;
    private bool arrastrando = false;
    private bool lanzado = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.position = puntoAnclaje.position;
    }

    void Update()
    {
        if (arrastrando)
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = 0;

            Vector2 direccion = mouse - puntoAnclaje.position;

            if (direccion.magnitude > maxDistancia)
            {
                direccion = direccion.normalized * maxDistancia;
            }

            transform.position = (Vector2)puntoAnclaje.position + direccion;
        }

        if (Input.GetMouseButtonUp(0) && arrastrando)
        {
            Lanzar();
        }
    }

    void OnMouseDown()
    {
        if (lanzado) return;

        arrastrando = true;
    }

    void Lanzar()
    {
        arrastrando = false;
        lanzado = true;

        rb.bodyType = RigidbodyType2D.Dynamic;

        Vector2 direccion = (Vector2)puntoAnclaje.position - (Vector2)transform.position;
        rb.linearVelocity = direccion * fuerza;
    }
}