using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] pajaros;
    public Transform puntoAnclaje;

    private int indiceActual = 0;

    void Start()
    {
        CrearSiguientePajaro();
    }

    public void CrearSiguientePajaro()
    {
        if (indiceActual >= pajaros.Length)
        {
            Debug.Log("Se acabaron los pájaros");
            return;
        }

        GameObject nuevo = Instantiate(
            pajaros[indiceActual],
            puntoAnclaje.position,
            Quaternion.identity
        );

        Resortera script = nuevo.GetComponent<Resortera>();
        script.manager = this;

        indiceActual++;
    }
}