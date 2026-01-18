using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoPalaBreakout : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        Vector3 direccion = Vector3.zero;

        
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            direccion.x = -1;
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            direccion.x = 1;
        }

        
        if (Keyboard.current.aKey.isPressed)
        {
            direccion.x = -1;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            direccion.x = 1;
        }

        
        transform.position = transform.position + direccion * speed * Time.deltaTime;
    }
}
