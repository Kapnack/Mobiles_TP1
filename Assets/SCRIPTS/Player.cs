using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [HideInInspector] public int Dinero = 0;
    public int IdPlayer = 0;

    [HideInInspector] public Bolsa[] Bolasas;
    [HideInInspector] public int CantBolsAct = 0;

    [HideInInspector] public ControladorDeDescarga ContrDesc;
    [HideInInspector] public ContrCalibracion ContrCalib;
    [HideInInspector] public ControlDireccion ControlDire;
    Visualizacion MiVisualizacion;

    [SerializeField] private GameObject CanvasUnJugador;
    [SerializeField] private GameObject CanvasDosJugadores;
    [HideInInspector] public GameObject CanvasDescarga;

    private Frenado frenado;
    private Respawn respawn;

    private Rigidbody rb;

    private Collider col;

    //------------------------------------------------------------------//

    private void Awake()
    {
        MiVisualizacion = GetComponent<Visualizacion>();

        frenado = GetComponent<Frenado>();
        respawn = GetComponent<Respawn>();

        rb = GetComponent<Rigidbody>();

        col = GetComponent<Collider>();

        ControlDire = GetComponent<ControlDireccion>();

        rb.useGravity = false;

        CanvasDescarga = GameplaySettingsManager.Instance.IsMultiplayer ? CanvasDosJugadores : CanvasUnJugador;

        CanvasUnJugador.gameObject.SetActive(false);
        CanvasDosJugadores.gameObject.SetActive(false);
    }

    private void Start()
    {
        for (int i = 0; i < Bolasas.Length; i++)
            Bolasas[i] = null;
    }

    //------------------------------------------------------------------//

    public bool AgregarBolsa(Bolsa b)
    {
        if (CantBolsAct + 1 <= Bolasas.Length)
        {
            Bolasas[CantBolsAct] = b;
            CantBolsAct++;
            Dinero += (int)b.Monto;
            b.Desaparecer();
            return true;
        }

        return false;
    }

    public void VaciarInv()
    {
        for (int i = 0; i < Bolasas.Length; i++)
            Bolasas[i] = null;

        CantBolsAct = 0;
    }

    public bool ConBolasas()
    {
        for (int i = 0; i < Bolasas.Length; i++)
        {
            if (Bolasas[i] != null)
            {
                return true;
            }
        }

        return false;
    }

    public void SetContrDesc(ControladorDeDescarga contr)
    {
        ContrDesc = contr;
    }

    public void CambiarACalibracion()
    {
        MiVisualizacion.CambiarACalibracion();
    }

    public void CambiarAConduccion()
    {
        CanvasDescarga?.SetActive(false);
        rb.useGravity = true;
        transform.forward = Vector3.forward;
        frenado.Frenar();
        MiVisualizacion.CambiarAConduccion();
        frenado.RestaurarVel();
        ControlDire.Habilitado = false;
        transform.forward = Vector3.forward;
    }

    public void CambiarADescarga()
    {
        CanvasDescarga?.SetActive(true);
        MiVisualizacion.CambiarADescarga();
    }

    public void SacarBolasa()
    {
        for (int i = 0; i < Bolasas.Length; i++)
        {
            if (Bolasas[i])
            {
                Bolasas[i] = null;
                return;
            }
        }
    }

    public void SalirDelDeposito(Vector3 posicion, Vector3 direccion)
    {
        VaciarInv();
        frenado.RestaurarVel();
        respawn.Respawnear(posicion, direccion);
        ControlDire.Habilitado = true;
        col.enabled = true;

        rb.useGravity = true;
    }

    public void EntrarAlDeposito(Vector3 posicion, Vector3 direccion)
    {
        col.enabled = false;

        rb.useGravity = false;

        transform.position = posicion;
        transform.forward = direccion;
    }

    public void EmpezarCarrera()
    {
        frenado.RestaurarVel();
        ControlDire.Habilitado = true;
    }
}