using UnityEngine;
using System.Collections;

public class MouseButton : MonoBehaviour {

    void OnMouseDown()
    {
        Debug.Log("onmouse-down");
    }
    void OnMouseUp()
    {
        Debug.Log("onmouse-up");
    }
}
