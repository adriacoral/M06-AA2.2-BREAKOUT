using UnityEngine;
using TMPro;

// GameManager: Controla el sistema de vidas, puntuación y estado general del juego
public class GameManager : MonoBehaviour
{
    // Singleton para acceso global
    public static GameManager Instance;

    [Header("Sistema de Vidas")]
    public int vidasActuales = 3;
    public int vidasIniciales = 3;
    public GameObject prefabPelota;
    public Transform puntoSpawnPelota; // Punto donde aparecen las pelotas

    [Header("Sistema de Puntuación")]
    public int puntuacion = 0;

    [Header("UI")]
    public TextMeshProUGUI textoVidas;
    public TextMeshProUGUI textoPuntuacion;
    public GameObject panelVictoria;
    public GameObject panelDerrota;

    [Header("Referencias")]
    public GeneradorBloques generadorBloques;

    private int bloquesTotales = 60;
    private int bloquesDestruidos = 0;
    private bool verificandoVidas = false;

    void Awake()
    {
        // Configurar Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        vidasActuales = vidasIniciales;
        puntuacion = 0;
        ActualizarUI();

        if (panelVictoria != null) panelVictoria.SetActive(false);
        if (panelDerrota != null) panelDerrota.SetActive(false);

        // Crear la primera pelota
        SpawnearPelota();
    }

    void Update()
    {
        // Verificar si no hay pelotas en pantalla (SOLO si el juego ya empezó)
        if (!verificandoVidas && vidasActuales > 0)
        {
            GameObject[] pelotas = GameObject.FindGameObjectsWithTag("Pelota");

            // Solo verificar si han pasado al menos 2 segundos desde el inicio
            if (Time.timeSinceLevelLoad > 2f && pelotas.Length == 0)
            {
                verificandoVidas = true;
                Invoke("PerderVida", 0.5f);
            }
        }
    }

    // Método llamado cuando la pelota cae
    public void PerderVida()
    {
        vidasActuales--;
        ActualizarUI();
        verificandoVidas = false;

        if (vidasActuales > 0)
        {
            // Crear nueva pelota
            SpawnearPelota();
        }
        else
        {
            // Game Over
            GameOver();
        }
    }

    // Añadir vidas (para powerups)
    public void AnadirVida(int cantidad = 1)
    {
        vidasActuales += cantidad;
        ActualizarUI();
    }

    // Sumar puntos cuando se destruye un bloque
    public void SumarPuntos(int puntos)
    {
        puntuacion += puntos;
        ActualizarUI();

        bloquesDestruidos++;

        // Verificar victoria
        if (bloquesDestruidos >= bloquesTotales)
        {
            Victoria();
        }
    }

    // Spawnear una pelota simple
    public void SpawnearPelota()
    {
        if (prefabPelota != null)
        {
            Vector3 posicion = puntoSpawnPelota != null ? puntoSpawnPelota.position : new Vector3(0, -3, 0);
            Instantiate(prefabPelota, posicion, Quaternion.identity);
        }
    }

    // Spawnear pelota en posición específica (para powerups)
    public GameObject SpawnearPelotaEn(Vector3 posicion)
    {
        if (prefabPelota != null)
        {
            GameObject pelota = Instantiate(prefabPelota, posicion, Quaternion.identity);
            return pelota;
        }
        return null;
    }

    // Actualizar textos de UI
    void ActualizarUI()
    {
        if (textoVidas != null)
        {
            textoVidas.text = "Vidas: " + vidasActuales;
        }

        if (textoPuntuacion != null)
        {
            textoPuntuacion.text = "Puntos: " + puntuacion;
        }
    }

    // Victoria
    void Victoria()
    {
        Debug.Log("¡Victoria! Puntuación final: " + puntuacion);
        if (panelVictoria != null)
        {
            panelVictoria.SetActive(true);
        }
        Time.timeScale = 0; // Pausar el juego
    }

    // Game Over
    void GameOver()
    {
        Debug.Log("Game Over. Puntuación final: " + puntuacion);
        if (panelDerrota != null)
        {
            panelDerrota.SetActive(true);
        }
        Time.timeScale = 0; // Pausar el juego
    }

    // Reiniciar juego
    public void ReiniciarJuego()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
}