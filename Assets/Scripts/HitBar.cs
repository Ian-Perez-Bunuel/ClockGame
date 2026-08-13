using System.Collections;
using UnityEngine;

public class HitBar : MonoBehaviour
{
    Animator animator;

    [SerializeField] float shrinkSpeed;
    bool shrinking = true;

    ScoreDisplay scoreDisplay;

    public void Init(ScoreDisplay s)
    {
        animator = GetComponent<Animator>();

        scoreDisplay = s;
        gameObject.SetActive(false);
    }

    public void Spawn(Vector3 pos, float rotation)
    {
        gameObject.SetActive(true);
        transform.localScale = new Vector3(1f, 1f, 1f);
        shrinking = true;

        transform.position = pos;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation + 90f);
    }

    private void Remove()
    {
        gameObject.SetActive(false);
        // Screen shake
        CameraShake.StartMediumShake();
    }


    // Update is called once per frame
    void Update()
    {
        Shrink();
    }

    void Shrink()
    {
        if (!shrinking)
            return;

        Vector3 currentScale = transform.localScale;
        float speed = shrinkSpeed * Time.deltaTime;
        Vector3 newScale = currentScale - new Vector3(speed, speed, speed);

        transform.localScale = newScale;

        if (transform.localScale.x <= 0.001f)
        {
            Remove();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered by: " + other.name);

        if (other.CompareTag("Player"))
        {
            scoreDisplay.ChangeValue(-50);

            animator.SetTrigger("Hit");
            shrinking = false;
            // Remove handled via animation event
        }
    }        
}
