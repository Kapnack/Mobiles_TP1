using System;
using UnityEngine;
using System.Collections;
using Systems;
using TMPro;
using UnityEngine.InputSystem;

public class MngPts : MonoBehaviour
{
    [SerializeField] private InputActionReference salirDelJuego;

    public float TiempEspReiniciar = 5;
    
    [SerializeField] private GameObject puntajeJugador1;
    [SerializeField] private GameObject puntajeJugador2;

    [SerializeField] private TMP_Text puntajeJugador1Texto;
    private string puntajeJugador1Formato;

    [SerializeField] private TMP_Text puntajeJugador2Texto;
    private string puntajeJugador2Formato;
    private bool cambioEscenaNoTrigereada = false;

    private void Awake()
    {
        if (salirDelJuego != null)
            salirDelJuego.action.started += SalirDelJuegoConInput;

        puntajeJugador1Formato = puntajeJugador1Texto.text;
        puntajeJugador1Texto.text = String.Format(puntajeJugador1Formato, DatosPartida.PtsPerdedor);

        puntajeJugador2Formato = puntajeJugador2Texto.text;
        puntajeJugador2Texto.text = String.Format(puntajeJugador2Formato, DatosPartida.PtsGanador);
    }

    private void OnDestroy()
    {
        if (salirDelJuego != null)
            salirDelJuego.action.started -= SalirDelJuegoConInput;
    }

    public void CargarNivel()
    {
        if (cambioEscenaNoTrigereada)
            return;

        cambioEscenaNoTrigereada = true;
        SceneOrganizer.Instance.LoadGameplayScene();
    }

    private void SalirDelJuegoConInput(InputAction.CallbackContext _)
    {
        SalirPrograma();
    }

    public void SalirPrograma()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void VolverAlMenuPrincipal()
    {
        if (cambioEscenaNoTrigereada)
            return;

        cambioEscenaNoTrigereada = true;
        SceneOrganizer.Instance.LoadMainMenuScene();
    }

    private void Start()
    {
        SetGanador();
    }

    private void SetGanador()
    {
        StartCoroutine(GanadorTexto(DatosPartida.LadoGanadaor == DatosPartida.Lados.Der
            ? puntajeJugador1
            : puntajeJugador2));
    }

    private IEnumerator GanadorTexto(GameObject ganadorTexto)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            ganadorTexto.SetActive(!ganadorTexto.activeSelf);
        }
    }
}