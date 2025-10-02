using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Fabrica
{
    public class FabricaDeTaxis : FabricaAbstracta
    {
        private GameObject taxiAsset;

        public FabricaDeTaxis(GameObject taxiAsset)
        {
            this.taxiAsset = taxiAsset;
        }

        public override void CrearAuto(Transform parent, Vector3 position, Quaternion rotation, int dificultad)
        {

            var GO = Object.Instantiate(taxiAsset, position, rotation, parent);

            var taxiComp = GO.GetComponent<TaxiComp>();
            if (taxiComp != null)
                taxiComp.Vel *= dificultad + 1;
        }
    }
}