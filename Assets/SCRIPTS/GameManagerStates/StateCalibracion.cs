using UnityEngine;
using UnityEngine.InputSystem;

namespace GameManagerStates
{
    public class StateCalibracion : AbstractState<GameManager>
    {
        public InputActionAsset _inputSystem;

        private InputAction _confirmar1;
        private InputAction _confirmar2;

        public override void Cambiar(GameManager entity)
        {
            this.entity = entity;
            Empezar();
        }

        public override void Empezar()
        {
            // Mostrar objetos de calibración
            for (int i = 0; i < entity.ObjsCalibracion1.Length; i++)
            {
                entity.ObjsCalibracion1[i].SetActiveRecursively(true);

                if (GameplaySettingsManager.Instance.IsMultiplayer)
                    entity.ObjsCalibracion2[i].SetActiveRecursively(true);
            }

            // Cambiar jugadores a modo calibración
            entity.Player1.CambiarACalibracion();
            if (GameplaySettingsManager.Instance.IsMultiplayer)
                entity.Player2.CambiarACalibracion();

            // Buscar acciones en el InputActionAsset
            var map1 = _inputSystem.FindActionMap("Jugador1", true);
            _confirmar1 = map1.FindAction("Confirmar");
            map1.Enable();

            if (GameplaySettingsManager.Instance.IsMultiplayer)
            {
                var map2 = _inputSystem.FindActionMap("Jugador2", true);
                _confirmar2 = map2.FindAction("Confirmar");
                map2.Enable();
            }
            
            if (!entity.PlayerInfo1.PJ)
            {
                entity.PlayerInfo1 = new GameManager.PlayerInfo(0, entity.Player1);
                entity.PlayerInfo1.LadoAct = Visualizacion.Lado.Izq;
                entity.SetPosicion(entity.PlayerInfo1);
            }

            // Jugador 2 confirma
            if (GameplaySettingsManager.Instance.IsMultiplayer)
            {
                if (!entity.PlayerInfo2.PJ)
                {
                    entity.PlayerInfo2 = new GameManager.PlayerInfo(1, entity.Player2);
                    entity.PlayerInfo2.LadoAct = Visualizacion.Lado.Der;
                    entity.SetPosicion(entity.PlayerInfo2);
                }
            }
        }

        public override void Update()
        {
            // Chequear finalización
            if (GameplaySettingsManager.Instance.IsMultiplayer)
            {
                if (entity.PlayerInfo1.PJ && entity.PlayerInfo2.PJ)
                    if (entity.PlayerInfo1.FinTuto1 && entity.PlayerInfo2.FinTuto2)
                        Finalizar();
            }
            else
            {
                if (entity.PlayerInfo1.PJ)
                    if (entity.PlayerInfo1.FinTuto1)
                        Finalizar();
            }
        }

        public override void Finalizar()
        {
            entity.state = entity.stateCarrera;
            entity.state.Cambiar(entity);

            // Desactivar inputs
            _confirmar1?.Disable();
            _confirmar2?.Disable();
        }
    }
}