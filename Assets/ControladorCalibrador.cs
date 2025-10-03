using UnityEngine;

public class ControladorCalibrador : MonoBehaviour
{
    [SerializeField] private GameObject Escena2;
    [SerializeField] private GameObject HUDUnJugador;
    [SerializeField] private GameObject HUDPJ1;
    [SerializeField] private GameObject HUDPJ2;

    private void Awake()
    {
        HUDPJ1.SetActive(false);
        HUDPJ2.SetActive(false);
        HUDUnJugador.SetActive(false);
        
        if (!GameplaySettingsManager.Instance.IsMultiplayer)
        {
#if UNITY_ANDROID || UNITY_IOS
            HUDPJ1.SetActive(false);
            HUDPJ2.SetActive(false);
            HUDUnJugador.SetActive(true);
#endif
            Escena2.SetActive(false);
        }
        else
        {
#if UNITY_ANDROID || UNITY_IOS
            HUDPJ1.SetActive(true);
            HUDPJ2.SetActive(true);
            HUDUnJugador.SetActive(false);
#endif

            Escena2.SetActive(true);
        }
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }
}