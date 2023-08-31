using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EControllerType
{
    Ground = 0,
    Player = 1,
    Enemies = 2,
    Bullet = 3,
}

public abstract class AController : MonoBehaviour
{
    [SerializeField] private EControllerType _controllerType;
    
    public EControllerType ControllerType => _controllerType;
}
