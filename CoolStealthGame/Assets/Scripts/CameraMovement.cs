using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float XZsensitivity = 20f;
    [SerializeField] private float rotateSensitivity = 10f;
    
    private Camera _cam;
    [SerializeField]private LayerMask _groundLayerMask;

    private void Awake()
    {
        _cam = Camera.main;
    }
    private void FixedUpdate()
    {
        //regular movement (left, right, up, down)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        transform.position += transform.right * x * XZsensitivity * Time.deltaTime;
        transform.position += transform.forward * z * XZsensitivity * Time.deltaTime;

        
        //rotation
        float rotate = 0;
        
        if (Input.GetKey(KeyCode.Q)) { rotate = 1; }
        else if (Input.GetKey(KeyCode.E)) { rotate = -1; }
        else { rotate = 0; }

        float rotateX = rotate * rotateSensitivity * Time.deltaTime;
        
        // Rotation with raycast not useful in the long run
        RaycastHit hit;
        Physics.Raycast(transform.position, _cam.transform.forward, out hit, 100f, _groundLayerMask);
        
        transform.RotateAround(hit.point, Vector3.up, rotateX);
    }

}
