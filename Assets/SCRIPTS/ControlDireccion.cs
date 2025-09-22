using UnityEngine;
using System.Collections;

public class ControlDireccion : MonoBehaviour
{
    public enum TipoInput
    {
        AWSD,
        Arrows
    }

    public TipoInput InputAct = TipoInput.AWSD;

    float Giro = 0;

    public bool Habilitado = true;

    private CarController carController;

    //---------------------------------------------------------//

    private void Awake()
    {
        carController = GetComponent<CarController>();
    }

    private void Update()
    {
        switch (InputAct)
        {
            case TipoInput.AWSD:

                if (Habilitado)
                {
                    if (Input.GetKey(KeyCode.A))
                    {
                        carController.SetGiro(-1);
                    }

                    if (Input.GetKey(KeyCode.D))
                    {
                        carController.SetGiro(1);
                    }
                }

                break;
            case TipoInput.Arrows:

                if (Habilitado)
                {
                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        carController.SetGiro(-1);
                    }

                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        carController.SetGiro(1);
                    }
                }

                break;
        }
    }

    public float GetGiro() => Giro;
}