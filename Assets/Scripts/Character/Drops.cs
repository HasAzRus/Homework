using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Homework
{
    [Serializable]
    public class Drop
    {
        [SerializeField] [Range(0.1f, 1.0f)] private float _probability;
        [SerializeField] private int _maxCount;
        [SerializeField] private GameObject _item;

        public float Probability => _probability;
        public int MaxCount => _maxCount;
        public GameObject Item => _item;
    }
    
    [Serializable]
    public class Drops
    {
        [SerializeField] private Drop[] _drops;
        [SerializeField] private float _radius;

        public void Drop(Vector3 position)
        {
            var currentProbability = Random.Range(0f, 1f);
            
            foreach (var drop in _drops)
            {
                if (currentProbability > drop.Probability)
                {
                    continue;
                }

                var count = Random.Range(1, drop.MaxCount + 1);

                while (count > 0)
                {
                    var randomPositionInsideRadius = position + (Vector3)Random.insideUnitCircle * _radius;

                    Object.Instantiate(drop.Item, randomPositionInsideRadius, Quaternion.identity);
                    
                    count -= 1;
                }
            }
        }
    }
}