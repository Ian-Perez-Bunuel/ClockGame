using UnityEngine;

public class HandManager : MonoBehaviour
{
    [SerializeField] Hand minuteHand;
    [SerializeField] Hand hourHand;

    // Update is called once per frame
    void Update()
    {
        // Left click
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            minuteHand.Activate();
        }
        // Right Click
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetMouseButtonDown(1))
        {
            hourHand.Activate();
        }
    }
}
