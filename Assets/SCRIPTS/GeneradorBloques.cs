using UnityEngine;


public class GeneradorBloques : MonoBehaviour
{
    public GameObject prefabBloque; 
    
    public int filas = 6;
    public int columnas = 10;
    
    public float espaciadoX = 1.5f; 
    public float espaciadoY = 0.8f;
    
    public Vector3 posicionInicial = new Vector3(-7f, 4f, 0f); 
    
    
    private Color[] coloresPorFila = new Color[]
    {
        Color.red,      // Fila 1 - 6 
        new Color(1f, 0.5f, 0f), // Naranja - Fila 2 -  5 
        Color.yellow,   // Fila 3 -  4 
        Color.green,    // Fila 4 -  3 
        Color.cyan,     // Fila 5 -  2 
        Color.magenta   // Fila 6 -  1 
    };

    void Start()
    {
        GenerarBloques();
    }

    void GenerarBloques()
    {
        for (int fila = 0; fila < filas; fila++)
        {
            for (int columna = 0; columna < columnas; columna++)
            {
                
                Vector3 posicion = new Vector3(
                    posicionInicial.x + (columna * espaciadoX),
                    posicionInicial.y - (fila * espaciadoY),
                    0
                );

                
                GameObject nuevoBloque = Instantiate(prefabBloque, posicion, Quaternion.identity);
                nuevoBloque.transform.parent = transform; 

               
                BloqueJuego componenteBloque = nuevoBloque.GetComponent<BloqueJuego>();
                if (componenteBloque != null)
                {
                    
                    componenteBloque.valorPuntos = filas - fila;
                    componenteBloque.colorBloque = coloresPorFila[fila];
                }
            }
        }
    }
}
