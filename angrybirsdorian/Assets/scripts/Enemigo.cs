using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float resistencia = 5f; // qué tan fuerte debe ser el golpe
    public float vida = 10f;

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
        Destroy(gameObject);
    }
}