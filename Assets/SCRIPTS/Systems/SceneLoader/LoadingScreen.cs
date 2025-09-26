using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Systems.SceneLoader
{
    public class LoadingScreen : MonoBehaviour
    {
        private ILoadingData _data;

        private bool _isLoading;

        [FormerlySerializedAs("_canvas")] [Header("Canvas")] [SerializeField]
        private Canvas canvas;

        [SerializeField] private Slider slider;

        private void Awake()
        {
            _data = GetComponent<ILoadingData>();
            slider.minValue = 0;
            slider.maxValue = 100;
        }

        public void StartLoadingScreen()
        {
            canvas?.gameObject.SetActive(true);

            _isLoading = true;
            StartCoroutine(UpdateLoading());
        }

        private IEnumerator UpdateLoading()
        {
            while (_isLoading)
            {
                var progress = _data.GetCurrentLoadingProgress() != 0 ? _data.GetCurrentLoadingProgress() * 100.0f : 0;

                slider.value = progress;

                yield return null;
            }
            
            yield return new WaitForSeconds(2f);
            canvas?.gameObject.SetActive(false);
        }

        public void EndLoadingScreen()
        {
            _isLoading = false;
        }
    }
}