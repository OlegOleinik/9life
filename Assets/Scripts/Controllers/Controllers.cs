using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public sealed class Controllers : MonoBehaviour
{
    [SerializeField] private List<AController> controllersList;
    
    private static Controllers _instance = null;
    private Dictionary<EControllerType, AController> controllersDictonary = new Dictionary<EControllerType, AController>();

    public static Controllers Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Controllers();
                _instance.Init();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        Init();
    }

    public bool GetController<T>(EControllerType type, out T controllerOut) where T : MonoBehaviour
    {
        var controller = _instance.controllersDictonary[type];
        controllerOut = controller.GetComponent<T>();
        return controllerOut != null;
    }

    private void Init()
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
}
