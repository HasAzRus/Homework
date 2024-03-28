using UnityEngine;

namespace Homework
{
    public class EnemySpawner : Behaviour
    {
        [SerializeField] private Vector2 _mapHalfSize;
        
        [SerializeField] private int _count;
        [SerializeField] private Enemy[] _enemyPrefabs;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            Game.Beginning += OnGameBeginning;
        }

        private void OnGameBeginning(Player player)
        {
            var cachedTransform = transform;
            var count = _count;

            while (count > 0)
            {
                var x = Random.Range(-_mapHalfSize.x, _mapHalfSize.x);
                var y = Random.Range(-_mapHalfSize.y, _mapHalfSize.y);

                var position = cachedTransform.position + new Vector3(x, y);

                var index = Random.Range(0, _enemyPrefabs.Length);

                var enemy = Instantiate(_enemyPrefabs[index], position, Quaternion.identity);
                enemy.Construct(player);
                
                count -= 1;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.zero, _mapHalfSize * 2);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            Game.Beginning -= OnGameBeginning;
        }
    }
}