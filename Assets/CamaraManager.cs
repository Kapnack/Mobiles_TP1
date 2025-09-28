using UnityEngine;

public class CamaraManager : MonoBehaviour
{
    [SerializeField] private Camera[] CamerasP1 = new Camera[3];
    [SerializeField] private Camera[] CamerasP2 = new Camera[3];

    private void Start()
    {
        if (GameplaySettingsManager.Instance.IsMultiplayer)
            return;

        foreach (var cam in CamerasP1)
        {
            cam.gameObject.SetActive(true);
            var res = cam.rect;
            res.x = 0;
            res.width = 1;
            cam.rect = res;
        }

        foreach (var cam in CamerasP2)
            cam.gameObject.SetActive(false);
    }
}