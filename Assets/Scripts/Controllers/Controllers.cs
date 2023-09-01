using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class Controllers : MonoBehaviour
{
    [SerializeField] private List<AController> controllersList;

    private static Controllers instance;
    private Dictionary<EControllerType, AController> controllersDictonary = new Dictionary<EControllerType, AController>();

    public static Controllers Instance => instance;

    private void Awake()
    {
        foreach (var controller in controllersList)
        {
            if (controllersDictonary.Keys.Contains(controller.ControllerType))
                Debug.LogError($"[Controllers][Awake] Controller with this type is already registered");
            else
                controllersDictonary.Add(controller.ControllerType, controller);
        }
        instance = this;
    }

    public bool GetController<T>(EControllerType type, out T controllerOut) where T : MonoBehaviour
    {
        var controller = instance.controllersDictonary[type];
        controllerOut = controller.GetComponent<T>();
        return controllerOut != null;
    }
}
