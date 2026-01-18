using UnityEngine;

// Script para los bloques individuales
public class BloqueJuego : MonoBehaviour
{
    public int valorPuntos = 1;
    public Color colorBloque;
    
    [Header("Powerups")]
    public GameObject[] prefabsPowerups; // Array con los 3 tipos de powerups
    public float probabilidadPowerup = 0.1f; // 10%
    
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = colorBloque;
        }
    }

    public int ObtenerPuntos()
    {
        return valorPuntos;
    }

    public void IntentarSpawnearPowerup()
    {
        if (prefabsPowerups != null && prefabsPowerups.Length > 0)
        {
            float random = Random.Range(0f, 1f);
            
            if (random < probabilidadPowerup)
            {
                int indicePowerup = Random.Range(0, prefabsPowerups.Length);
                
                if (prefabsPowerups[indicePowerup] != null)
                {
                    Instantiate(prefabsPowerups[indicePowerup], transform.position, Quaternion.identity);
                }
            }
        }
    }
}
