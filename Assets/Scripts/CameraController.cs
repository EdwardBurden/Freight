using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
	private GameControls gameControls;
	private InputAction movement;

	[Header("Input")]
	[SerializeField]
	private float smoothInputSpeed = 0.2f;

	[Header("Movement")]
	[SerializeField]
	private float maxSpeed = 30f;

	[SerializeField]
	private float acceleration = 10f;
	[SerializeField]
	private float damping = 15f;

	[Header("Zooming")]
	[SerializeField]
	private float stepSize = 2f;
	[SerializeField]
	private float zoomDampening = 7.5f;
	[SerializeField]
	private float minHeight = 5f;
	[SerializeField]
	private float maxHeight = 50f;
	[SerializeField]
	private float zoomSpeed = 2f;
	[SerializeField]
	private float forwardZoomAmount = 0.1f;

	[Header("Rotation")]
	[SerializeField]
	private float maxRotationSpeed = 0.1f;

	[Header("Transforms")]
	[SerializeField]
	private Camera camera;
	[SerializeField]
	private Transform cameraBase;
	[SerializeField]
	private Transform lookTarget;


	//Input
	private Vector2 movementInputValue;
	private Vector2 smoothInputVelocity;

	private float speed;
	private Transform cameraTransform;
	private Vector3 targetDirection;
	private float zoomHeight;
	private Vector3 horizontalVelocity;
	private Vector3 lastPosition;
	private Vector3 startDrag;

	private void Awake()
	{
		gameControls = new GameControls();
		cameraTransform = camera.transform;
	}

	private void OnEnable()
	{
		zoomHeight = cameraBase.transform.localPosition.y;
		cameraTransform.LookAt(lookTarget);

		lastPosition = this.transform.position;

		movement = gameControls.Camera.Movement;
		gameControls.Camera.RotateCamera.performed += RotateCamera;
		gameControls.Camera.ZoomCamera.performed += ZoomCamera;
		gameControls.Camera.Enable();
	}

	private void OnDisable()
	{
		gameControls.Camera.RotateCamera.performed -= RotateCamera;
		gameControls.Camera.ZoomCamera.performed -= ZoomCamera;
		gameControls.Camera.Disable();
	}

	private void Update()
	{
		//inputs
		GetKeyboardMovement();
		DragCamera();

		//move base and camera objects
		UpdateVelocity();
		UpdateBasePosition();
		UpdateCameraPosition();
	}

	private void UpdateVelocity()
	{
		horizontalVelocity = (this.transform.position - lastPosition) / Time.deltaTime;
		horizontalVelocity.y = 0f;
		lastPosition = this.transform.position;
	}

	private void GetKeyboardMovement()
	{
		Vector2 inputValue = movement.ReadValue<Vector2>();
		movementInputValue = Vector2.SmoothDamp(movementInputValue, inputValue, ref smoothInputVelocity, smoothInputSpeed);

		if (movementInputValue.sqrMagnitude > 0.1f)
		{
			targetDirection += movementInputValue.x * GetCameraRight() + movementInputValue.y * GetCameraForward();
		}
	}

	private void DragCamera()
	{
		if (!Mouse.current.rightButton.isPressed)
			return;

		//create plane to raycast to
		Plane plane = new Plane(Vector3.up, Vector3.zero);
		Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

		if (plane.Raycast(ray, out float distance))
		{
			if (Mouse.current.rightButton.wasPressedThisFrame)
				startDrag = ray.GetPoint(distance);
			else
				targetDirection = startDrag - ray.GetPoint(distance);
		}
	}



	private void UpdateBasePosition()
	{
		if (targetDirection.sqrMagnitude > 0.1f)
		{
			speed = Mathf.Lerp(speed, maxSpeed, Time.deltaTime * acceleration);
			transform.position += targetDirection * speed * Time.deltaTime;
		}
		else
		{
			horizontalVelocity = Vector3.Lerp(horizontalVelocity, Vector3.zero, Time.deltaTime * damping);
			transform.position += horizontalVelocity * Time.deltaTime;
		}
		targetDirection = Vector3.zero;
	}

	private void ZoomCamera(InputAction.CallbackContext obj)
	{
		float inputValue = -obj.ReadValue<Vector2>().y / 100f;

		if (Mathf.Abs(inputValue) > 0.1f)
		{
			zoomHeight = cameraBase.localPosition.y + inputValue * stepSize;
			zoomHeight = Mathf.Clamp(zoomHeight, minHeight, maxHeight);
		}
	}

	private void UpdateCameraPosition()
	{
		//set zoom target
		//Vector3 zoomTarget = new Vector3(cameraBase.localPosition.x, zoomHeight, cameraBase.localPosition.z);
		//add vector for forward/backward zoom
		Vector3 zoomForwardDistance = new Vector3(cameraBase.localPosition.x, zoomHeight, -zoomHeight * forwardZoomAmount); //testing now

		cameraBase.localPosition = Vector3.Lerp(cameraBase.localPosition, zoomForwardDistance, Time.deltaTime * zoomDampening);
		cameraTransform.LookAt(lookTarget);
	}

	private void RotateCamera(InputAction.CallbackContext obj)
	{
		if (!Mouse.current.middleButton.isPressed)
		{
			return;
		}
		float inputValue = obj.ReadValue<Vector2>().x;
		transform.rotation = Quaternion.Euler(0f, inputValue * maxRotationSpeed + transform.rotation.eulerAngles.y, 0f);
	}

	//gets the horizontal forward vector of the camera
	private Vector3 GetCameraForward()
	{
		Vector3 forward = cameraBase.forward;
		forward.y = 0f;
		return forward;
	}

	//gets the horizontal right vector of the camera
	private Vector3 GetCameraRight()
	{
		Vector3 right = cameraBase.right;
		right.y = 0f;
		return right;
	}
}
