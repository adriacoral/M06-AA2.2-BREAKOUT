using UnityEngine;
using UnityEngine.InputSystem;

// Script adaptado del Pong para controlar la pala en Breakout
// Solo necesitamos un jugador que se mueve horizontalmente en la parte inferior
public class MovimientoPalaBreakout : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        Vector3 direccion = Vector3.zero;

        // Control con flechas izquierda/derecha
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            direccion.x = -1;
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            direccion.x = 1;
        }

        // También permitimos control con A/D
        if (Keyboard.current.aKey.isPressed)
        {
            direccion.x = -1;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            direccion.x = 1;
        }

        // Aplicar movimiento
        transform.position = transform.position + direccion * speed * Time.deltaTime;
    }
}
