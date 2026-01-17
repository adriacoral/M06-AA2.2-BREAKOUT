using UnityEngine;

// Script para generar automáticamente la cuadrícula de bloques
// Crea 6 filas x 10 columnas = 60 bloques
public class GeneradorBloques : MonoBehaviour
{
    public GameObject prefabBloque; // Prefab del bloque (debe tener el componente Bloque)
    
    public int filas = 6;
    public int columnas = 10;
    
    public float espaciadoX = 1.5f; // Espacio entre bloques horizontalmente
    public float espaciadoY = 0.8f; // Espacio entre bloques verticalmente
    
    public Vector3 posicionInicial = new Vector3(-7f, 4f, 0f); // Esquina superior izquierda
    
    // Colores para cada fila (de arriba a abajo)
    private Color[] coloresPorFila = new Color[]
    {
        Color.red,      // Fila 1 - Valor 6 puntos
        new Color(1f, 0.5f, 0f), // Naranja - Fila 2 - Valor 5 puntos
        Color.yellow,   // Fila 3 - Valor 4 puntos
        Color.green,    // Fila 4 - Valor 3 puntos
        Color.cyan,     // Fila 5 - Valor 2 puntos
        Color.magenta   // Fila 6 - Valor 1 punto
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
                // Calcular posición del bloque
                Vector3 posicion = new Vector3(
                    posicionInicial.x + (columna * espaciadoX),
                    posicionInicial.y - (fila * espaciadoY),
                    0
                );

                // Crear el bloque
                GameObject nuevoBloque = Instantiate(prefabBloque, posicion, Quaternion.identity);
                nuevoBloque.transform.parent = transform; // Organizarlo como hijo del generador

                // Configurar el componente BloqueJuego
                BloqueJuego componenteBloque = nuevoBloque.GetComponent<BloqueJuego>();
                if (componenteBloque != null)
                {
                    // La primera fila vale 6 puntos, la última vale 1
                    componenteBloque.valorPuntos = filas - fila;
                    componenteBloque.colorBloque = coloresPorFila[fila];
                }
            }
        }
    }
}
