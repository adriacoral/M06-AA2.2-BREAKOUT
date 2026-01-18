using UnityEngine;


public class LimitesMovimientoPala : MonoBehaviour
{
    public float limiteXpos = 8.5f;  
    public float limiteXneg = -8.5f; 

    void Update()
    {
        
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
