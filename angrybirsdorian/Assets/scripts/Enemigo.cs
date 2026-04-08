using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float resistencia = 5f;
    public float vida = 10f;
    public int puntos = 100;

    private GameManager manager;

    void Start()
    {
        manager = FindFirstObjectByType<GameManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        float fuerzaImpacto = collision.relativeVelocity.magnitude;

        if (fuerzaImpacto > resistencia)
        {
            vida -= fuerzaImpacto;

            if (vida <= 0)
            {
                Destruir();
            }
        }
    }

    void Destruir()
    {
        if (manager != null)
        {
            manager.SumarPuntos(puntos);
        }

        Destroy(gameObject);
    }
}