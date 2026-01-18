using UnityEngine;

// Powerup AZUL: Multiplicar Bolas
public class PowerupMultiplicarBolas : Powerup
{
    void Start()
    {
        colorPowerup = Color.cyan;
        
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = colorPowerup;
        }
    }

    protected override void ActivarPowerup()
    {
        Debug.Log("¡Powerup Multiplicar Bolas activado!");
        
        if (GameManager.Instance != null)
        {
            GameObject[] pelotasActuales = GameObject.FindGameObjectsWithTag("Pelota");
            
            int cantidadPelotas = pelotasActuales.Length;
            
            GameManager.Instance.AnadirVida(cantidadPelotas);
            
            foreach (GameObject pelota in pelotasActuales)
            {
                if (pelota != null)
                {
                    Vector3 posicion = pelota.transform.position;
                    GameObject nuevaPelota = GameManager.Instance.SpawnearPelotaEn(posicion);
                    
                    if (nuevaPelota != null)
                    {
                        PelotaBreakout movimiento = nuevaPelota.GetComponent<PelotaBreakout>();
                        if (movimiento != null)
                        {
                            movimiento.direccion = new Vector3(
                                Random.Range(-1.0f, 1.0f),
                                Random.Range(-1.0f, 1.0f),
                                0
                            ).normalized;
                        }
                    }
                }
            }
            
            Debug.Log($"Se duplicaron {cantidadPelotas} bolas. Total ahora: {cantidadPelotas * 2}");
        }
    }
}
