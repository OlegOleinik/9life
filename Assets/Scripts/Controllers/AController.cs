using UnityEngine;

public enum EControllerType
{
    Ground = 0,
    Player = 1,
    Enemies = 2,
    Bullet = 3,
    Windows = 4,
    Pause = 5,
}

public abstract class AController : MonoBehaviour
{
    [SerializeField] private EControllerType controllerType;
    
    public EControllerType ControllerType => controllerType;
}
