using Systems;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGameScene()
    {
        SceneOrganizer.Instance.LoadGameplayScene();
    }
}
