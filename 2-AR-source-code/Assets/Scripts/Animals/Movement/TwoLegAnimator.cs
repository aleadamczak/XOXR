using UnityEngine;

public class TwoLegAnimator : MonoBehaviour
{
    
    private GameObject RLeg;
    private GameObject LLeg;

    public float maxRotationAngle = 20f;
    public float rotationSpeed = 5f;

    private Quaternion initialRLegRotation;
    private Quaternion initialLLegRotation;

    private bool isMoving;
    private float timeCounter;
    private Vector3 lastPosition;

    void Start()
    {
        GameObject armature = Utility.FindChildByName(transform, "Armature");
        LLeg = Utility.FindChildByName(armature.transform, "LLeg");
        RLeg = Utility.FindChildByName(armature.transform, "RLeg");
        
        lastPosition = transform.position;
        initialRLegRotation = RLeg.transform.localRotation;
        initialLLegRotation = LLeg.transform.localRotation;
    }


    void Update()
    {
        if (transform.position != lastPosition)
        {
            isMoving = true;
            MoveLegs();
        }
        else
        {
            isMoving = false;
            StayStill();
        }
        
        lastPosition = transform.position;
    }

    private void MoveLegs()
    {
        timeCounter += Time.deltaTime * rotationSpeed;

        float legRotation = Mathf.Sin(timeCounter) * maxRotationAngle;

        RLeg.transform.localRotation = initialRLegRotation * Quaternion.Euler(0, 0, legRotation);
        LLeg.transform.localRotation = initialLLegRotation * Quaternion.Euler(0, 0, -legRotation);

    }

    private void StayStill()
    {
        RLeg.transform.localRotation = Quaternion.Slerp(RLeg.transform.localRotation, initialRLegRotation, Time.deltaTime * rotationSpeed);
        LLeg.transform.localRotation = Quaternion.Slerp(LLeg.transform.localRotation, initialLLegRotation, Time.deltaTime * rotationSpeed);
        timeCounter = 0f;
    }
}
