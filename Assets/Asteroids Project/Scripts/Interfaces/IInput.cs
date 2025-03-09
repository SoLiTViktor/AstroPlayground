using System;
using UnityEngine;

namespace AsteroidProject
{
    public interface IInput
    {
        public event Action<Vector2> AxisButtonsDown;
        public event Action AttackButtonDown;
        public event Action SuperAttackButtonDown;
    }
}