using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreDisplay : MonoBehaviour
{
    Animator animator;

    [SerializeField] TextMeshProUGUI valueText;
    [SerializeField] float timeBetweenChanges = 0.1f;

    bool valueChangedCalled = false;

    // Amount
    int amount = 0;
    int displayedAmount = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Shaking", false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        displayedAmount = amount;
        SetText(displayedAmount);
    }

    public void ValueChanging()
    {
        if (valueChangedCalled)
        {
            return;
        }

        valueChangedCalled = true;
        animator.SetBool("Shaking", true);

        StartCoroutine(SetToNewValue());
    }

    void SetText(int v)
    {
        valueText.text = v.ToString();
    }

    public void ChangeValue(int change)
    {
        amount += change;

        ValueChanging();
    }

    IEnumerator SetToNewValue()
    {
        while (true)
        {
            // Count towards the latest Amount value.
            while (displayedAmount != amount)
            {
                int targetAmount = amount;
                int direction = targetAmount > displayedAmount ? 1 : -1;

                displayedAmount += direction;
                SetText(displayedAmount);

                yield return new WaitForSeconds(timeBetweenChanges);
            }


            // The value changed during the delay.
            if (displayedAmount != amount)
                continue;

            break;
        }

        // Done changing
        animator.SetBool("Shaking", false);
        valueChangedCalled = false;
    }
}
