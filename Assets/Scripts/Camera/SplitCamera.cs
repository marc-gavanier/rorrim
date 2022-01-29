using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SplitCamera : MonoBehaviour
{
    public Camera leftCamera;

    public Camera rightCamera;

    private Camera mainCamera;

    private void Awake() {
        mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // mainCamera.targetTexture = leftCamera.activeTexture;
    }
}
