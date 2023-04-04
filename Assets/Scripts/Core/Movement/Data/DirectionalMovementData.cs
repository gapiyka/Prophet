using System;
using Core.Enums;
using UnityEngine;

namespace Core.Movement.Data
{
    [Serializable]
    public class DirectionalMovementData
    {
        [field: SerializeField] public float Speed { get; private set; }
    }
}