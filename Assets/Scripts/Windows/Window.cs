using UnityEngine;

public enum EWindowType
{
    Pause = 0,
    Fail = 1,
}

public class Window : MonoBehaviour
{
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
