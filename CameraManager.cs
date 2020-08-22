using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _controlPoints;
    [SerializeField] private float _cameraStepDistance;

    private List<Vector3> _screenPoistions;
    private float _screeWidth;
    private float _screenHeight;
    private Camera _camera;
    private bool isAllPointsVisible;


    private void Awake()
    {
        isAllPointsVisible = false;
        _camera = GetComponent<Camera>();
        _screenHeight = Screen.height;
        _screeWidth = Screen.width;
    }

    private void Update()
    {
        SetCameraPosition();
    }

    private void SetCameraPosition()
    {
        while (!isAllPointsVisible)
        {
            _screenPoistions = new List<Vector3>();
            foreach (Transform point in _controlPoints)
            {
                _screenPoistions.Add(_camera.WorldToScreenPoint(point.position));
            }

            foreach (Vector3 pos in _screenPoistions)
            {
                if (!isPointVisible(pos))
                {
                    BackspaceCamera();
                    break;
                }
                else if (_screenPoistions.IndexOf(pos) == _screenPoistions.Count - 1 && isPointVisible(pos))
                {
                    isAllPointsVisible = true;
                }
            }
        }
    }

    private bool isPointVisible(Vector3 pos)
    {
        if (pos.x > _screeWidth || pos.x < 0)
        {
            return false;
        }

        if (pos.y > _screenHeight || pos.y < 0)
        {
            return false;
        }

        return true;
    }

    private void BackspaceCamera()
    {
        _camera.transform.position +=
            _camera.transform.forward * _cameraStepDistance * -Time.deltaTime * _cameraStepDistance;
    }
}