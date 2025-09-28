using Systems;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    
    public void PlayGameScene()
    {
        GameplaySettingsManager.Instance.ActivarCanvas();
        canvas.gameObject.SetActive(false);
    }
}
