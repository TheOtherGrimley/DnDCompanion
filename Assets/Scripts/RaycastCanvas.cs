using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RaycastCanvas : MonoBehaviour
{
    GraphicRaycaster m_Raycaster;
    EventSystem m_EventSystem;
    // Start is called before the first frame update
    void Start()
    {
        m_Raycaster = this.GetComponent<GraphicRaycaster>();
        m_EventSystem = this.GetComponent<EventSystem>();
        
    }

    public void CastToCanvas(Vector3 virtualPos)
    {
        RaycastHit hit;
        PointerEventData m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = virtualPos;
        List<RaycastResult> results = new List<RaycastResult>();

        m_Raycaster.Raycast(m_PointerEventData, results);

        foreach (RaycastResult result in results)
        {
            Debug.Log("Hit " + result.gameObject.name);
            if (result.gameObject.GetComponent<Button>())
                result.gameObject.GetComponent<Button>().onClick.Invoke();
        }
    }
}
