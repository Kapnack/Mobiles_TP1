using UnityEngine;

public class HUDFinalManager : MonoBehaviour
{
    [SerializeField] private GameObject player2Texto;
    [SerializeField] private GameObject fueraDeServicio;

    private void Awake()
    {
        player2Texto.SetActive(GameplaySettingsManager.Instance.IsMultiplayer);
        fueraDeServicio.SetActive(!GameplaySettingsManager.Instance.IsMultiplayer);
    }
}
