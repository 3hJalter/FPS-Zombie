using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
    private Transform cameraTarget;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothTime = 0.3f;
    // This value will change at the run time depending on target movement. Initialize with zero vector
    private Vector3 cameraVelocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        cameraTarget = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - cameraTarget.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        Vector3 targetPostition = cameraTarget.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPostition, ref cameraVelocity, smoothTime);
        transform.LookAt(cameraTarget);
    }
}
