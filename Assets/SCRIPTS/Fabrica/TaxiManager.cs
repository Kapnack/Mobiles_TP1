using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Fabrica
{
    public class TaxiManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] posicionDeFabrica;
        [SerializeField] private AssetReference taxiAsset;
        private GameObject taxiCargado;
        private FabricaAbstracta factory;
        private int dificultad;

        private async void Awake()
        {
            dificultad = GameplaySettingsManager.Instance.DificultadOpcionActual;

            var operacion = Addressables.LoadAssetAsync<GameObject>(taxiAsset);

            taxiCargado = await operacion.Task;

            factory = new FabricaDeTaxis(taxiCargado);

            foreach (var item in posicionDeFabrica)
            {
                factory.CrearAuto(
                    transform,
                    item.transform.position,
                    item.transform.rotation,
                    dificultad
                );

                Destroy(item.gameObject);
            }
        }

        private void OnDestroy()
        {
            Addressables.Release(taxiCargado);
        }
    }
}