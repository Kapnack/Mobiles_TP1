using Systems;
using Systems.SceneLoader;
using UnityEngine;
using UnityEngine.UI;

public class PausaManager : MonoBehaviour
{
    [Header("Boton de Pausa")] [SerializeField]
    private RectTransform rectTransform;

    [SerializeField] private Button button;
    [SerializeField] private GameObject panel;

    [Header("Botones Menu Pausa")] [SerializeField]
    private Button menuPrincipal;
    [SerializeField] private Button pausaBotonEnMenu;
    [SerializeField] private Button resetear;

    private bool _pausado = false;

    private void Awake()
    {
        button.onClick.AddListener(ManejarPause);
        pausaBotonEnMenu.onClick.AddListener(ManejarPause);
        menuPrincipal.onClick.AddListener(OnMenuPrincipal);
        resetear.onClick.AddListener(OnResetear);
    }

    private void Start()
    {
        ActualizarPosicion();
        panel.SetActive(false);
    }

    private void ActualizarPosicion()
    {
        if (GameplaySettingsManager.Instance.IsMultiplayer)
        {
            rectTransform.anchorMin = new Vector2(0.5f, 1f);
            rectTransform.anchorMax = new Vector2(0.5f, 1f);
            rectTransform.pivot = new Vector2(0.5f, 1f);

            rectTransform.anchoredPosition = new Vector2(0f, -80f);
        }
        else
        {
            rectTransform.anchorMin = new Vector2(1f, 1f);
            rectTransform.anchorMax = new Vector2(1f, 1f);
            rectTransform.pivot = new Vector2(1f, 1f);

            rectTransform.anchoredPosition = new Vector2(-80f, -20f);
        }
    }

    private void ManejarPause()
    {
        _pausado = !_pausado;
        button.gameObject.SetActive(!_pausado);
        panel.SetActive(_pausado);
        Time.timeScale = _pausado ? 0f : 1f;
    }

    private void OnMenuPrincipal()
    {
        ManejarPause();
        SceneOrganizer.Instance?.LoadMainMenuScene();
    }

    private void OnResetear()
    {
        ManejarPause();
        SceneOrganizer.Instance?.LoadGameplayScene();
    }
}