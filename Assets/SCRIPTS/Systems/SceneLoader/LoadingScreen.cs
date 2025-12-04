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
            canvas?.gameObject.SetActive(false);
        }

        public void StartLoadingScreen()
        {
            canvas?.gameObject.SetActive(true);

            _isLoading = true;
            StartCoroutine(UpdateLoading());
        }

        private IEnumerator UpdateLoading()
        {
            float timer = 0f;

            while (_isLoading)
            {
                if (!_isLoading)
                {
                    canvas?.gameObject.SetActive(false);
                    yield break;
                }

                float progress = _data.GetCurrentLoadingProgress() != 0
                    ? _data.GetCurrentLoadingProgress() * 100.0f
                    : 0;

                slider.value = progress;

                timer += Time.deltaTime;

                if (timer >= 2f && (progress <= 0f || progress >= 100f))
                {
                    canvas?.gameObject.SetActive(false);
                    _isLoading = false;
                    yield break;
                }

                yield return null;
            }
        }

        public void EndLoadingScreen()
        {
            _isLoading = false;
            canvas?.gameObject.SetActive(false);
        }
    }
}