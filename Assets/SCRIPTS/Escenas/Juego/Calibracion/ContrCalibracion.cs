using UnityEngine;

public class ContrCalibracion : MonoBehaviour
{
    public Player Pj;
    
    public float TiempEspCalib = 3;
    float Tempo2 = 0;

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

    //----------------------------------------------------//

    private void Awake()
    {
        pRenderer = P.GetComponent<Renderer>();
        
        LlegadaRenderer = Llegada.GetComponent<Renderer>();
        LlegadaCollider = Llegada.GetComponent<Collider>();

        partidaRenderer = Partida.GetComponent<Renderer>();
        partidaCollider = Partida.GetComponent<Collider>();
    }
    
    public void Start()
    {
        palletsMover.enabled = false;
        Pj.ContrCalib = this;

        Partida.Recibir(P);

        SetActivComp(false);
    }
    
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