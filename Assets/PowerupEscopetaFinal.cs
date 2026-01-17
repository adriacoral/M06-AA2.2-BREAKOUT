using UnityEngine;

// Powerup VERDE: Escopeta
public class PowerupEscopeta : Powerup
{
    void Start()
    {
        colorPowerup = Color.green;
        
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = colorPowerup;
        }
    }

    protected override void ActivarPowerup()
    {
        Debug.Log("¡Powerup Escopeta activado!");
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AnadirVida(3);
            
            GameObject pala = GameObject.FindGameObjectWithTag("Pala");
            Vector3 posicionPala = pala != null ? pala.transform.position : new Vector3(0, -5, 0);
            
            // Bola izquierda
            GameObject pelotaIzq = GameManager.Instance.SpawnearPelotaEn(posicionPala);
            if (pelotaIzq != null)
            {
                PelotaBreakout mov = pelotaIzq.GetComponent<PelotaBreakout>();
                if (mov != null)
                {
                    mov.direccion = new Vector3(-0.7f, 0.7f, 0).normalized;
                }
            }
            
            // Bola centro
            GameObject pelotaCentro = GameManager.Instance.SpawnearPelotaEn(posicionPala);
            if (pelotaCentro != null)
            {
                PelotaBreakout mov = pelotaCentro.GetComponent<PelotaBreakout>();
                if (mov != null)
                {
                    mov.direccion = new Vector3(0, 1, 0).normalized;
                }
            }
            
            // Bola derecha
            GameObject pelotaDer = GameManager.Instance.SpawnearPelotaEn(posicionPala);
            if (pelotaDer != null)
            {
                PelotaBreakout mov = pelotaDer.GetComponent<PelotaBreakout>();
                if (mov != null)
                {
                    mov.direccion = new Vector3(0.7f, 0.7f, 0).normalized;
                }
            }
        }
    }
}
