using System.Collections;
using UnityEngine;

public class Hand : MonoBehaviour
{
    Animator animator;

    [Header("Stats")]
    public float speed;
    int dir = -1;

    [Header("Pausing")]
    [SerializeField] float pauseTime = 0.2f;
    bool paused = false;

    [Header("Collision")]
    [SerializeField] Collider2D col;

    [Header("Coloring")]
    [SerializeField] Color color;
    [SerializeField] SpriteRenderer sprite;

    private void Start()
    {
        animator = GetComponent<Animator>();
        sprite.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        if (paused)
            return;

        float rot = speed * dir;
        Vector3 rotationAmount = new Vector3(0f, 0f, rot * Time.deltaTime);
        transform.Rotate(rotationAmount);
    }

    public void Activate()
    {
        animator.SetTrigger("Impact");
        StartCoroutine(MinuteHandStopped());
    }

    IEnumerator MinuteHandStopped()
    {
        paused = true;
        dir *= -1;

        col.gameObject.SetActive(true);

        yield return new WaitForSeconds(pauseTime);

        col.gameObject.SetActive(false);
        paused = false;
    }
}
