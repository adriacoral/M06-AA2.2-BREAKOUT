using UnityEngine;

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
        
        transform.position = transform.position + direccion * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Vertical"))
        {
            direccion.x = direccion.x * -1;
        }

        
        if (collision.CompareTag("Horizontal"))
        {
            direccion.y = direccion.y * -1;
        }

        
        if (collision.CompareTag("Pala"))
        {
            ReboteDireccional(collision);
        }

       
        if (collision.CompareTag("Bloque"))
        {
            ReboteDireccional(collision);

            
            Destroy(collision.gameObject);
        }

       
        if (collision.CompareTag("ZonaMuerte"))
        {
            Debug.Log("¡Pelota perdida!");
            
            Destroy(gameObject);
        }
    }

    
    void ReboteDireccional(Collider2D collision)
    {
        
        Vector3 puntoImpacto = transform.position - collision.transform.position;
        puntoImpacto.Normalize();

        
        direccion = puntoImpacto;

        
        if (collision.CompareTag("Pala") && direccion.y < 0)
        {
            direccion.y = -direccion.y;
        }
    }

    
    void Spawn()
    {
        
        transform.position = new Vector3(0, -3, 0);

        
        direccion = new Vector3(
            Random.Range(-1.0f, 1.0f),
            Random.Range(0.5f, 1.0f),  // Siempre hacia arriba
            0
        );

        // Normalizar para mantener velocidad constante
        direccion.Normalize();
    }
}