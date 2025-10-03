using UnityEngine;
using UnityEngine.InputSystem;

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

    public bool Habilitado = true;
    private CarController carController;

    private void Awake()
    {
        carController = GetComponent<CarController>();

        string mapName = InputAct == TipoInput.AWSD ? "Jugador1" : "Jugador2";
        _inputField = _inputSystem.FindActionMap(mapName, true);

        inputMovimiento = _inputField.FindAction("Movimiento", true);
        _inputField.Enable();
    }

    private void OnDestroy()
    {
        _inputField?.Disable();
    }

    private void Update()
    {
        if(!_inputField.enabled)
            _inputField.Enable();
        
        if (Habilitado)
        {
            float giro = inputMovimiento.ReadValue<float>();
            carController.SetGiro(giro);
        }
    }
}