using Systems;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameManagerStates
{
    public class StateFinDelJuego : AbstractState<GameManager>
    {
        public override void Cambiar(GameManager gameManager)
        {
            entity = gameManager;
        }

        public override void Update()
        {
            entity.TiempEspMuestraPts -= Time.deltaTime;
            if (entity.TiempEspMuestraPts <= 0)
            {
                Addressables.ReleaseInstance(entity.ObstaculosGO);
                Addressables.ReleaseInstance(entity.PistaGO);
                SceneOrganizer.Instance.LoadEndGameScene();
            }
        }

        public override void Empezar()
        {
            throw new System.NotImplementedException();
        }

        public override void Finalizar()
        {
            throw new System.NotImplementedException();
        }
    }
}