using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;

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

        Vector3 mousePos = mouseScreenPosition;

        mousePos.z = -mainCam.transform.position.z;

        Vector3 worldPos = mainCam.ScreenToWorldPoint(mousePos);

        worldPos.x = Mathf.Clamp(worldPos.x, minX, maxX);
        worldPos.y = Mathf.Clamp(worldPos.y, minY, maxY);
        worldPos.z = 0f;

        transform.position = worldPos;
    }

}
