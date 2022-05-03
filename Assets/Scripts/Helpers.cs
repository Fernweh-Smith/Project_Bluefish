using System;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Helpers {

    public static bool IsOverUI() => EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    public static bool IsOverUI(int fingerID) => EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(fingerID);

            
}