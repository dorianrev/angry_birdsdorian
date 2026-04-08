using UnityEngine;

public class Resortera : MonoBehaviour
{
    public float maxDistancia = 1f;
    public float fuerza = 10f;

    private bool yaAviso = false;
    private float tiempoDesdeLanzamiento = 0f;

    public float tiempoMinimo = 2f;
    public float velocidadMinima = 0.15f;

    private Rigidbody2D rb;
    private bool arrastrando = false;
    private bool lanzado = false;

    public GameManager manager;

    private Vector2 puntoAnclaje;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.bodyType = RigidbodyType2D.Kinematic; // 👈 SIEMPRE inicia así
        rb.linearVelocity = Vector2.zero;

        puntoAnclaje = transform.position;
    }

    void Update()
    {
        // 👇 Asegura que mientras no esté lanzado, no tenga física
        if (!lanzado)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
        }

        if (arrastrando)
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = 0;

            Vector2 direccion = (Vector2)mouse - puntoAnclaje;

            if (direccion.magnitude > maxDistancia)
            {
                direccion = direccion.normalized * maxDistancia;
            }

            transform.position = puntoAnclaje + direccion;
        }

        if (Input.GetMouseButtonUp(0) && arrastrando)
        {
            Lanzar();
        }

        if (lanzado && !yaAviso)
        {
            tiempoDesdeLanzamiento += Time.deltaTime;

            if (tiempoDesdeLanzamiento > tiempoMinimo && rb.linearVelocity.magnitude < velocidadMinima)
            {
                yaAviso = true;
                Invoke("AvisarManager", 0.5f);
            }
        }
    }

    void AvisarManager()
    {
        if (manager != null)
        {
            manager.CrearSiguientePajaro();
        }
        else
        {
            Debug.LogError("Manager no asignado");
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

        rb.bodyType = RigidbodyType2D.Dynamic; // 👈 aquí se activa la física

        Vector2 direccion = puntoAnclaje - (Vector2)transform.position;
        rb.linearVelocity = direccion * fuerza;
    }
}