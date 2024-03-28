using UnityEngine;

namespace Homework
{
    public class Bootstrap : Behaviour
    {
        [SerializeField] private GameConfig _gameConfig;

        private Game _game;
        
        protected override void Start()
        {
            base.Start();
            
            Debug.Log("Инициализация обработчика корутинов");
            Coroutines.Initialize();

            Debug.Log("Создание игры");
            _game = new Game(_gameConfig);
            
            Debug.Log("Запуск игры");
            _game.Run();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            _game.Dispose();
        }
    }
}