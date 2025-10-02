using GameManagerStates;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace entityStates
{
    public class StateCarrera : AbstractState<GameManager>
    {
        public override void Update()
        {
            if (Input.GetKey(KeyCode.Mouse1) &&
                Input.GetKey(KeyCode.Keypad0))
            {
                entity.TiempoDeJuego = 0;
            }

            if (entity.TiempoDeJuego <= 0)
            {
                Finalizar();
            }

            if (entity.ConteoRedresivo)
            {
                entity.ConteoParaInicion -= Time.deltaTime;
                if (entity.ConteoParaInicion < 0)
                {
                    Empezar();
                    entity.ConteoRedresivo = false;
                }
            }
            else
                entity.TiempoDeJuego -= Time.deltaTime;
        }

        public override async void Cambiar(GameManager entity)
        {
            this.entity = entity;

            entity.calibracionHUDs.gameObject.SetActive(false);
            
            var handle = Addressables.InstantiateAsync(entity.Pista);

            entity.PistaGO = await handle.Task;
            entity.PistaGO.transform.position = new Vector3(-17.88721f, -30.0f, 5202.328f);

            handle = Addressables.InstantiateAsync(entity.Obstaculos);

            entity.ObstaculosGO = await handle.Task;
            entity.ObstaculosGO.transform.position = new Vector3(-19.49809f, 5.903175f, 5392.36f);

            SceneManager.MoveGameObjectToScene(entity.ObstaculosGO, entity.gameObject.scene);

            SceneManager.MoveGameObjectToScene(entity.PistaGO, entity.gameObject.scene);

            entity.CanvasJuegoGO?.SetActive(true);

            for (int i = 0; i < entity.ObjsCalibracion1.Length; i++)
            {
                entity.ObjsCalibracion1[i].SetActiveRecursively(false);
            }

            if (GameplaySettingsManager.Instance.IsMultiplayer)
                for (int i = 0; i < entity.ObjsCalibracion2.Length; i++)
                {
                    entity.ObjsCalibracion2[i].SetActiveRecursively(false);
                }


            //posiciona los camiones dependiendo de que lado de la pantalla esten
            if (entity.PlayerInfo1.LadoAct == Visualizacion.Lado.Izq)
            {
                entity.Player1.gameObject.transform.position = entity.PosCamionesCarrera[0];

                if (GameplaySettingsManager.Instance.IsMultiplayer)
                    entity.Player2.gameObject.transform.position = entity.PosCamionesCarrera[1];
            }
            else
            {
                entity.Player1.gameObject.transform.position = entity.PosCamionesCarrera[1];

                if (GameplaySettingsManager.Instance.IsMultiplayer)
                    entity.Player2.gameObject.transform.position = entity.PosCamionesCarrera[0];
            }

            entity.Player1.CambiarAConduccion();

            if (GameplaySettingsManager.Instance.IsMultiplayer)
                entity.Player2.CambiarAConduccion();

            Empezar();
        }

        public override void Empezar()
        {
            entity.Player1.EmpezarCarrera();

            if (GameplaySettingsManager.Instance.IsMultiplayer)
                entity.Player2.EmpezarCarrera();
        }

        public override void Finalizar()
        {
            entity.TiempoDeJuego = 0;

            if (GameplaySettingsManager.Instance.IsMultiplayer)
            {
                if (entity.Player1.Dinero > entity.Player2.Dinero)
                {
                    //lado que gano
                    if (entity.PlayerInfo1.LadoAct == Visualizacion.Lado.Der)
                        DatosPartida.LadoGanadaor = DatosPartida.Lados.Der;
                    else
                        DatosPartida.LadoGanadaor = DatosPartida.Lados.Izq;

                    //puntajes
                    DatosPartida.PtsGanador = entity.Player1.Dinero;
                    DatosPartida.PtsPerdedor = entity.Player2.Dinero;
                }
                else
                {
                    //lado que gano
                    if (entity.PlayerInfo2.LadoAct == Visualizacion.Lado.Der)
                        DatosPartida.LadoGanadaor = DatosPartida.Lados.Der;
                    else
                        DatosPartida.LadoGanadaor = DatosPartida.Lados.Izq;

                    //puntajes
                    DatosPartida.PtsGanador = entity.Player2.Dinero;
                    DatosPartida.PtsPerdedor = entity.Player1.Dinero;
                }

                entity.Player1.GetComponent<Frenado>().Frenar();
                entity.Player2.GetComponent<Frenado>().Frenar();

                entity.Player1.ContrDesc.FinDelJuego();
                entity.Player2.ContrDesc.FinDelJuego();
            }
            else
            {
                    //lado que gano
                    if (entity.PlayerInfo1.LadoAct == Visualizacion.Lado.Der)
                        DatosPartida.LadoGanadaor = DatosPartida.Lados.Der;
                    else
                        DatosPartida.LadoGanadaor = DatosPartida.Lados.Izq;
                    
                    DatosPartida.PtsGanador = entity.Player1.Dinero;
            }

            entity.state = entity.StateFinDelJuego;
            entity.state.Cambiar(entity);
        }
    }
}