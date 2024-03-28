using System;
using System.Collections.Generic;
using UnityEngine;

namespace Homework
{
    public class GameData : IWritableData, IReadableData, IDisposable
    {
        private readonly Dictionary<string, object> _dictionary;

        public GameData()
        {
            _dictionary = new Dictionary<string, object>();
        }

        public void Write<T>(string key, T value)
        {
            _dictionary[key] = value;
        }

        public T Read<T>(string key, T valueByDefault)
        {
            if (_dictionary.TryGetValue(key, out var result))
            {
                return (T)result;
            }
            
            Write(key, valueByDefault);
            
            return valueByDefault;
        }

        public void Dispose()
        {
            _dictionary.Clear();
        }
    }
}