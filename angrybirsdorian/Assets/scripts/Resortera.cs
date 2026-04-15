using UnityEngine;
using UnityEngine.InputSystem;

public class Resortera : MonoBehaviour
{
    public float maxDistancia = 1f;
    public float fuerza = 10f;
    private Controles inputActions;
    

    private bool yaAviso = false;
    private float tiempoDesdeLanzamiento = 0f;
    private float tiempoQuieto = 0f;

    public float tiempoMinimo = 2f;
    public float velocidadMinima = 1.5f;
    public float tiempoParaSiguiente = 1f;

    private Rigidbody2D rb;
    private bool arrastrando = false;
    private bool lanzado = false;

    public GameManager manager;

    private Vector2 puntoAnclaje;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;

        puntoAnclaje = transform.position;
    }

    void Update()
    {
        
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

            if (rb.linearVelocity.magnitude < velocidadMinima)
            {
                tiempoQuieto += Time.deltaTime;
            }
            else
            {
                tiempoQuieto = 0f;
            }

            if (tiempoDesdeLanzamiento > tiempoMinimo && tiempoQuieto > tiempoParaSiguiente)
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
      
    }
    private void OnEnable()
    {
        inputActions.PajarosVScerdos.Enable();
        inputActions.PajarosVScerdos.Presionado.started += LePicamos;
        inputActions.PajarosVScerdos.Presionado.canceled += YaNo;
        inputActions.PajarosVScerdos.Posicion.ReadValue<Vector2>();

    }

    void LePicamos(InputAction.CallbackContext handler)
    {
        print("jaja click");
    }
    void YaNo(InputAction.CallbackContext handler)
    {
        print("jaja soltaste");
    }
    void OnMouseDown()
    {
        if (lanzado) return;

        arrastrando = true;
    }
    private void Awake()
    {
        inputActions = new Controles();
       
    }


    void Lanzar()
    {
        arrastrando = false;
        lanzado = true;

        rb.bodyType = RigidbodyType2D.Dynamic;

        Vector2 direccion = puntoAnclaje - (Vector2)transform.position;
        rb.linearVelocity = direccion * fuerza;
    }
}