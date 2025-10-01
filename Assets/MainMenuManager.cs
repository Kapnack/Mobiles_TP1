using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Canvas MenuPrincipal;
    [SerializeField] private Canvas Creditos;

    private void Awake()
    {
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