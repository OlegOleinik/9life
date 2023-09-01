using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public sealed class Controllers : MonoBehaviour
{
    private static Controllers _instance = null;
    
    public static Controllers Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Controllers();
            }
            return _instance;
        }
    }

    [SerializeField] private List<AController> controllersList;
    private Dictionary<EControllerType, AController> controllersDictonary = new Dictionary<EControllerType, AController>();

    void Awake()
    {
        foreach (var controller in controllersList)
        {
            if (controllersDictonary.Keys.Contains(controller.ControllerType))
                Debug.LogError($"[Controllers][Awake] Controller with this type is already registered");
            else
                controllersDictonary.Add(controller.ControllerType, controller);
        }
        _instance = this;
    }

    public bool GetController<T>(EControllerType type, out T controllerOut) where T : MonoBehaviour
    {
        var controller = _instance.controllersDictonary[type];
        controllerOut = controller.GetComponent<T>();
        return controllerOut != null;
    }
}
