using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class Controllers : MonoBehaviour
{
    private static Controllers _instance;

    [SerializeField] private List<AController> controllersList;
    private Dictionary<EControllerType, AController> controllersDictonary = new Dictionary<EControllerType, AController>();

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        foreach (var controller in controllersList)
        {
            controllersDictonary.Add(controller.ControllerType, controller);
        }
        _instance = this;
    }

    public static bool GetController<T>(EControllerType type, out T controllerOut) where T : MonoBehaviour
    {
        var controller = _instance.controllersDictonary[type];
        controllerOut = controller.GetComponent<T>();
        return controllerOut != null;
    }
}
