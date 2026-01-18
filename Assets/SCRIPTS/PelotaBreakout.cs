using UnityEngine;

public class PelotaBreakout : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 direccion; 

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
            
           
            BloqueJuego bloque = collision.GetComponent<BloqueJuego>();
            if (bloque != null)
            {
                int puntos = bloque.ObtenerPuntos();
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.SumarPuntos(puntos);
                }
                
                
                bloque.IntentarSpawnearPowerup();
            }
            
            
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
        // dirección del rebote basada en el impacto
        Vector3 puntoImpacto = transform.position - collision.transform.position;
        puntoImpacto.Normalize();
        
        
        direccion = puntoImpacto;
        
       
        if (collision.CompareTag("Pala") && direccion.y < 0)
        {
            direccion.y = -direccion.y;
        }
    }

    // se usa para inicializar dirección aleatoria
    void InicializarDireccion()
    {
        direccion = new Vector3(
            Random.Range(-1.0f, 1.0f),
            Random.Range(0.5f, 1.0f),
            0
        ).normalized;
    }
}
