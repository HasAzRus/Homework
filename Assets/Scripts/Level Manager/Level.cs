using UnityEngine;

namespace Homework
{
    [CreateAssetMenu(fileName = "New Level", menuName = "Homework/Level")]
    public class Level : ScriptableObject, ILevel
    {
        [SerializeField] private string _name;
        public string Name => _name;
    }
}