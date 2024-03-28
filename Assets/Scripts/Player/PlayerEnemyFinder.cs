using UnityEngine;

namespace Homework
{
    public sealed class PlayerEnemyFinder : Behaviour
    {
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _layerMask;

        [SerializeField] private int _maxEnemiesCount;

        private int _enemiesCount;
        private Collider2D[] _enemies;
        
        private Transform _transform;
        
        public Transform NearestEnemy { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            _enemies = new Collider2D[_maxEnemiesCount];
        }

        protected override void Start()
        {
            base.Start();

            _transform = transform;
        }

        private void UpdateNearestEnemy()
        {
            var minDistance = Mathf.Infinity;
            
            foreach (var enemy in _enemies)
            {
                if (enemy == null)
                {
                    continue;
                }
                
                var enemyTransform = enemy.transform;
                var distance = Vector3.Distance(_transform.position, enemyTransform.position);

                if (distance > minDistance)
                {
                    continue;
                }

                minDistance = distance;
                NearestEnemy = enemyTransform;
            }
        }

        protected override void Update()
        {
            base.Update();

            _enemiesCount = Physics2D.OverlapCircleNonAlloc(_transform.position, _radius, _enemies, _layerMask);

            if (CheckEnemies())
            {
                UpdateNearestEnemy();
            }
        }

        public bool CheckEnemies()
        {
            return _enemiesCount > 0;
        }
    }
}