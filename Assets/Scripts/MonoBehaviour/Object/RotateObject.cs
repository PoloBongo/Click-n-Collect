using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class RotateObject : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private ListObject listObject;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private Transform transformObject;
    [SerializeField] private Camera rotationCamera;

    [Header("Paramètres")]
    [SerializeField] private float rotationSpeed = 0.3f;
    [SerializeField] private float friction = 0.95f;
    [SerializeField] private bool invertRotation = false;

    private Vector2 _lastTouchPosition;
    private float _rotationVelocity;
    private bool _isTouching;

    private void Awake()
    {
        SpawnObject();
    }

    private void SpawnObject()
    {
        if (!listObject) return;
        targetObject = listObject.SetEquippedObject();
        if (!targetObject) return;
        targetObject = Instantiate(targetObject, Vector3.zero, Quaternion.identity);
        
        var meshes = listObject.GetMeshesObjectTarget();
        for (int i = 0; i < meshes.Count; i++)
        {
            var meshPrefab = meshes[i];
            if (!meshPrefab) continue;

            GameObject meshInstance = Instantiate(meshPrefab);
            meshInstance.transform.SetParent(targetObject.transform, false);
        }
        transformObject = targetObject.transform;
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        TouchSimulation.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
        TouchSimulation.Disable();
    }

     private void Update()
    {
        ProcessTouchInput();
        ApplyRotationInertia();
    }

    private void ProcessTouchInput()
    {
        foreach (var touch in Touch.activeTouches)
        {
            switch (touch.phase)
            {
                case UnityEngine.InputSystem.TouchPhase.Began:
                    _lastTouchPosition = touch.screenPosition;
                    _isTouching = true;
                    _rotationVelocity = 0f;
                    break;

                case UnityEngine.InputSystem.TouchPhase.Moved when _isTouching:
                    Vector2 currentPosition = touch.screenPosition;
                    float deltaX = currentPosition.x - _lastTouchPosition.x;
                    _lastTouchPosition = currentPosition;

                    _rotationVelocity = deltaX * rotationSpeed;
                    ApplyRotation(_rotationVelocity);
                    break;

                case UnityEngine.InputSystem.TouchPhase.Ended:
                case UnityEngine.InputSystem.TouchPhase.Canceled:
                    _isTouching = false;
                    break;
            }
        }
    }

    private void ApplyRotation(float deltaX)
    {
        if (!transformObject) return;

        float rotationAmount = invertRotation ? -deltaX : deltaX;
        transformObject.Rotate(Vector3.up, rotationAmount, Space.World);
    }

    private void ApplyRotationInertia()
    {
        if (_isTouching || Mathf.Abs(_rotationVelocity) < 0.01f) return;

        _rotationVelocity *= friction;
        ApplyRotation(_rotationVelocity * Time.deltaTime * 60f);
    }
}
