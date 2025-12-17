using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class PlayerInputSystem : MonoBehaviour
{
    
    public static PlayerInputSystem Instance;
    
    private Dictionary<InputMode, IInputActionHandler> inputHandlers = new();
    private InputMode currentMode = InputMode.Note;
    
    private PlayerControls PlayerControls { get; set; }
    private InputDevice currentDevice;
    
    public delegate void DeviceChangeDelegate(InputDevice newDevice);
    public event DeviceChangeDelegate OnDeviceChangeEvent;


    private void Awake()
    {
        Instance = this;
        AutoRegisterHandlers();
    }

    private void OnEnable()
    {
        PlayerControls = new PlayerControls();
        PlayerControls.Enable();
        InputSystem.onEvent += WatchInputChange;
        SetInputMode(InputMode.Note);
    }

    private void OnDisable()
    {
        PlayerControls.Disable();
        InputSystem.onEvent -= WatchInputChange;
    }

    /// <summary>
    /// Auto-registra todos los handlers que tengan el atributo [AutoRegisterInput]
    /// </summary>
    private void AutoRegisterHandlers()
    {
        // Busca todos los tipos en el ensamblado actual que implementen IInputActionHandler
        var handlerTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(IInputActionHandler).IsAssignableFrom(type) 
                          && !type.IsInterface 
                          && !type.IsAbstract
                          && type.GetCustomAttributes(typeof(AutoRegisterInputAttribute), false).Length > 0);

        foreach (var handlerType in handlerTypes)
        {
            // Obtiene el atributo para saber qué modo es
            var attribute = (AutoRegisterInputAttribute)handlerType
                .GetCustomAttributes(typeof(AutoRegisterInputAttribute), false)[0];
            
            // Añade el componente y lo registra
            var handler = (IInputActionHandler)gameObject.AddComponent(handlerType);
            inputHandlers[attribute.Mode] = handler;
            
            Debug.Log($"Auto-registrado: {handlerType.Name} para modo {attribute.Mode}");
        }
    }
    
    /// <summary>
    /// Obtiene un handler específico por su tipo
    /// </summary>
    public T GetHandler<T>() where T : class, IInputActionHandler
    {
        foreach (var handler in inputHandlers.Values)
        {
            if (handler is T typedHandler)
            {
                return typedHandler;
            }
        }
        
        Debug.LogWarning($"No se encontró handler de tipo {typeof(T).Name}");
        return null;
    }

    /// <summary>
    /// Cambia al modo de input especificado
    /// </summary>
    public void SetInputMode(InputMode mode)
    {
        if (!inputHandlers.ContainsKey(mode))
        {
            Debug.LogWarning($"No hay handler registrado para el modo {mode}");
            return;
        }

        // Desactiva el modo actual
        if (inputHandlers.ContainsKey(currentMode))
        {
            inputHandlers[currentMode].Disable(PlayerControls);
        }

        // Activa el nuevo modo
        currentMode = mode;
        inputHandlers[mode].Enable(PlayerControls);

    }


    /// <summary>
    /// Comprueba si el dispositivo actual es ratón o teclado
    /// </summary>
    private bool IsMouseKeyboard(InputDevice device)
    {
        if (device == null) return false;
        return device.GetType().BaseType == typeof(Keyboard) || 
               device.GetType().BaseType == typeof(Mouse);
    }

    /// <summary>
    /// Observa cambios en el dispositivo de input
    /// </summary>
    private void WatchInputChange(InputEventPtr evento, InputDevice control)
    {
        if (control != currentDevice)
        {
            currentDevice = control;
            OnDeviceChangeEvent?.Invoke(currentDevice);
            
        }
    }

    // Métodos públicos para mantener compatibilidad con código existente
    public void HandleNotesControl() => SetInputMode(InputMode.Note);
}