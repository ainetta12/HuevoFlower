using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
   
   [SerializeField] private Camera cam;

   private Vector3 previousPosition;
    
   float  touchesPrevPosDifference, touchesCurPosDifference, zoomModifier;
   Vector2 firstTouchPrevPos, secondTouchPrevPos;

   [SerializeField] float zoomModifierSpeed = 0.1f;
   [SerializeField] Text text;

   void Start()
   {
     //cam = GetComponent<Camera> ();
   }
    
    void Update()
    {
        text.text = Input.touchCount.ToString();

        if (Input.GetMouseButtonDown(0))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);

            cam.transform.RotateAround(new Vector3(), new Vector3(1,0,0), direction.y * 0);
            cam.transform.RotateAround(new Vector3(), new Vector3(0,1,0), -direction.x * 180);

            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.touchCount == 2)
        {
            Touch firstTouch = Input.GetTouch (0);
            Touch secondTouch = Input.GetTouch (1);

            firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            touchesPrevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

            zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomModifierSpeed;

            if(touchesPrevPosDifference > touchesCurPosDifference)
            {
                cam.orthographicSize += zoomModifier;
        
            }

            if(touchesPrevPosDifference < touchesCurPosDifference)
            {
                cam.orthographicSize += zoomModifier;
        
            }

            cam.orthographicSize = Mathf.Clamp (cam.orthographicSize, 2f, 10f);

            //touchcount -> do smthng

        }

    }

    
}
