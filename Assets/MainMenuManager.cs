using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Canvas MenuPrincipal;
    [SerializeField] private Canvas Creditos;
    [SerializeField] private GameObject SplashScreen;

    private void Awake()
    {
        if (GameplaySettingsManager.Instance.isFirstLoad)
        {
            SplashScreen.gameObject.SetActive(true);
            GameplaySettingsManager.Instance.isFirstLoad =  false;
        }
        else
            AbrirMenuPrincipal();
    }

    public void PlayGameScene()
    {
        GameplaySettingsManager.Instance.ActivarCanvas();
        MenuPrincipal.gameObject.SetActive(false);
    }

    public void AbrirMenuPrincipal()
    {
        MenuPrincipal.gameObject.SetActive(true);
        Creditos.gameObject.SetActive(false);
    }

    public void AbrirCreditos()
    {
        Creditos.gameObject.SetActive(true);
        MenuPrincipal.gameObject.SetActive(false);
    }

    public void CerrarJuego()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}