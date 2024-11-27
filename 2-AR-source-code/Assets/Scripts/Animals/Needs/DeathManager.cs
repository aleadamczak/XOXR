using UnityEngine;

public class DeathManager : MonoBehaviour
{
    public float rotationSpeed = 180f;
    public float dropHeight = 0f;

    private bool isDead;
    private bool hasFallen;

    private GameObject hungerBar;
    private GameObject thirstBar;
    private IdleAnimalWalk idleWalk;
    private LedgeDetection ledgeDetection;

    private float elapsedTime = 0f;
    public float moveDuration = 0.5f;
    private Vector3 targetPosition;
    private bool isMoving;

    private GameObject animalModel;

    private Vector3 startingPosition;

    private AnimalSoundController soundController;


    void Start()
    {
        hungerBar = Utility.FindChildByName(transform, "HungerBar");
        thirstBar = Utility.FindChildByName(transform, "ThirstBar");
        animalModel = Utility.FindChildByName(transform, "Armature");
        idleWalk = GetComponent<IdleAnimalWalk>();
        ledgeDetection = GetComponent<LedgeDetection>();
        soundController = GetComponent<AnimalSoundController>();
    }

    void Update()
    {
        if (isDead && !hasFallen)
        {
            Rotate();
        }

        if (isMoving)
        {
            Drop();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Die();
        }
    }

    public void Die()
    {
        
        idleWalk.SetShouldMove(false);
        ledgeDetection.StopAllCoroutines();
        ledgeDetection.enabled = false;
        Vector3 currentPos = animalModel.transform.position;
        startingPosition = new Vector3(currentPos.x, currentPos.y, currentPos.z);
        targetPosition = new Vector3(currentPos.x, currentPos.y - dropHeight, currentPos.z);
        isMoving = true;
        isDead = true;
        HideBars();
        soundController.Hurt();
        AnimalCounter.animalCount--;
        Destroy(gameObject, 2f);
    }

    void HideBars()
    {
        hungerBar.gameObject.SetActive(false);
        thirstBar.gameObject.SetActive(false);
    }

    void Rotate()
    {
        Vector3 positionAtDeath = animalModel.transform.position;
        animalModel.transform.position =
            new Vector3(positionAtDeath.x, startingPosition.y - dropHeight, positionAtDeath.z);
        if (animalModel.transform.eulerAngles.x < 358)
        {
            animalModel.transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        }
        else
        {
            hasFallen = true;
        }
    }

    private void Drop()
    {
        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / moveDuration);
        animalModel.transform.position = Vector3.Lerp(startingPosition, targetPosition, t);
        if (t >= 1f)
        {
            isMoving = false;
        }
    }
}