using UnityEngine;

// Script de movimiento de pelota para Breakout - Versión Final
// Incluye rebotes con paredes, pala y bloques con física direccional
public class PelotaBreakout : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 direccion; // Público para que los powerups puedan modificarlo

    void Start()
    {
        InicializarDireccion();
    }

    void Update()
    {
        // Mover la pelota
        transform.position = transform.position + direccion * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Rebote con paredes verticales (izquierda y derecha)
        if (collision.CompareTag("Vertical"))
        {
            direccion.x = direccion.x * -1;
        }

        // Rebote con pared superior
        if (collision.CompareTag("Horizontal"))
        {
            direccion.y = direccion.y * -1;
        }

        // Rebote con la pala (direccional según punto de impacto)
        if (collision.CompareTag("Pala"))
        {
            ReboteDireccional(collision);
        }

        // Rebote con bloques (direccional según punto de impacto)
        if (collision.CompareTag("Bloque"))
        {
            ReboteDireccional(collision);
            
            // Obtener puntos del bloque antes de destruirlo
            BloqueJuego bloque = collision.GetComponent<BloqueJuego>();
            if (bloque != null)
            {
                int puntos = bloque.ObtenerPuntos();
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.SumarPuntos(puntos);
                }
                
                // Intentar spawnear powerup antes de destruir
                bloque.IntentarSpawnearPowerup();
            }
            
            // Destruir el bloque
            Destroy(collision.gameObject);
        }

        // Si la pelota cae por la parte inferior (perder vida)
        if (collision.CompareTag("ZonaMuerte"))
        {
            Debug.Log("¡Pelota perdida!");
            Destroy(gameObject);
        }
    }

    // Rebote direccional: según donde impacte, la pelota irá en una dirección diferente
    void ReboteDireccional(Collider2D collision)
    {
        // Calcular la dirección del rebote basada en la posición de impacto
        Vector3 puntoImpacto = transform.position - collision.transform.position;
        puntoImpacto.Normalize();
        
        // Asignar la nueva dirección
        direccion = puntoImpacto;
        
        // Asegurar que la pelota siempre suba después de rebotar con la pala
        if (collision.CompareTag("Pala") && direccion.y < 0)
        {
            direccion.y = -direccion.y;
        }
    }

    // Inicializar dirección aleatoria
    void InicializarDireccion()
    {
        direccion = new Vector3(
            Random.Range(-1.0f, 1.0f),
            Random.Range(0.5f, 1.0f),
            0
        ).normalized;
    }
}
