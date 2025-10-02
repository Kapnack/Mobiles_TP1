using entityStates;
using UnityEngine;

public class ControladorCalibrador : MonoBehaviour
{
    [SerializeField] private GameObject Escena2;
    [SerializeField] private GameObject HUDUnJugador;
    [SerializeField] private GameObject HUDPJ1;
    [SerializeField] private GameObject HUDPJ2;

    private void Awake()
    {
        if (!GameplaySettingsManager.Instance.IsMultiplayer)
        {
            HUDPJ1.SetActive(false);
            HUDPJ2.SetActive(false);
            Escena2.SetActive(false);
            HUDUnJugador.SetActive(true);
        }
        else
        {
            HUDPJ1.SetActive(true);
            HUDPJ2.SetActive(true);
            Escena2.SetActive(true);
            HUDUnJugador.SetActive(false);
        }
    }

    private void OnDisable()
    {
       Destroy(gameObject);
    }
}
