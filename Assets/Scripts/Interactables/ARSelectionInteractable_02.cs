
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class ARSelectionInteractable_02 : ARSelectionInteractable
{
    protected override bool CanStartManipulationForGesture(TapGesture gesture) => !EventSystem.current.IsPointerOverGameObject(gesture.fingerId);
    
}
