using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class InputPlacementManager : MonoBehaviour
{
    [SerializeField]
    private ARRaycastManager _arRaycastManager;

    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    private Vector2? _screenPos;

    void Update()
    {
        if (Touchscreen.current != null && Touchscreen.current.press.wasReleasedThisFrame)
        {
            _screenPos = Touchscreen.current.position.ReadValue();
            TryPlaceObjectAR(_screenPos.Value);
        }

        #if UNITY_EDITOR
        if (Mouse.current != null && Mouse.current.leftButton.wasReleasedThisFrame)
        {
            _screenPos = Mouse.current.position.ReadValue();
            TryPlaceObject(_screenPos.Value);
        }
        #endif
    }

    private void TryPlaceObject(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject objectToPlace = SelectionManager.Instance.GetSelectedPrefab();

            if (objectToPlace != null)
            {
                Instantiate(objectToPlace, hit.point, Quaternion.identity);
            }
        }
    }

    private void TryPlaceObjectAR(Vector2 screenPos)
    {
        if (_arRaycastManager.Raycast(screenPos, _hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = _hits[0].pose;
            GameObject prefab = SelectionManager.Instance.GetSelectedPrefab();

            if (prefab != null)
            {
                Instantiate(prefab, hitPose.position, hitPose.rotation);
            }
        }
    }
}
