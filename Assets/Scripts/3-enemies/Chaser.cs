using UnityEngine;

public class Chaser : TargetMover
{
    [Tooltip("The object that we try to chase")]
    [SerializeField] Transform targetObject = null;

    //private Sleep sleepScript;

    private void Start()
    {
        //sleepScript = GetComponent<Sleep>();
    }

    public Vector3 TargetObjectPosition()
    {
        return targetObject.position;
    }

    private void Update()
    {
        // Check if the sleep script is enabled, if yes, do not chase
        //if (sleepScript != null && sleepScript.enabled)
        //    return;

        // Update the target position only if the sleep script is not enabled
        SetTarget(targetObject.position);
    }
}
