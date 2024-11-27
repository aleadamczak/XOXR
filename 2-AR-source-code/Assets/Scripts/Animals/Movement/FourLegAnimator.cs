using UnityEngine;

public class FourLegAnimator : MonoBehaviour
{
    private GameObject frontRLeg;
    private GameObject frontLLeg;
    private GameObject backRLeg;
    private GameObject backLLeg;

    public float maxRotationAngle = 20f;
    public float rotationSpeed = 5f;

    private Quaternion initialFRLegRotation;
    private Quaternion initialFLLegRotation;
    private Quaternion initialBRLegRotation;
    private Quaternion initialBLLegRotation;

    private bool isMoving;
    private float timeCounter;
    private Vector3 lastPosition;


    void Start()
    {
        InitializeLegs();
        lastPosition = transform.position;
        initialFRLegRotation = frontRLeg.transform.localRotation;
        initialFLLegRotation = frontLLeg.transform.localRotation;
        initialBRLegRotation = backRLeg.transform.localRotation;
        initialBLLegRotation = backLLeg.transform.localRotation;
    }

    GameObject FindLeg(string legName)
    {
        return Utility.FindChildByName(transform, legName);
    }

    void InitializeLegs()
    {
        if (gameObject.name.Contains("Cow"))
        {
            backLLeg = FindLeg("BRLeg");
            backRLeg = FindLeg("BLLeg");
            frontLLeg = FindLeg("FLLeg");
            frontRLeg = FindLeg("FRLeg");
        }
        else
        {
            backLLeg = FindLeg("LegBL");
            backRLeg = FindLeg("LegBR");
            frontLLeg = FindLeg("LegFL");
            frontRLeg = FindLeg("LegFR");
        }
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

        frontRLeg.transform.localRotation = initialFRLegRotation * Quaternion.Euler(0, 0, legRotation);
        backLLeg.transform.localRotation = initialBLLegRotation * Quaternion.Euler(0, 0, legRotation);

        frontLLeg.transform.localRotation = initialFLLegRotation * Quaternion.Euler(0, 0, -legRotation);
        backRLeg.transform.localRotation = initialBRLegRotation * Quaternion.Euler(0, 0, -legRotation);
    }

    private void StayStill()
    {
        frontRLeg.transform.localRotation = Quaternion.Slerp(frontRLeg.transform.localRotation, initialFRLegRotation,
            Time.deltaTime * rotationSpeed);
        frontLLeg.transform.localRotation = Quaternion.Slerp(frontLLeg.transform.localRotation, initialFLLegRotation,
            Time.deltaTime * rotationSpeed);
        backRLeg.transform.localRotation = Quaternion.Slerp(backRLeg.transform.localRotation, initialBRLegRotation,
            Time.deltaTime * rotationSpeed);
        backLLeg.transform.localRotation = Quaternion.Slerp(backLLeg.transform.localRotation, initialBLLegRotation,
            Time.deltaTime * rotationSpeed);

        timeCounter = 0f;
    }
}