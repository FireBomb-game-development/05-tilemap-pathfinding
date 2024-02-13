using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : MonoBehaviour
{
    [SerializeField] float sleepAngle;
    [SerializeField] float standAngle;
    [SerializeField] int sleepingTime;
    [SerializeField] Vector3 angularVelocity = new Vector3(0, 0, 30);
    private float angle = 0;
    private int direction = 1;




    // Start is called before the first frame update
    void Start()
    {
        // Reset the rotation vector to make the object stand
        transform.eulerAngles = Vector3.zero;
    }

    public IEnumerator sleep(int time)
    {
        while (angle > sleepAngle)
        {
            transform.Rotate(angularVelocity * Time.deltaTime);
            angle -= direction * Time.deltaTime;
            Debug.Log("Rotating to sleep, angle: " + angle);
            yield return null; // Ensure the coroutine yields to allow other processes to run
        }

        for (int i = 0; i < sleepingTime; i++)
        {
            yield return new WaitForSeconds(time);
            Debug.Log("Waiting...");
        }

        while (angle > standAngle)
        {
            transform.Rotate(-angularVelocity * Time.deltaTime); // Reverse rotation
            angle -= direction * Time.deltaTime;
            Debug.Log("Rotating to stand, angle: " + angle);
            yield return null;
        }
    }


    // Update is called once per frame
    void Update()
    {
        // Calculate the target angle
        float targetAngle = angle + 90f;

        // Rotate until the current angle is less than the target angle
        if (angle < targetAngle)
        {
            transform.Rotate(angularVelocity * Time.deltaTime);
            angle += direction * Time.deltaTime;
            Debug.Log("Rotating to sleep, angle: " + angle);
        }

        // Ensure the angle is exactly 90 degrees
        if (angle >= targetAngle)
        {
            angle = targetAngle;
        }
    }

}
