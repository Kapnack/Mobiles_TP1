using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControlDireccion : MonoBehaviour
{
    public enum TipoInput
    {
        AWSD,
        Arrows
    }

    public TipoInput InputAct = TipoInput.AWSD;

    [SerializeField] private InputActionAsset _inputSystem;
    private InputActionMap _inputField;
    private InputAction inputMovimiento;
    
    float Giro = 0;

    public bool Habilitado = true;

    private CarController carController;

    //---------------------------------------------------------//

    private void Awake()
    {
        carController = GetComponent<CarController>();

        if (InputAct == TipoInput.AWSD)
        {
            _inputField = _inputSystem.FindActionMap("Jugador1", true);
        }
        else if (InputAct == TipoInput.Arrows)
        {
            _inputField = _inputSystem.FindActionMap("Jugador2", true);
        }
        
        inputMovimiento = _inputField.FindAction("Movimiento");
        
        _inputField.Enable();
    }

    private void Update()
    {
        int lolo;
        
        if (inputMovimiento.ReadValue<float>() > 1.0f)
             lolo = 1;
            
        carController.SetGiro(inputMovimiento.ReadValue<float>());
        
        /*
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
        */
    }
}