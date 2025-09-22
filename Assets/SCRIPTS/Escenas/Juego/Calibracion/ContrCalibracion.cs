using UnityEngine;
using System.Collections;

public class ContrCalibracion : MonoBehaviour
{
    public Player Pj;

    /*
    public string ManoIzqName = "Left Hand";
    public string ManoDerName = "Right Hand";

    bool StayIzq = false;
    bool StayDer = false;
    */
    /*
    public float TiempCalib = 3;
    float Tempo = 0;
    */
    public float TiempEspCalib = 3;
    float Tempo2 = 0;

    //bool EnTutorial = false;

    public enum Estados
    {
        Calibrando,
        Tutorial,
        Finalizado
    }

    public Estados EstAct = Estados.Calibrando;

    public ManejoPallets Partida;

    private Renderer partidaRenderer;
    private Collider partidaCollider;

    public ManejoPallets Llegada;

    private Renderer LlegadaRenderer;
    private Collider LlegadaCollider;


    public Pallet P;

    private Renderer pRenderer;
    
    public ManejoPallets palletsMover;

    GameManager GM;

    //----------------------------------------------------//

    private void Awake()
    {
        pRenderer = P.GetComponent<Renderer>();
        
        LlegadaRenderer = Llegada.GetComponent<Renderer>();
        LlegadaCollider = Llegada.GetComponent<Collider>();

        partidaRenderer = Partida.GetComponent<Renderer>();
        partidaCollider = Partida.GetComponent<Collider>();
    }

    // Use this for initialization
    void Start()
    {
        /*
        renderer.enabled = false;
        collider.enabled = false;
        */
        palletsMover.enabled = false;
        Pj.ContrCalib = this;

        GM = GameObject.Find("GameMgr").GetComponent<GameManager>();

        Partida.Recibir(P);

        SetActivComp(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (EstAct == Estados.Tutorial)
        {
            if (Tempo2 < TiempEspCalib)
            {
                Tempo2 += Time.deltaTime;
                if (Tempo2 > TiempEspCalib)
                {
                    SetActivComp(true);
                }
            }
        }
    }

    public void IniciarTesteo()
    {
        EstAct = Estados.Tutorial;
        palletsMover.enabled = true;
    }

    public void FinTutorial()
    {
        EstAct = ContrCalibracion.Estados.Finalizado;
        palletsMover.enabled = false;
        GM.FinCalibracion(Pj.IdPlayer);
    }

    private void SetActivComp(bool estado)
    {
        if (partidaRenderer)
            partidaRenderer.enabled = estado;
        partidaCollider.enabled = estado;
        if (LlegadaRenderer)
            LlegadaRenderer.enabled = estado;
        LlegadaCollider.enabled = estado;
        
        pRenderer.enabled = estado;
    }
}