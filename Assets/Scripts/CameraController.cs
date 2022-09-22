using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
	private CameraControlActions cameraActions;
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
		cameraActions = new CameraControlActions();
		cameraTransform = camera.transform;
	}

	private void OnEnable()
	{
		zoomHeight = camera.transform.localPosition.y;
		cameraTransform.LookAt(lookTarget);

		lastPosition = this.transform.position;

		movement = cameraActions.Camera.Movement;
		cameraActions.Camera.RotateCamera.performed += RotateCamera;
		//	cameraActions.Camera.ZoomCamera.performed += ZoomCamera;
		cameraActions.Camera.Enable();
	}

	private void OnDisable()
	{
		cameraActions.Camera.RotateCamera.performed -= RotateCamera;
		//	cameraActions.Camera.ZoomCamera.performed -= ZoomCamera;
		cameraActions.Camera.Disable();
	}

	private void Update()
	{
		//inputs
		GetKeyboardMovement();
		//DragCamera();

		//move base and camera objects
		UpdateVelocity();
		UpdateBasePosition();
		//	UpdateCameraPosition();
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
		Debug.Log("input:" +inputValue);
		Debug.Log("smooth input :" + movementInputValue);
		//	inputValue = inputValue.normalized;



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
				targetDirection += startDrag - ray.GetPoint(distance);
		}
	}



	private void UpdateBasePosition()
	{
		if (targetDirection.sqrMagnitude > 0.1f)
		{
			//create a ramp up or acceleration
			speed = Mathf.Lerp(speed, maxSpeed, Time.deltaTime * acceleration);
			transform.position += targetDirection * speed * Time.deltaTime;
		}
		else
		{
			//	targetDirection = Vector3.zero;
			//create smooth slow down
			horizontalVelocity = Vector3.Lerp(horizontalVelocity, Vector3.zero, Time.deltaTime * damping);
			transform.position += horizontalVelocity * Time.deltaTime;
		}
		targetDirection = Vector3.zero;
		//reset for next frame
		//targetPosition = Vector3.zero;
	}

	private void ZoomCamera(InputAction.CallbackContext obj)
	{
		float inputValue = obj.ReadValue<Vector2>().y / 100f;

		if (Mathf.Abs(inputValue) > 0.1f)
		{
			zoomHeight = cameraTransform.localPosition.y + inputValue * stepSize;

			if (zoomHeight < minHeight)
				zoomHeight = minHeight;
			else if (zoomHeight > maxHeight)
				zoomHeight = maxHeight;
		}
	}

	private void UpdateCameraPosition()
	{
		//set zoom target
		Vector3 zoomTarget = new Vector3(cameraTransform.localPosition.x, zoomHeight, cameraTransform.localPosition.z);
		//add vector for forward/backward zoom
		zoomTarget -= zoomSpeed * (zoomHeight - cameraTransform.localPosition.y) * Vector3.forward;

		cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, zoomTarget, Time.deltaTime * zoomDampening);
		cameraTransform.LookAt(this.transform);
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
