using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private Canvas HUDUnJugador;
    [SerializeField] private Canvas Jugador1HUD;
    [SerializeField] private Canvas Jugador2HUD;

    private void Awake()
    {
        Jugador1HUD?.gameObject.SetActive(GameplaySettingsManager.Instance.IsMultiplayer);
        Jugador2HUD?.gameObject.SetActive(GameplaySettingsManager.Instance.IsMultiplayer);

        HUDUnJugador?.gameObject.SetActive(!GameplaySettingsManager.Instance.IsMultiplayer);
    }
}