using UnityEngine;

// Script adaptado para limitar el movimiento de la pala en Breakout
// Solo necesitamos límites horizontales (izquierda y derecha)
public class LimitesMovimientoPala : MonoBehaviour
{
    public float limiteXpos = 8.5f;  // Límite derecho
    public float limiteXneg = -8.5f; // Límite izquierdo

    void Update()
    {
        // Limitar movimiento horizontal
        if (transform.position.x > limiteXpos)
        {
            transform.position = new Vector3(limiteXpos, transform.position.y, 0);
        }
        if (transform.position.x < limiteXneg)
        {
            transform.position = new Vector3(limiteXneg, transform.position.y, 0);
        }
    }
}
