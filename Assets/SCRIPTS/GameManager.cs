using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using entityStates;
using GameManagerStates;
using Systems;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instancia;

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

    public enum EstadoJuego
    {
        Calibrando,
        Jugando,
        Finalizado
    }

    public EstadoJuego EstAct = EstadoJuego.Calibrando;

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

    //escena de tutorial
    public GameObject[] ObjsTuto1;

    public GameObject[] ObjsTuto2;

    //--------------------------------------------------------//

    private void Awake()
    {
        Instancia = this;
        CanvasJuegoGO?.SetActive(false);
    }

    private void Start()
    {
        state = stateCalibracion;
        state.Cambiar(this);
        //IniciarCalibracion();
    }

    private void Update()
    {
        //REINICIAR
        if (Input.GetKey(KeyCode.Mouse1) &&
            Input.GetKey(KeyCode.Keypad0))
        {
            SceneOrganizer.Instance.LoadGameplayScene();
        }

        //CIERRA LA APLICACION
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        state.Update();

/*
        switch (EstAct)
        {
            case EstadoJuego.Calibrando:

                //SKIP EL TUTORIAL
                if (Input.GetKey(KeyCode.Mouse0) &&
                    Input.GetKey(KeyCode.Keypad0))
                {
                    if (PlayerInfo1 != null && PlayerInfo2 != null)
                    {
                        FinCalibracion(0);
                        FinCalibracion(1);

                        FinTutorial(0);
                        FinTutorial(1);
                    }
                }

                if (!PlayerInfo1.PJ && Input.GetKeyDown(KeyCode.W))
                {
                    PlayerInfo1 = new PlayerInfo(0, Player1);
                    PlayerInfo1.LadoAct = Visualizacion.Lado.Izq;
                    SetPosicion(PlayerInfo1);
                }

                if (!PlayerInfo2.PJ && Input.GetKeyDown(KeyCode.UpArrow))
                {
                    PlayerInfo2 = new PlayerInfo(1, Player2);
                    PlayerInfo2.LadoAct = Visualizacion.Lado.Der;
                    SetPosicion(PlayerInfo2);
                }

                if (PlayerInfo1.PJ && PlayerInfo2.PJ)
                {
                    if (PlayerInfo1.FinTuto2 && PlayerInfo2.FinTuto2)
                    {
                        EmpezarCarrera();
                    }
                }

                break;


            case EstadoJuego.Jugando:

                //SKIP LA CARRERA
                if (Input.GetKey(KeyCode.Mouse1) &&
                    Input.GetKey(KeyCode.Keypad0))
                {
                    TiempoDeJuego = 0;
                }

                if (TiempoDeJuego <= 0)
                {
                    FinalizarCarrera();
                }

                if (ConteoRedresivo)
                {
                    ConteoParaInicion -= Time.deltaTime;
                    if (ConteoParaInicion < 0)
                    {
                        EmpezarCarrera();
                        ConteoRedresivo = false;
                    }
                }
                else
                    TiempoDeJuego -= Time.deltaTime;

                break;


            case EstadoJuego.Finalizado:

                TiempEspMuestraPts -= Time.deltaTime;
                if (TiempEspMuestraPts <= 0)
                {
                    Addressables.ReleaseInstance(ObstaculosGO);
                    Addressables.ReleaseInstance(PistaGO);
                    SceneOrganizer.Instance.LoadEndGameScene();
                }

                break;
        }
        */
    }

    //----------------------------------------------------------//

    public void IniciarCalibracion()
    {
        for (int i = 0; i < ObjsCalibracion1.Length; i++)
        {
            ObjsCalibracion1[i].SetActiveRecursively(true);
            ObjsCalibracion2[i].SetActiveRecursively(true);
        }

        for (int i = 0; i < ObjsTuto2.Length; i++)
        {
            ObjsTuto2[i].SetActiveRecursively(false);
            ObjsTuto1[i].SetActiveRecursively(false);
        }


        Player1.CambiarACalibracion();
        Player2.CambiarACalibracion();
    }

    public void EmpezarCarrera()
    {
        Player1.GetComponent<Frenado>().RestaurarVel();
        Player1.GetComponent<ControlDireccion>().Habilitado = true;

        Player2.GetComponent<Frenado>().RestaurarVel();
        Player2.GetComponent<ControlDireccion>().Habilitado = true;
    }

    public void FinalizarCarrera()
    {
        EstAct = EstadoJuego.Finalizado;

        TiempoDeJuego = 0;

        if (Player1.Dinero > Player2.Dinero)
        {
            //lado que gano
            if (PlayerInfo1.LadoAct == Visualizacion.Lado.Der)
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Der;
            else
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Izq;

            //puntajes
            DatosPartida.PtsGanador = Player1.Dinero;
            DatosPartida.PtsPerdedor = Player2.Dinero;
        }
        else
        {
            //lado que gano
            if (PlayerInfo2.LadoAct == Visualizacion.Lado.Der)
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Der;
            else
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Izq;

            //puntajes
            DatosPartida.PtsGanador = Player2.Dinero;
            DatosPartida.PtsPerdedor = Player1.Dinero;
        }

        Player1.GetComponent<Frenado>().Frenar();
        Player2.GetComponent<Frenado>().Frenar();

        Player1.ContrDesc.FinDelJuego();
        Player2.ContrDesc.FinDelJuego();
    }


    public void SetPosicion(PlayerInfo pjInf)
    {
        pjInf.PJ.GetComponent<Visualizacion>().SetLado(pjInf.LadoAct);
        //en este momento, solo la primera vez, deberia setear la otra camara asi no se superponen
        pjInf.PJ.ContrCalib.IniciarTesteo();
        PosSeteada = true;


        if (pjInf.PJ == Player1)
        {
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

    [Obsolete("Obsolete")]
    private async void CambiarACarrera()
    {
        var handle = Addressables.InstantiateAsync(Pista);

        PistaGO = await handle.Task;
        PistaGO.transform.position = new Vector3(-17.88721f, -30.0f, 5202.328f);

        handle = Addressables.InstantiateAsync(Obstaculos);

        ObstaculosGO = await handle.Task;
        ObstaculosGO.transform.position = new Vector3(-19.49809f, 5.903175f, 5392.36f);

        SceneManager.MoveGameObjectToScene(ObstaculosGO, gameObject.scene);

        SceneManager.MoveGameObjectToScene(PistaGO, gameObject.scene);

        for (int i = 0; i < ObjsTuto1.Length; i++)
        {
            ObjsTuto1[i].SetActiveRecursively(true);
        }

        for (int i = 0; i < ObjsCalibracion1.Length; i++)
        {
            ObjsCalibracion1[i].SetActiveRecursively(false);
        }

        for (int i = 0; i < ObjsCalibracion2.Length; i++)
        {
            ObjsCalibracion2[i].SetActiveRecursively(false);
        }

        for (int i = 0; i < ObjsTuto2.Length; i++)
        {
            ObjsTuto2[i].SetActiveRecursively(true);
        }


        //posiciona los camiones dependiendo de que lado de la pantalla esten
        if (PlayerInfo1.LadoAct == Visualizacion.Lado.Izq)
        {
            Player1.gameObject.transform.position = PosCamionesCarrera[0];
            Player2.gameObject.transform.position = PosCamionesCarrera[1];
        }
        else
        {
            Player1.gameObject.transform.position = PosCamionesCarrera[1];
            Player2.gameObject.transform.position = PosCamionesCarrera[0];
        }

        Player1.CambiarAConduccion();

        Player2.CambiarAConduccion();

        EstAct = EstadoJuego.Jugando;
    }

    public void FinTutorial(int playerID)
    {
        if (playerID == 0)
        {
            PlayerInfo1.FinTuto2 = true;
        }
        else if (playerID == 1)
        {
            PlayerInfo2.FinTuto2 = true;
        }
        
    }

    public void FinCalibracion(int playerID)
    {
        if (playerID == 0)
        {
            PlayerInfo1.FinTuto1 = true;
        }
        else if (playerID == 1)
        {
            PlayerInfo2.FinTuto1 = true;
        }
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