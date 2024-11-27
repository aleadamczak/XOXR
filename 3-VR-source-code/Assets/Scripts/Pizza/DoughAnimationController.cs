using UnityEngine;

public class DoughAnimationController : MonoBehaviour
{
    private Animator anim;
    public GameObject armature;
    public GameObject doughBall;
    public GameObject pizzaBase;
    public Collider[] boxColliders;
    private Rigidbody rb;
    private bool animationDone = false;

    void Start()
    {
        pizzaBase.SetActive(false);
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!animationDone && armature.gameObject.transform.localScale.x <= 81.24f)
        {
            animationDone = true;
            switchPizzaModel();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!animationDone && other.gameObject.tag == "rollingPin")
        {
            anim.SetTrigger("kneadStart");
            resumeAnimation();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!animationDone && other.gameObject.tag == "rollingPin")
        {
            stopAnimation();
        }
    }

    private void stopAnimation()
    {
        anim.SetFloat("speedMultiplier", 0);
    }

    private void resumeAnimation()
    {
        anim.SetFloat("speedMultiplier", 1);
    }

    private void switchPizzaModel()
    {
        pizzaBase.SetActive(true);
        Destroy(armature);
        Destroy(doughBall);
        rb.constraints = RigidbodyConstraints.None;
        rb.detectCollisions = false;
        foreach (Collider collider in boxColliders)
        {
            collider.enabled = false;
        }
    }
}