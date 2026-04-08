using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] pajaros;
    public Transform puntoSpawn;

    private int indiceActual = 0;

    [Header("Puntaje")]
    public int puntaje = 0;
    public TextMeshProUGUI textoPuntaje;

    void Start()
    {
        ActualizarUI();
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
            puntoSpawn.position,
            Quaternion.identity
        );

        Resortera script = nuevo.GetComponent<Resortera>();
        script.manager = this;

        indiceActual++;
    }

    public void SumarPuntos(int puntos)
    {
        puntaje += puntos;
        ActualizarUI();
    }

    void ActualizarUI()
    {
        textoPuntaje.text = "Puntos: " + puntaje;
    }
}