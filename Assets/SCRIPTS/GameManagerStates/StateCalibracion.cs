using UnityEngine;

namespace GameManagerStates
{
    public class StateCalibracion : AbstractState<GameManager>
    {
        public override void Update()
        {
            if (entity.PlayerInfo1 != null && !entity.PlayerInfo1.PJ && Input.GetKeyDown(KeyCode.W))
            {
                entity.PlayerInfo1 = new GameManager.PlayerInfo(0, entity.Player1);
                entity.PlayerInfo1.LadoAct = Visualizacion.Lado.Izq;
                entity.SetPosicion(entity.PlayerInfo1);
            }

            if (entity.PlayerInfo2 != null && !entity.PlayerInfo2.PJ && Input.GetKeyDown(KeyCode.UpArrow))
            {
                entity.PlayerInfo2 = new GameManager.PlayerInfo(1, entity.Player2);
                entity.PlayerInfo2.LadoAct = Visualizacion.Lado.Der;
                entity.SetPosicion(entity.PlayerInfo2);
            }

            if (entity.PlayerInfo1.PJ && entity.PlayerInfo2.PJ)
                Finalizar();
        }

        public override void Cambiar(GameManager entity)
        {
            this.entity = entity;
            Empezar();
        }

        public override void Empezar()
        {
            for (int i = 0; i < entity.ObjsCalibracion1.Length; i++)
            {
                entity.ObjsCalibracion1[i].SetActiveRecursively(true);
                entity.ObjsCalibracion2[i].SetActiveRecursively(true);
            }

            for (int i = 0; i < entity.ObjsTuto2.Length; i++)
            {
                entity.ObjsTuto2[i].SetActiveRecursively(false);
                entity.ObjsTuto1[i].SetActiveRecursively(false);
            }


            entity.Player1.CambiarACalibracion();
            entity.Player2.CambiarACalibracion();
        }

        public override void Finalizar()
        {
            entity.state = entity.stateCarrera;
            entity.state.Cambiar(entity);
        }
    }
}