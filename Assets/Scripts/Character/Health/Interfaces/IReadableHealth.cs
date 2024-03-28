using System;

namespace Homework
{
    public interface IReadableHealth
    {
        event Action<float> ValueChanged;
        
        float Value { get; }
        float MaxValue { get; }
        bool IsEmpty { get; }
    }
}