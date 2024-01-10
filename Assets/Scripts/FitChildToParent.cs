using System.Collections;
using UnityEngine;

public class FitChildToParent : MonoBehaviour
{
    private ScreenOrientation currentOrientation;

    void Awake()
    {
        currentOrientation = Screen.orientation;
        StartCoroutine(FitChildToParentSize());
    }

    void Update()
    {
        CheckOrientationChange();
    }

    private void CheckOrientationChange()
    {
        if (currentOrientation != Screen.orientation)
        {
            currentOrientation = Screen.orientation;
            StartCoroutine(FitChildToParentSize());
        }
    }

    IEnumerator FitChildToParentSize()
    {
        yield return null; 
        Transform parentTransform = gameObject.transform.parent;
        
        // Check if the parent exists
        if (parentTransform != null)
        {
            // Get the RectTransform components of the parent and child objects
            RectTransform parentRectTransform = parentTransform.GetComponent<RectTransform>();
            RectTransform childRectTransform = gameObject.GetComponent<RectTransform>();

            if (parentRectTransform != null && childRectTransform != null)
            {
                // Get the heights of the parent and child objects
                float parentHeight = parentRectTransform.rect.height;
                float childHeight = childRectTransform.rect.height;

                // Calculate the scale factor needed to fit the child object inside the parent object
                float scaleFactor = parentHeight / childHeight;

                // Apply the scale factor to the child object
                gameObject.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);

                // Position the child object at the center of the parent object
                gameObject.transform.localPosition = Vector3.zero;
            }
            else
            {
                Debug.LogWarning("Both parent and child objects must have a RectTransform component.");
            }
        }
        else
        {
            Debug.LogWarning("The child object does not have a parent.");
        }
    }
}