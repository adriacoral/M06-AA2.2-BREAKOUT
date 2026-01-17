using UnityEngine;

// Script base para todos los powerups
// Los powerups caen con gravedad y el jugador debe recogerlos con la pala
public class Powerup : MonoBehaviour
{
    public float velocidadCaida = 2f;
    public Color colorPowerup = Color.white;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Configurar el color del powerup
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = colorPowerup;
        }
    }

    void Update()
    {
        // Caer hacia abajo
        transform.position += Vector3.down * velocidadCaida * Time.deltaTime;

        // Destruir si sale de la pantalla
        if (transform.position.y < -7)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si la pala recoge el powerup
        if (collision.CompareTag("Pala"))
        {
            ActivarPowerup();
            Destroy(gameObject);
        }
    }

    // Método virtual que será sobreescrito por cada tipo de powerup
    protected virtual void ActivarPowerup()
    {
        Debug.Log("Powerup activado");
    }
}
