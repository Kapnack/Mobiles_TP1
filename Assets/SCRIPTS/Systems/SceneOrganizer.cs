using System;
using Systems.SceneLoader;
using UnityEngine;

namespace Systems
{
    [RequireComponent(typeof(SceneLoader.SceneLoader))]
    public class SceneOrganizer : Singleton<SceneOrganizer>
    {
        private ISceneLoader _sceneLoader;

        [SerializeField] private SceneRef mainMenuScene;
        [SerializeField] private SceneRef gameplayScene;
        [SerializeField] private SceneRef endGameScene;

        private SceneRef[] _sceneToLoad;
        
        private LoadingScreen _loadingScreen;
        
        protected override void Awake()
        {
            base.Awake();
            
            _loadingScreen = GetComponent<LoadingScreen>();
                
            _sceneLoader = GetComponent<ISceneLoader>();

            LoadMainMenuScene();
        }

        public void LoadMainMenuScene()
        {
            _sceneToLoad = new[] {mainMenuScene};
            TryChangeScene(_sceneToLoad);
        }

        public void LoadGameplayScene()
        {
            _sceneToLoad = new[] {gameplayScene};
            TryChangeScene(_sceneToLoad);
        }

        public void LoadEndGameScene()
        {
            _sceneToLoad = new[] {endGameScene};
            
            TryChangeScene(_sceneToLoad);
        }

        private async void TryChangeScene(SceneRef[] newSceneRef)
        {
            _loadingScreen.StartLoadingScreen();
            
            try
            {
                await _sceneLoader.UnloadAll();
                await _sceneLoader.LoadSceneAsync(newSceneRef);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            _loadingScreen.EndLoadingScreen();
        }
    }
}