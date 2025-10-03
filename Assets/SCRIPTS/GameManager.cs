using System;
using UnityEngine;
using entityStates;
using GameManagerStates;
using Systems;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    public GameObject calibracionHUDs;
    public AbstractState<GameManager> state;

    private StateCalibracion stateCalibracion = new();
    public StateCarrera stateCarrera = new();
    public StateFinDelJuego StateFinDelJuego = new();

    public float TiempoDeJuego = 60;

    public AssetReference Obstaculos;
    public AssetReference Pista;
    public GameObject CanvasJuegoGO;
    [HideInInspector] public GameObject ObstaculosGO;
    [HideInInspector] public GameObject PistaGO;

    public PlayerInfo PlayerInfo1 = null;
    public PlayerInfo PlayerInfo2 = null;

    public Player Player1;
    public Player Player2;

    bool PosSeteada = false;

    public bool ConteoRedresivo = true;
    public float ConteoParaInicion = 3;

    public float TiempEspMuestraPts = 3;

    //posiciones de los camiones dependientes del lado que les toco en la pantalla
    //la pos 0 es para la izquierda y la 1 para la derecha
    public Vector3[] PosCamionesCarrera = new Vector3[2];

    //listas de GO que activa y desactiva por sub-escena
    //escena de calibracion
    public GameObject[] ObjsCalibracion1;

    public GameObject[] ObjsCalibracion2;

    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private InputActionReference cerrarJuego;

    //--------------------------------------------------------//

    protected override void Awake()
    {
        base.Awake();

        if (actionAsset != null)
            stateCalibracion._inputSystem = actionAsset;

        if (cerrarJuego != null)
            cerrarJuego.action.started += CerrarJuego;

        CanvasJuegoGO?.SetActive(false);

        if (!GameplaySettingsManager.Instance.IsMultiplayer)
            Player2.gameObject.SetActive(false);
        else
            Player2.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        if (cerrarJuego != null)
            cerrarJuego.action.started -= CerrarJuego;
    }

    private void Start()
    {
        state = stateCalibracion;
        state.Cambiar(this);
    }

    private void Update() => state.Update();

    private void CerrarJuego(InputAction.CallbackContext _)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void SetPosicion(PlayerInfo pjInf)
    {
        pjInf.PJ.GetComponent<Visualizacion>().SetLado(pjInf.LadoAct);
        //en este momento, solo la primera vez, deberia setear la otra camara asi no se superponen
        pjInf.PJ.ContrCalib.IniciarTesteo();
        PosSeteada = true;


        if (pjInf.PJ == Player1)
        {
            if (!GameplaySettingsManager.Instance.IsMultiplayer)
                return;

            if (pjInf.LadoAct == Visualizacion.Lado.Izq)
                Player2.GetComponent<Visualizacion>().SetLado(Visualizacion.Lado.Der);
            else
                Player2.GetComponent<Visualizacion>().SetLado(Visualizacion.Lado.Izq);
        }
        else
        {
            if (pjInf.LadoAct == Visualizacion.Lado.Izq)
                Player1.GetComponent<Visualizacion>().SetLado(Visualizacion.Lado.Der);
            else
                Player1.GetComponent<Visualizacion>().SetLado(Visualizacion.Lado.Izq);
        }
    }

    public void FinCalibracion(int playerID)
    {
        if (GameplaySettingsManager.Instance.IsMultiplayer)
        {
            if (playerID == 0)
            {
                PlayerInfo1.FinTuto1 = true;
            }
            else if (playerID == 1)
            {
                PlayerInfo2.FinTuto1 = true;
            }

            if (PlayerInfo1.PJ != null && PlayerInfo2.PJ != null)
                if (PlayerInfo1.FinTuto1 && PlayerInfo2.FinTuto1)
                {
                    state.Finalizar();
                }
        }
        else if (PlayerInfo1.PJ != null)
            state.Finalizar();
    }

    [Serializable]
    public class PlayerInfo
    {
        public PlayerInfo(int tipoDeInput, Player pj)
        {
            PJ = pj;
        }

        public bool FinTuto1 = false;
        public bool FinTuto2 = false;

        public Visualizacion.Lado LadoAct;

        public Player PJ;
    }
}