using Systems;
using UnityEngine;
using UnityEngine.UI;

public class GameSetUp : MonoBehaviour
{
    [Header("HUD")] [SerializeField] private Button ButtonUnJugador;
    [SerializeField] private Button ButtonDosJugadores;
    [SerializeField] private Button BotonFacil;
    [SerializeField] private Button BotonNormal;
    [SerializeField] private Button BotonDificil;

    protected void Awake()
    {
        ModoDeUnJugador();
        ModoFacil();
    }

    public void ModoDeUnJugador()
    {
        GameplaySettingsManager.Instance.IsMultiplayer = false;
        ButtonUnJugador.image.color = Color.yellow;
        ButtonDosJugadores.image.color = Color.white;
    }

    public void ModoDeDosJugadores()
    {
        GameplaySettingsManager.Instance.IsMultiplayer = true;
        ButtonDosJugadores.image.color = Color.yellow;
        ButtonUnJugador.image.color = Color.white;
    }

    public void EmpezarJuego()
    {
        SceneOrganizer.Instance.LoadGameplayScene();
    }

    public void ModoFacil()
    {
        GameplaySettingsManager.Instance.DificultadOpcionActual = 0;
        BotonFacil.image.color = Color.yellow;
        BotonNormal.image.color = Color.white;
        BotonDificil.image.color = Color.white;
    }

    public void ModoNormal()
    {
        GameplaySettingsManager.Instance.DificultadOpcionActual = 1;
        BotonFacil.image.color = Color.white;
        BotonNormal.image.color = Color.yellow;
        BotonDificil.image.color = Color.white;
    }

    public void ModoDificil()
    {
        GameplaySettingsManager.Instance.DificultadOpcionActual = 2;
        BotonFacil.image.color = Color.white;
        BotonNormal.image.color = Color.white;
        BotonDificil.image.color = Color.yellow;
    }
}