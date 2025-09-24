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

        protected override void Awake()
        {
            base.Awake();
            
            _sceneLoader = GetComponent<ISceneLoader>();

            LoadMainMenuScene();
        }

        public async void LoadMainMenuScene()
        {
            try
            {
                await _sceneLoader.UnloadAll();
                await _sceneLoader.LoadSceneAsync(mainMenuScene);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async void LoadGameplayScene()
        {
            try
            {
                await _sceneLoader.UnloadAll();
                await _sceneLoader.LoadSceneAsync(gameplayScene);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async void LoadEndGameScene()
        {
            try
            {
                await _sceneLoader.UnloadAll();
                await _sceneLoader.LoadSceneAsync(endGameScene);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}