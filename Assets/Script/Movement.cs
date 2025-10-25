using UnityEngine;

public class Movement : MonoBehaviour
{
    public float rotationSpeed = 10.0f;
    public float movementSpeed = 5.0f;
    private Vector3 targetPoint;

    private void Update()
    {
        //Retrieve the mouse click position by shooting a ray from the camera
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out RaycastHit hit, 100.0f))
        {
            //Take the point where the ray hits the ground plane as the target rotation
            targetPoint = hit.point;
            targetPoint.y = transform.position.y;
        }

        float distance = Vector3.Distance(transform.position, targetPoint);

        if (distance < 1.0f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
    }
}