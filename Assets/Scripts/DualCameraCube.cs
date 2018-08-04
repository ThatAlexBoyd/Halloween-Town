/*************************
This is the cleanest Cam Cube 9/1/15
 * **********************/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
public class DualCameraCube : MonoBehaviour
{
	private Camera cam; //the main camera that is being used

	[Header("Camera Settings")]
	public float velocity = 5; //how fast the camera moves around the object
    public bool camFree = true; //if the camera is free to move/zoom
	
	public float yMinLimit = 0; //the minimum angle the camera is allowed to go
	public float yMaxLimit = 0;//the maximum angle the camera is allowed to go
	
	//Rotatations around - set to full 360 degree by default
    private float xMinLimit = -361;
	private float xMaxLimit = 360;
	
	[Header("Zoom Settings")]
	public float zoomStart; //the distance you want the camera to start at
    public float zoomMin = 15; //minimum distance the camera can get away from the cube
    public float zoomMax = 45f; //maximum distance the camera can get away from the cube
	public float zoomMobileMultipler = 1; //to help handle the zoom sesntivity on mobile/touch
	
	private float smoothTime = 2f;
	
    //Calculation variables for rotation
	private float rotationYAxis = 0.0f;
	private float rotationXAxis = 0.0f;
	private float velocityX = 0.0f;
	private float velocityY = 0.0f;
	

	public float zoomSensitvity = 4f; //how far the camera moves toward or away from the cube per "click"
	public float zoomSpeed = 15f; //how long the movement toward or away takes
	private float zoom; //current distance camera is from the cube
	
    //Calculation variables for touch
	private Vector3 delta = Vector3.zero;
	private Vector3 lastPos = Vector3.zero;
	
	[Header("AutoRotate")]
	public bool autoRotateEnabled = false; //rotation is allowed to happen
	public float autoRotateSpeed = .5f; //how quickly to rotate
	public float autoRotateDelay = 4.0f; //how long it takes to begin auto rotation
	private float idleTimer = 0; //internal counter for how long the camera has been idle

	
	
	void Start()
	{
        cam = gameObject.transform.GetChild(0).gameObject.GetComponent<Camera>(); //auto finding the camera - No more dragging in!

		Vector3 angles = transform.eulerAngles;
		rotationYAxis = angles.y;
		rotationXAxis = angles.x;
		cam.transform.localPosition = new Vector3 (cam.transform.localPosition.x, cam.transform.localPosition.y, -zoomStart); //set the camera to starting zoom posiiton
		zoom = zoomStart; //record the current zoom as the start zoom
		
	}

    void Update()
    {
        if (camFree)
        {
            MouseOrbit();
            Zoom();
        }
    }

	void MouseOrbit ()
	{
		if (Input.GetMouseButtonDown (0)) {
			idleTimer = 0;
			lastPos = Input.mousePosition;
		}
		else
		if (Input.GetMouseButton (0)) {
			idleTimer = 0;
			delta = Input.mousePosition - lastPos;
			lastPos = Input.mousePosition;
		}
		if(Input.GetMouseButtonUp(0)){
			delta.x = 0;
			delta.y = 0;
		}else{
			idleRotate();
		}
		MouseOrbitLate();
	}

	void MouseOrbitLate ()
	{
		if (true) 
        {
			velocityX += delta.x * velocity * 0.02f * Time.deltaTime;
			velocityY += delta.y * velocity * 0.02f * Time.deltaTime;
		}
		rotationYAxis += velocityX;
		rotationXAxis -= velocityY;
		rotationXAxis = ClampAngleY (rotationXAxis, yMinLimit, yMaxLimit);
		rotationYAxis = ClampAngleX (rotationYAxis, xMinLimit, xMaxLimit);
		transform.rotation = Quaternion.Euler (rotationXAxis, rotationYAxis, 0);
		velocityX = Mathf.Lerp (velocityX, 0, Time.deltaTime * smoothTime);
		velocityY = Mathf.Lerp (velocityY, 0, Time.deltaTime * smoothTime);
	}

    void Zoom()
    {
        zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitvity;
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            zoom += deltaMagnitudeDiff * Time.deltaTime * zoomSensitvity * zoomMobileMultipler;
        }
        zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);

        ZoomLate();
    }

    void ZoomLate()
    {
        cam.transform.localPosition = new Vector3(
            cam.transform.localPosition.x,
            cam.transform.localPosition.y,
            Mathf.Lerp(cam.transform.localPosition.z, -zoom, Time.deltaTime * zoomSpeed));
    }

    /// <summary>
    /// Pass a degree you wish to camera to rotate on one axis
    /// </summary>
    /// <param name="toDegree"></param>
    public void RotateCameraHorizontally(float toDegree)
    {
        velocityX = 0;
        velocityY = 0;
        transform.DORotate(new Vector3(transform.rotation.x, toDegree, transform.rotation.z),1f).OnComplete(KeepCameraFromJumpingBack);
        
    }
    public void RotateCameraVertically(Vector3 rot)
    {
        velocityX = 0;
        velocityY = 0;
        transform.DORotate(rot, 1f).OnComplete(KeepCameraFromJumpingBack);
    }
    private void KeepCameraFromJumpingBack()
    {

        rotationXAxis = transform.rotation.eulerAngles.x;
        rotationYAxis = transform.rotation.eulerAngles.y;
    }
    public void MoveCamera(Vector3 newLoc)
    {
        transform.DOLocalMove(newLoc, 1f);
    }

    private void idleRotate()
    {
        idleTimer += 0.01f;

        if (idleTimer > autoRotateDelay && autoRotateEnabled)
        {
            delta = new Vector2(autoRotateSpeed, 0.0f);
        }
    }


    private float ClampAngleY(float angle, float min, float max)
    {
        return ClampAngle(angle, min, max);
    }

    private float ClampAngleX(float angle, float min, float max)
    {
        if (max < min)
        {
            //the clamp is crossing the 360/0 degrees threshold
            if (angle < -0F)
                angle += 360F;
            if (angle > 360F)
                angle -= 360F;
            if (angle > min && 360 >= angle)
            {
                //angle is between 360 and min
                return ClampAngle(angle, min, 360);
            }
            else if (max > angle && 0 <= angle)
            {
                //angle is between 0 and max
                return ClampAngle(angle, 0, max);
            }
            else
            {
                //angle is outside the bounds of the min/max limit
                //find out which side we need to clamp to
                float outmid = (min - max) / 2;
                if (angle < outmid + max)
                {
                    return ClampAngle(angle, 0, max);
                }
                else
                {
                    return ClampAngle(angle, min, 360);
                }
            }
        }
        else
        {
            //clamp as normal
            return ClampAngle(angle, min, max);
        }
    }

    public void ChangeClampX(float min, float max)
    {
        xMinLimit = min;
        xMaxLimit = max;
    }

    public void ChangeClampY(float min, float max)
    {
        yMinLimit = min;
        yMaxLimit = max;
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
    public float GetXRotMinVal() { return yMinLimit; }
    public float GetXRotMaxVal() { return yMaxLimit; }

}
