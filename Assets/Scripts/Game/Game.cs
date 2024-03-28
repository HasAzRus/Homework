using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Homework
{
    public class Game : IDisposable
    {
        public static event Action<Player> Beginning;
        
        private Player _player;

        private UserInterface _userInterface;
        private InputHandler _inputHandler;

        private int _currentLevel;

        private bool _isRunning;
        
        private readonly GameConfig _config;
        private readonly LevelManager _levelManager;
        private readonly GameData _gameData;

        public Game(GameConfig config)
        {
            _config = config;
            
            _levelManager = new LevelManager();
            
            _levelManager.Loading += OnLevelLoading;
            _levelManager.Loaded += OnLevelLoaded;
            
            Finish.Finished += OnFinished;

            _gameData = new GameData();
        }

        private IEnumerator BeginRoutine()
        {
            Debug.Log("Спавн игрока");
            
            var playerStart = Object.FindObjectOfType<PlayerStart>();

            if (playerStart == null)
            {
                throw new NullReferenceException("Отсутствует точка спавна игрока");
            }

            var playerStartTransform = playerStart.transform;

            _player = Object.Instantiate(_config.PlayerPrefab, playerStartTransform.position, Quaternion.identity);

            yield return null;

            _userInterface.Construct(_player);
            _inputHandler.Control(_player);
            
            yield return null;
            
            _player.Load(_gameData);
            
            Debug.Log("Начало игры");
            Beginning?.Invoke(_player);
        }

        private void OnLevelLoading()
        {
            if (!_isRunning)
            {
                return;
            }
            
            _player.Save(_gameData);
            
            _userInterface.Clear();
            _inputHandler.StopControlling();
        }

        private void OnLevelLoaded(ILevel level)
        {
            Coroutines.Run(BeginRoutine());
        }
        
        private void OnFinished()
        {
            GoNextLevel();
        }

        private void GoToLevel(int index)
        {
            _currentLevel = index;
            _levelManager.Load(_config.Levels[index]);
        }
            
        public void GoNextLevel()
        {
            var nextIndex = _currentLevel + 1;

            if (nextIndex >= _config.Levels.Length)
            {
                return;
            }
            
            GoToLevel(nextIndex);
        }
        
        public void Run()
        {
            Debug.Log("Cоздание пользовательского интерфейса");
            _userInterface = Object.Instantiate(_config.UserInterfacePrefab);
            
            Debug.Log("Cоздание обработчика ввода");
            _inputHandler = Object.Instantiate(_config.InputHandlerPrefab);

            _levelManager.Load(_config.Levels[0]);

            _isRunning = true;
        }

        public void Dispose()
        {
            _levelManager.Loaded -= OnLevelLoaded;

            Finish.Finished -= OnFinished;
        }
    }
}