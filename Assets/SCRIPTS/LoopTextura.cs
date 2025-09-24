using UnityEngine;

public class LoopTextura : MonoBehaviour
{
    public float Intervalo = 1;
    float Tempo = 0;

    public Texture2D[] Imagenes;
    int Contador = 0;

    private Renderer renderer;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        if (Imagenes.Length > 0)
            renderer.material.mainTexture = Imagenes[0];
    }

    // Update is called once per frame
    private void Update()
    {
        Tempo += Time.deltaTime;

        if (Tempo >= Intervalo)
        {
            Tempo = 0;
            Contador++;
            if (Contador >= Imagenes.Length)
            {
                Contador = 0;
            }

            renderer.material.mainTexture = Imagenes[Contador];
        }
    }
}