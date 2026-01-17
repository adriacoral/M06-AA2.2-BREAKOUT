using UnityEngine;

// Script adaptado del Pong para el movimiento de la pelota en Breakout
// Incluye rebotes con paredes, pala y bloques con física direccional
public class MovimientoPelotaBreakout : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 direccion;

    void Start()
    {
        Spawn();
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

            // Destruir el bloque
            Destroy(collision.gameObject);
        }

        // Si la pelota cae por la parte inferior (Game Over)
        if (collision.CompareTag("ZonaMuerte"))
        {
            Debug.Log("¡Pelota perdida!");
            // Aquí se manejará el sistema de vidas más adelante
            Destroy(gameObject);
        }
    }

    // Rebote direccional: según donde impacte, la pelota irá en una dirección diferente
    // Si impacta en el centro: más vertical
    // Si impacta en los extremos: más horizontal
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

    // Función para hacer aparecer la pelota en el centro con dirección aleatoria
    void Spawn()
    {
        // Posición inicial cerca de la pala (más abajo para evitar colisión con bloques)
        transform.position = new Vector3(0, -3, 0);

        // Dirección aleatoria
        direccion = new Vector3(
            Random.Range(-1.0f, 1.0f),
            Random.Range(0.5f, 1.0f),  // Siempre hacia arriba
            0
        );

        // Normalizar para mantener velocidad constante
        direccion.Normalize();
    }
}