using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PalletMover : ManejoPallets
{
    public MoveType miInput;

    public enum MoveType
    {
        WASD,
        Arrows
    }

    public ManejoPallets Desde, Hasta;
    bool segundoCompleto = false;

    [SerializeField] private InputActionAsset _inputSystem;
    private InputActionMap _inputField;
    private InputAction inputMovimiento;
    private InputAction inputAbajo;

    private void Awake()
    {
        if (miInput == MoveType.WASD)
        {
            _inputField = _inputSystem.FindActionMap("Jugador1", true);
        }
        else if (miInput == MoveType.Arrows)
        {
            _inputField = _inputSystem.FindActionMap("Jugador2", true);
        }

        inputMovimiento = _inputField.FindAction("Movimiento");
        inputAbajo = _inputField.FindAction("Abajo");

        inputMovimiento.started += ADManager;
        inputAbajo.started += AbajoManager;
    }

    private void OnDestroy()
    {
        inputMovimiento.started -= ADManager;
        inputAbajo.started -= AbajoManager;
    }

    /*
    private void Update()
    {
        switch (miInput)
        {
            case MoveType.WASD:
                if (!Tenencia() && Desde.Tenencia() && Input.GetKeyDown(KeyCode.A))
                {
                    PrimerPaso();
                }

                if (Tenencia() && Input.GetKeyDown(KeyCode.S))
                {
                    SegundoPaso();
                }

                if (segundoCompleto && Tenencia() && Input.GetKeyDown(KeyCode.D))
                {
                    TercerPaso();
                }

                break;
            case MoveType.Arrows:
                if (!Tenencia() && Desde.Tenencia() && Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    PrimerPaso();
                }

                if (Tenencia() && Input.GetKeyDown(KeyCode.DownArrow))
                {
                    SegundoPaso();
                }

                if (segundoCompleto && Tenencia() && Input.GetKeyDown(KeyCode.RightArrow))
                {
                    TercerPaso();
                }

                break;
        }
    }
*/
    private void ADManager(InputAction.CallbackContext context)
    {
        if (Mathf.Approximately(context.ReadValue<float>(), -1))
        {
            PrimerPaso();
        }
        else if (Mathf.Approximately(context.ReadValue<float>(), 1))
        {
            SegundoPaso();
        }
    }

    private void AbajoManager(InputAction.CallbackContext _) => TercerPaso();

    private void PrimerPaso()
    {
        if (Tenencia() || !Desde.Tenencia())
            return;

        Desde.Dar(this);
        segundoCompleto = false;
    }

    private void SegundoPaso()
    {
        if (!Tenencia())
            return;

        Pallets[0].script.transform.position = transform.position;
        segundoCompleto = true;
    }

    void TercerPaso()
    {
        if (!segundoCompleto || !Tenencia())
            return;

        Dar(Hasta);
        segundoCompleto = false;
    }

    public override void Dar(ManejoPallets receptor)
    {
        if (Tenencia())
        {
            if (receptor.Recibir(Pallets[0].script))
            {
                Pallets.RemoveAt(0);
            }
        }
    }

    public override bool Recibir(Pallet pallet)
    {
        if (!Tenencia())
        {
            pallet.Portador = this.gameObject;
            base.Recibir(pallet);
            return true;
        }
        else
            return false;
    }
}