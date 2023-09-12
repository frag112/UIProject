using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        public float panningSpeed = 10f;

        public float maxHeight = 10f; 
        public float minHeight = 15f; 
        public float heightDampening = 5f; 
        public float scrollWheelZoomingSensitivity = 25f;

        private float zoomPos = 0; 
        public float limitX = 50f; 
        public float limitY = 50f; 

        private string zoomingAxis = "Mouse ScrollWheel";

        private float ScrollWheel
        {
            get { return Input.GetAxis(zoomingAxis); }
        }

        private Vector2 MouseAxis
        {
            get { return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); }
        }

        private void Update()
        {
            Move();
            HeightCalculation();
            LimitPosition();
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            print(eventData.position);
        }

        private void Move()
        {
            if(Input.GetKey(KeyCode.Mouse0) && MouseAxis != Vector2.zero)
            {
                Vector3 desiredMove = new Vector3(-MouseAxis.x, 0, -MouseAxis.y);
               desiredMove = desiredMove * Time.deltaTime * panningSpeed;
                desiredMove = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f)) * desiredMove;
                desiredMove = transform.InverseTransformDirection(desiredMove);

                transform.Translate(desiredMove, Space.Self);
            }
        }


        private void HeightCalculation()
        {
            float distanceToGround = transform.position.y;
            zoomPos += ScrollWheel * Time.deltaTime * scrollWheelZoomingSensitivity;

            zoomPos = Mathf.Clamp01(zoomPos);

            float targetHeight = Mathf.Lerp(minHeight, maxHeight, zoomPos);
            float difference = 0; 

            if(distanceToGround != targetHeight)
                difference = targetHeight - distanceToGround;

            transform.position = Vector3.Lerp(transform.position, 
                new Vector3(transform.position.x, targetHeight + difference, transform.position.z), Time.deltaTime * heightDampening);
        }

        private void LimitPosition()
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -limitX, limitX),
                transform.position.y,
                Mathf.Clamp(transform.position.z, -limitY, limitY));
        }
    }