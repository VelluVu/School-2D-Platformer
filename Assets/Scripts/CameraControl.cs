using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    Transform player;
    bool isShaking;
    public float smoothTime = 0.3F;
    float cameraHeight;
    private Vector3 velocity = Vector3.zero;
    private Vector3 offset;

    private void Start()
    {
        cameraHeight = -10f;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


    void LateUpdate()
    {
        if (!isShaking)
        {
            //transform.position = player.transform.position;

            //offset = transform.position - player.position;
            // Define a target position above and behind the target transform
            if (player != null)
            {
                Vector3 targetPosition = player.TransformPoint(new Vector3(0, 0, -10));


                // Smoothly move the camera towards that target position
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            }
        }
    }

    public IEnumerator Shake(float duration, float magnitude)
    {

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            isShaking = true;
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        isShaking = false;

    }
}
