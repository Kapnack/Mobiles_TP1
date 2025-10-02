using UnityEngine;

public class CanvasDescargaManager : MonoBehaviour
{
    [SerializeField] GameObject canvasDescargaSoloJugador;
    [SerializeField] GameObject canvasDescargaPJ1;
    [SerializeField] GameObject canvasDescargaPJ2;

    private void Awake()
    {
        canvasDescargaSoloJugador.gameObject.SetActive(!GameplaySettingsManager.Instance.IsMultiplayer);
        canvasDescargaPJ1.gameObject.SetActive(GameplaySettingsManager.Instance.IsMultiplayer);
        canvasDescargaPJ2.gameObject.SetActive(GameplaySettingsManager.Instance.IsMultiplayer);
    }
}