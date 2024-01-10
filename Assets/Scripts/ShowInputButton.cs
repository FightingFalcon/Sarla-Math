using UnityEngine;
using UnityEngine.EventSystems;

public class ShowInputButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public GameObject objectToToggle;
    public InputHandler inputHandler;
    private bool _isMobilePlatform;

    private void Awake()
    {
        _isMobilePlatform = Input.touchSupported;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isMobilePlatform)
        {
            objectToToggle.SetActive(true);
        }
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        if (_isMobilePlatform)
        {
            objectToToggle.SetActive(false);
            if (inputHandler.CurrentValue != null) inputHandler.SetAnswer();
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_isMobilePlatform)
        {
            objectToToggle.SetActive(!objectToToggle.activeSelf);
            if (!objectToToggle.activeSelf && inputHandler.CurrentValue != null) inputHandler.SetAnswer();
        }
    }
}
