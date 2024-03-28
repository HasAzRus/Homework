using System;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Homework
{
    public class LevelManager
    {
        public event Action Loading;
        public event Action<ILevel> Loaded;
        
        private ILevel _currentLevel;
        
        private IEnumerator LoadRoutine(ILevel level)
        {
            Loading?.Invoke();
            
            if (_currentLevel != null)
            {
                yield return SceneManager.UnloadSceneAsync(_currentLevel.Name);
            }

            var operation = SceneManager.LoadSceneAsync(level.Name, LoadSceneMode.Additive);
            operation.allowSceneActivation = false;

            while (!operation.isDone)
            {
                if (operation.progress >= 0.9f)
                {
                    operation.allowSceneActivation = true;
                }

                yield return null;
            }

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(level.Name));
            
            _currentLevel = level;
            
            Loaded?.Invoke(level);
        }

        public void Load(ILevel level)
        {
            Coroutines.Run(LoadRoutine(level));
        }
    }
}