using System;
using UnityEngine;

namespace Homework
{
    public class Finish
    {
        public static event Action Finished;

        public static void SetFinish()
        {
            Debug.Log("Финиш");
            
            Finished?.Invoke();
        }
    }
}