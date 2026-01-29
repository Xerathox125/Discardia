using UnityEngine;
using UnityEngine.InputSystem;

public class SceneClickVisualizer : MonoBehaviour
{
    [SerializeField] private Sprite cursorSprite;
    [SerializeField] private Color clickColor = Color.yellow;
    [SerializeField] private float clickFlashDuration = 0.15f;
    [SerializeField] private float cursorScale = 0.2f;
    [SerializeField] private bool hideSystemCursor = true;

    GameObject _cursorGO;
    SpriteRenderer _sr;
    Color _origColor;
    float _flashTimer;

    void Awake()
    {
        // Crear visual del cursor
        _cursorGO = new GameObject("RuntimeMouseCursor");
        _sr = _cursorGO.AddComponent<SpriteRenderer>();
        _sr.sortingOrder = 1000;
        if (cursorSprite != null)
            _sr.sprite = cursorSprite;
        else
        {
            // sprite sencillo blanco 1x1 si no hay asignado
            var tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, Color.white);
            tex.Apply();
            _sr.sprite = Sprite.Create(tex, new Rect(0, 0, 1, 1), Vector2.one * 0.5f, 1f);
        }

        _cursorGO.transform.localScale = Vector3.one * cursorScale;
        _origColor = _sr.color;

        if (hideSystemCursor) Cursor.visible = false;
    }

    void Update()
    {
        if (Mouse.current == null || Camera.main == null) return;

        // Posición del mouse en mundo (Z = 0)
        Vector2 screen = Mouse.current.position.ReadValue();
        Vector3 screenWithZ = new Vector3(screen.x, screen.y, -Camera.main.transform.position.z);
        Vector3 world = Camera.main.ScreenToWorldPoint(screenWithZ);
        world.z = 0f;
        _cursorGO.transform.position = world;

        // Click: flash color y log del objeto bajo el cursor
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            _sr.color = clickColor;
            _flashTimer = clickFlashDuration;

            // Raycast 2D para detectar objeto bajo el cursor
            RaycastHit2D hit = Physics2D.Raycast(world, Vector2.zero);
        }

        if (_flashTimer > 0f)
        {
            _flashTimer -= Time.deltaTime;
            if (_flashTimer <= 0f)
            {
                _sr.color = _origColor;
                _flashTimer = 0f;
            }
        }
    }

    void OnDestroy()
    {
        if (hideSystemCursor) Cursor.visible = true;
        if (_cursorGO != null) Destroy(_cursorGO);
    }
}