using UnityEngine;

public class ControladorCalibrador : MonoBehaviour
{
    [SerializeField] private GameObject Escena2;

    private void Awake()
    {
        if (!GameplaySettingsManager.Instance.IsMultiplayer)
            Escena2.SetActive(false);
    }
}
