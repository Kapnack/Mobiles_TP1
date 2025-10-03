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
    private int estadoBolsa = 0;

    [SerializeField] private InputActionAsset _inputSystem;
    private InputActionMap _inputField;
    private InputAction inputIzq;
    private InputAction inpurDer;
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

        inputIzq = _inputField.FindAction("Izquierda");
        inpurDer = _inputField.FindAction("Derecha");
        inputAbajo = _inputField.FindAction("Abajo");

        inputIzq.started += IzquierdaManager;
        inpurDer.started += DerechaManager;
        inputAbajo.started += AbajoManager;
    }

    private void OnDestroy()
    {
        inputIzq.started -= IzquierdaManager;
        inpurDer.started -= DerechaManager;
        inputAbajo.started -= AbajoManager;
    }
    
    private void ADManager(InputAction.CallbackContext context)
    {
        if (Mathf.Approximately(context.ReadValue<float>(), -1))
        {
            PrimerPaso();
        }
        else if (Mathf.Approximately(context.ReadValue<float>(), 1))
        {
            TercerPaso();
        }
    }
    
    private void DerechaManager(InputAction.CallbackContext _) => TercerPaso();
    private void AbajoManager(InputAction.CallbackContext _) => SegundoPaso();
    private void IzquierdaManager(InputAction.CallbackContext _) => PrimerPaso();


    private void PrimerPaso()
    {
        if (Tenencia() || !Desde.Tenencia() || estadoBolsa != 0)
            return;

        Desde.Dar(this);
        estadoBolsa = 1;
    }

    private void SegundoPaso()
    {
        if (!Tenencia() || estadoBolsa != 1)
            return;

        Pallets[0].script.transform.position = transform.position;
        estadoBolsa = 2;
    }

    void TercerPaso()
    {
        if (estadoBolsa != 2 || !Tenencia())
            return;

        Dar(Hasta);
        estadoBolsa = 0;
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