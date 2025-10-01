using Systems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplaySettingsManager : Singleton<GameplaySettingsManager>
{
    [Header("HUD")] [SerializeField] private Button ButtonUnJugador;
    [SerializeField] private Button ButtonDosJugadores;
    [SerializeField] private TMP_Dropdown DificultadOpciones;
    [SerializeField] private Canvas canvas;
    public int DificultadOpcionActual => DificultadOpciones.value;
    public bool IsMultiplayer;

    protected override void Awake()
    {
        base.Awake();
        DesactivarCanvas();
    }
    
    public void ModoDeUnJugador()
    {
        IsMultiplayer = false;
        ButtonUnJugador.image.color = Color.yellow;
        ButtonDosJugadores.image.color = Color.white;
    }

    public void ModoDeDosJugadores()
    {
        IsMultiplayer = true;
        ButtonDosJugadores.image.color = Color.yellow;
        ButtonUnJugador.image.color = Color.white;
    }

    public void ActivarCanvas() => canvas.gameObject.SetActive(true);

    public void DesactivarCanvas() => canvas.gameObject.SetActive(false);

    public void EmpezarJuego()
    {
        SceneOrganizer.Instance.LoadGameplayScene();
        canvas.gameObject.SetActive(false);
    }
        
}