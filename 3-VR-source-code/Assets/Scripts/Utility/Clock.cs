using UnityEngine;

public class Clock : MonoBehaviour
{
    public Transform pivotPoint;
    public float secondsToGoAround;
    public float timePassed;
    private bool ticking;
    private Quaternion ogRotation;
    private float degreesPerSecond;

    void Awake()
    {
        ogRotation = pivotPoint.rotation;
        degreesPerSecond = 360f / secondsToGoAround;
    }

    public void SetTime(float seconds)
    {
        timePassed = seconds;
        float degrees = degreesPerSecond * timePassed;
        pivotPoint.Rotate(0, degrees, 0);
    }

    public void StartTicking()
    {
        ticking = true;
    }

    public void StopTicking()
    {
        ticking = false;
    }

    public void Reset()
    {
        timePassed = 0;
        pivotPoint.rotation = ogRotation;
    }

    void Update()
    {
        if (ticking)
        {
            pivotPoint.Rotate(0, degreesPerSecond * Time.deltaTime, 0);
            timePassed += Time.deltaTime;
        }
    }
}
