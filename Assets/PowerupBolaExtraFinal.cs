using UnityEngine;

// Powerup ROJO: Bola Extra
public class PowerupBolaExtra : Powerup
{
    void Start()
    {
        colorPowerup = Color.red;
        
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = colorPowerup;
        }
    }

    protected override void ActivarPowerup()
    {
        Debug.Log("¡Powerup Bola Extra activado!");
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AnadirVida(1);
            
            Vector3 posicion = new Vector3(0, 0, 0);
            GameObject pelota = GameManager.Instance.SpawnearPelotaEn(posicion);
            
            if (pelota != null)
            {
                PelotaBreakout movimiento = pelota.GetComponent<PelotaBreakout>();
                if (movimiento != null)
                {
                    movimiento.direccion = new Vector3(
                        Random.Range(-1.0f, 1.0f),
                        Random.Range(0.5f, 1.0f),
                        0
                    ).normalized;
                }
            }
        }
    }
}
