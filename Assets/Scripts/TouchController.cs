using UnityEngine;

public class TouchController : MonoBehaviour
{
    private float rotationSpeed = 0.5f;
    private float zoomSpeed = 0.1f;

    private Vector2 prevTouchPos = Vector2.zero;

     private float minZoom = 100;
    private float maxZoom = 200;

    void Update()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            // Rotar el objeto
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Rotate(0, 0, -touchDeltaPosition.x * rotationSpeed);
        }

         if (Input.touchCount == 2)
        {
            // Hacer zoom en el objeto
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            float newScale = transform.localScale.x - deltaMagnitudeDiff * zoomSpeed;
            newScale = Mathf.Clamp(newScale, minZoom, maxZoom);

            transform.localScale = new Vector3(newScale, newScale, newScale);
        }
    }
}