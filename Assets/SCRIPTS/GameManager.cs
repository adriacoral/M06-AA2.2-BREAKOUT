using UnityEngine;
using TMPro;

// GameManager: Controla el sistema de vidas, puntuación y estado general del juego
public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;

    [Header("Sistema de Vidas")]
    public int vidasActuales = 3;
    public int vidasIniciales = 3;
    public GameObject prefabPelota;
    public Transform puntoSpawnPelota;

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

        
        SpawnearPelota();
    }

    void Update()
    {
        
        if (!verificandoVidas && vidasActuales > 0)
        {
            GameObject[] pelotas = GameObject.FindGameObjectsWithTag("Pelota");

           
            if (Time.timeSinceLevelLoad > 2f && pelotas.Length == 0)
            {
                verificandoVidas = true;
                Invoke("PerderVida", 0.5f);
            }
        }
    }

   
    public void PerderVida()
    {
        vidasActuales--;
        ActualizarUI();
        verificandoVidas = false;

        if (vidasActuales > 0)
        {
            
            SpawnearPelota();
        }
        else
        {
            
            GameOver();
        }
    }

    
    public void AnadirVida(int cantidad = 1)
    {
        vidasActuales += cantidad;
        ActualizarUI();
    }

    
    public void SumarPuntos(int puntos)
    {
        puntuacion += puntos;
        ActualizarUI();

        bloquesDestruidos++;

        
        if (bloquesDestruidos >= bloquesTotales)
        {
            Victoria();
        }
    }

    
    public void SpawnearPelota()
    {
        if (prefabPelota != null)
        {
            Vector3 posicion = puntoSpawnPelota != null ? puntoSpawnPelota.position : new Vector3(0, -3, 0);
            Instantiate(prefabPelota, posicion, Quaternion.identity);
        }
    }

    
    public GameObject SpawnearPelotaEn(Vector3 posicion)
    {
        if (prefabPelota != null)
        {
            GameObject pelota = Instantiate(prefabPelota, posicion, Quaternion.identity);
            return pelota;
        }
        return null;
    }

    
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

    
    void Victoria()
    {
        Debug.Log("¡Victoria! Puntuación final: " + puntuacion);
        if (panelVictoria != null)
        {
            panelVictoria.SetActive(true);
        }
        Time.timeScale = 0; 
    }

    
    void GameOver()
    {
        Debug.Log("Game Over. Puntuación final: " + puntuacion);
        if (panelDerrota != null)
        {
            panelDerrota.SetActive(true);
        }
        Time.timeScale = 0; 
    }

    
    public void ReiniciarJuego()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
}