using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [Header("Movement Limits")]
    [SerializeField] private float minX = -8f;
    [SerializeField] private float maxX = 8f;
    [SerializeField] private float minY = -4.5f;
    [SerializeField] private float maxY = 4.5f;

    private Camera mainCam;
    private Vector2 mouseScreenPosition;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    public void OnMousePosition(InputValue value)
    {
        mouseScreenPosition = value.Get<Vector2>(); 
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {

        Vector2 mousePos = mouseScreenPosition;
        Vector2 worldPos = mainCam.ScreenToWorldPoint(mousePos);

        worldPos.x = Mathf.Clamp(worldPos.x, minX, maxX);
        worldPos.y = Mathf.Clamp(worldPos.y, minY, maxY);

        transform.position = worldPos;
    }

}
