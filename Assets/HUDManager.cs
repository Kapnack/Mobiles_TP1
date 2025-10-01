using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private Canvas Jugador1HUD;
    [SerializeField] private Canvas Jugador2HUD;

    private void Awake()
    {
        Jugador1HUD?.gameObject.SetActive(true);

        Jugador2HUD?.gameObject.SetActive(GameplaySettingsManager.Instance.IsMultiplayer);
    }
}