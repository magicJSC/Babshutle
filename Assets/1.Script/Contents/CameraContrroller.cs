using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContrroller : MonoBehaviour
{
    public GameObject target;
   [SerializeField] Vector2 center;
   [SerializeField] Vector2 size;

    float height;
    float width;
    void Start()
    {
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }
    void Update()
    {
        transform.position = target.transform.position;

        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);
        float ly = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }
}