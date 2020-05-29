using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour
{
	public Vector3 CurrentMousePos,MousePosInitiale,DeltaMousePos,InitialePos;
	
    void Start()
    {
        //Fetch the Event Trigger component from your GameObject
        EventTrigger trigger = GetComponent<EventTrigger>();
        //Create a new entry for the Event Trigger
        EventTrigger.Entry entry1 = new EventTrigger.Entry();
		EventTrigger.Entry entry2 = new EventTrigger.Entry();
        //Add a Drag type event to the Event Trigger
        entry1.eventID = EventTriggerType.Drag;
		entry2.eventID = EventTriggerType.BeginDrag;
        //call the OnDragDelegate function when the Event System detects dragging
        entry1.callback.AddListener((data) => { onDrag(); });
		entry2.callback.AddListener((data) => { onBeginDrag(); });
        //Add the trigger entry
        trigger.triggers.Add(entry1);
		trigger.triggers.Add(entry2);
		//GetComponent<RectTransform>().ForceUpdateRectTransforms();
    }

	public void onBeginDrag()
	{
		InitialePos=transform.localPosition;
		MousePosInitiale = Input.mousePosition;
	}
	
	public void onDrag()
    {
        CurrentMousePos = Input.mousePosition;
		DeltaMousePos = CurrentMousePos-MousePosInitiale;
		Debug.Log(DeltaMousePos);
		transform.localPosition=DeltaMousePos+InitialePos;
    }
}
