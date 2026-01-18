using UnityEngine;

public class Powerup : MonoBehaviour
{
    public float velocidadCaida = 2f;
    public Color colorPowerup = Color.white;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
       
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

        
        if (transform.position.y < -7)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Pala"))
        {
            ActivarPowerup();
            Destroy(gameObject);
        }
    }

    
    protected virtual void ActivarPowerup()
    {
        Debug.Log("Powerup activado");
    }
}
