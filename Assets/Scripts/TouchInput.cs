using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchInput : MonoBehaviour
{
    Camera _cam;
    public float lp_x_Correction;
    public float lp_y_Correction;

    public float rp_x_Correction;
    public float rp_y_Correction;

    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0) // If a touch is registered
        {
            foreach(Touch t in Input.touches) // Do x with all registered touches
            {
                if(Physics.Raycast(_cam.ScreenPointToRay(t.position), out RaycastHit TouchHit)) // If the touch hits something, do a thing
                {

                }
            }
        }

#if UNITY_EDITOR // We'll only be using the mouse in editor so lets ignore this block in player
        if (Input.GetMouseButtonDown(0)) // If the mouse is clicked
            if (Physics.Raycast(_cam.ScreenPointToRay(Input.mousePosition), out RaycastHit MouseHit))// If the mouse hits something, do a thing
            {
                if (MouseHit.collider.CompareTag("BookCover")) // if the mouse hits the book cover
                {
                    MouseHit.collider.GetComponentInParent<BookController>().OpenBook(); // Call the open book function of that book controller
                }
                if(MouseHit.collider.CompareTag("LeftPage") || MouseHit.collider.CompareTag("RightPage")) //|| MouseHit.collider.CompareTag("Book"))
                {
                    BookCanvasHit(MouseHit);
                }
                    
            }
#endif
    }

    void BookCanvasHit(RaycastHit originHit)
    {
        BookController bc = originHit.collider.GetComponentInParent<BookController>();

        Camera pageCam = bc.CanvasSections[bc.ActiveSection].LeftPageCamera;
        Canvas pageCanvas = bc.CanvasSections[bc.ActiveSection].LeftPageCanvas;

        Vector3 virtualPos = Vector3.zero;

        switch (originHit.collider.tag) // We have 2 seperate cameras (optimisation be damned) rendering the canvases for left and right pages. Here we just set the correct one
        {
            case "LeftPage":
                pageCam = bc.CanvasSections[bc.ActiveSection].LeftPageCamera;
                // Figure out where the pointer would be in the second camera based on texture position or RenderTexture.
                virtualPos = new Vector3(originHit.textureCoord.x + lp_x_Correction, originHit.textureCoord.y + lp_y_Correction);
                break;
            case "RightPage":
                pageCam = bc.CanvasSections[bc.ActiveSection].RightPageCamera;
                // Figure out where the pointer would be in the second camera based on texture position or RenderTexture.
                virtualPos = new Vector3(originHit.textureCoord.x + rp_x_Correction, originHit.textureCoord.y + rp_y_Correction);
                break;
        }


        //Debug.Log(originHit.textureCoord.x + " " + originHit.textureCoord.y);
        Ray ray = pageCam.ViewportPointToRay(virtualPos);

        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 10f);

        RaycastHit hit;
        

        if(Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.gameObject.GetComponent<Button>())
                hit.collider.gameObject.GetComponent<Button>().onClick.Invoke();
        }

        /*

        USING GRAPHIC RAYCASTER JUST DOESN'T WORK FOR SOME FUCKIN REASON, ONLY SUCCEEDS AT 0,0
        JUST PUT A FUCKIN BOX COLLIDER ON UI OBJECTS AND WE'RE SWEET USING PHYSICS

        GraphicRaycaster m_Raycaster = pageCanvas.GetComponent<GraphicRaycaster>();
        EventSystem m_EventSystem = pageCanvas.GetComponent<EventSystem>();
        PointerEventData m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = virtualPos;
        List<RaycastResult> results = new List<RaycastResult>();
        m_Raycaster.Raycast(m_PointerEventData, results);

        Debug.Log("RAY FIRED");
        foreach (RaycastResult result in results)
        {
            Debug.Log("Hit " + result.gameObject.name);
            if (result.gameObject.GetComponent<Button>())
                result.gameObject.GetComponent<Button>().onClick.Invoke();
        }*/
    }
}
