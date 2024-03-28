using UnityEngine;

namespace Homework
{
    [CreateAssetMenu(fileName = "New Game Config", menuName = "Homework/Game/Config")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private UserInterface _userInterfacePrefab;
        [SerializeField] private InputHandler _inputHandlerPrefab;
        
        [SerializeField] private Level[] _levels;

        public Player PlayerPrefab => _playerPrefab;
        public UserInterface UserInterfacePrefab => _userInterfacePrefab;
        public InputHandler InputHandlerPrefab => _inputHandlerPrefab;
        
        public Level[] Levels => _levels;
    }
}