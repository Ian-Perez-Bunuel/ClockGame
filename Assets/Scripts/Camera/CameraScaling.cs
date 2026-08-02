using UnityEngine;

public class CameraScaling : MonoBehaviour
{
    [SerializeField] Camera targetCamera;
    [SerializeField] Transform minVisibiltyPoint;
    [SerializeField] Transform maxVisibiltyPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FitScreen();
    }

    void FitScreen()
    {
        Vector2 minPoint = minVisibiltyPoint.position;
        Vector2 maxPoint = maxVisibiltyPoint.position;
        float width = Mathf.Abs(maxPoint.x - minPoint.x);
        float height = Mathf.Abs(maxPoint.y - minPoint.y);
        Vector2 centre = (minPoint + maxPoint) * 0.5f;

        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = width / height;

        // Center camera
        targetCamera.transform.position = new Vector3(centre.x, centre.y, targetCamera.transform.position.z);

        // Decide if it needs the vertical size or horizontal size
        if (screenRatio >= targetRatio)
            Camera.main.orthographicSize = height / 2f;
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = height / 2f * differenceInSize;
        }
    }
}
