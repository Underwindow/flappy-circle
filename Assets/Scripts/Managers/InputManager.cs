using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviourSingletonPersistent<InputManager>
{
    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnStartTouch;
    public delegate void EndTouchEvent(Vector2 position, float time);
    public event EndTouchEvent OnEndTouch;

    private TouchControls touchControls;

    public override void Awake()
    {
        base.Awake();
        touchControls = new TouchControls();
    }

    private void Start()
    {
        touchControls.Touch.TouchPress.started += ctx => StartTouch(ctx);
        touchControls.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call<bool>("moveTaskToBack", true);
        }
    }

    private void OnEnable()
    {
        touchControls.Enable();
        //TouchSimulation.Enable();

        //UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerDown;
    }

    private void OnDisable()
    {
        touchControls.Disable();
        //TouchSimulation.Disable();

        //UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= FingerDown;
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        //Debug.Log($"StartTouch {touchControls.Touch.TouchPosition.ReadValue<Vector2>()}");

        OnStartTouch?.Invoke(touchControls.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
    }

    private void EndTouch(InputAction.CallbackContext context)
    {
        //Debug.Log($"EndTouch {touchControls.Touch.TouchPosition.ReadValue<Vector2>()}");

        OnEndTouch?.Invoke(touchControls.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.time);
    }

    //private void FingerDown(Finger finger)
    //{
    //    OnStartTouch?.Invoke(finger.screenPosition, Time.time);
    //}
}
