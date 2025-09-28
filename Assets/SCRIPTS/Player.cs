using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public int Dinero = 0;
    public int IdPlayer = 0;

    public Bolsa[] Bolasas;
    public int CantBolsAct = 0;

    public enum Estados
    {
        EnDescarga,
        EnConduccion,
        EnCalibracion,
        EnTutorial
    }

    public Estados EstAct = Estados.EnConduccion;

    public ControladorDeDescarga ContrDesc;
    public ContrCalibracion ContrCalib;
    public ContrTutorial ContrTuto;
    public ControlDireccion ControlDire;
    Visualizacion MiVisualizacion;

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
        EstAct = Player.Estados.EnCalibracion;
    }

    public void CambiarATutorial()
    {
        MiVisualizacion.CambiarATutorial();
        EstAct = Player.Estados.EnTutorial;
        ContrTuto.Iniciar();
    }

    public void CambiarAConduccion()
    {
        rb.useGravity = true;
        transform.forward = Vector3.forward;
        frenado.Frenar();
        MiVisualizacion.CambiarAConduccion();
        EstAct = Estados.EnConduccion;
        frenado.RestaurarVel();
        ControlDire.Habilitado = false;
        transform.forward = Vector3.forward;
    }

    public void CambiarADescarga()
    {
        MiVisualizacion.CambiarADescarga();
        EstAct = Estados.EnDescarga;
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