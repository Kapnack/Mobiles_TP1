using UnityEngine;
using System.Collections;

public class Frenado : MonoBehaviour
{
    public float VelEntrada = 0;
    public string TagDeposito = "Deposito";

    ControlDireccion KInput;


    float DagMax = 15f;
    float DagIni = 1f;
    int Contador = 0;
    int CantMensajes = 10;
    float TiempFrenado = 0.5f;
    float Tempo = 0f;

    Vector3 Destino;

    public bool Frenando = false;
    bool ReduciendoVel = false;

    private ControlDireccion controlDireccion;
    private CarController carController;
    private Rigidbody rb;

    //-----------------------------------------------------//

    private void Awake()
    {
        controlDireccion = GetComponent<ControlDireccion>();
        carController = GetComponent<CarController>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Frenar();
    }

    void FixedUpdate()
    {
        if (Frenando)
        {
            Tempo += T.GetFDT();
            
            if (Tempo >= (TiempFrenado / CantMensajes) * Contador)
            {
                Contador++;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagDeposito))
        {
            Deposito2 dep = other.GetComponent<Deposito2>();
            if (dep.Vacio)
            {
                if (this.GetComponent<Player>().ConBolasas())
                {
                    dep.Entrar(this.GetComponent<Player>());
                    Destino = other.transform.position;
                    transform.forward = Destino - transform.position;
                    Frenar();
                }
            }
        }
    }

    //-----------------------------------------------------------//

    public void Frenar()
    {
        controlDireccion.enabled = false;
        carController.SetAcel(0);

        rb.linearVelocity = Vector3.zero;

        Frenando = true;
        
        Tempo = 0;
        Contador = 0;
    }

    public void RestaurarVel()
    {
        controlDireccion.enabled = true;
        carController.SetAcel(1);
        Frenando = false;
        Tempo = 0;
        Contador = 0;
    }
}