using System.Threading.Tasks;
using UnityEngine;

namespace Fabrica
{
    public abstract class FabricaAbstracta
    {
        public abstract void CrearAuto(Transform parent, Vector3 position, Quaternion rotation, int dificultad);
    }
}