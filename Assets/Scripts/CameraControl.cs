using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Vector2 PositionOffset = Vector2.zero;
    public float LerpSpeed = 5f;

    protected Vector2 targetPos = Vector2.zero;
    
    protected Vector2 _initialOffset = Vector2.zero;
    
    PlayerWeaponHandler _playerWeaponHandler;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerWeaponHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerWeaponHandler>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_playerWeaponHandler == null)
            return;

        
        targetPos = Vector2.Lerp(targetPos, _playerWeaponHandler.AimPosition(), Time.deltaTime * LerpSpeed);
        
        transform.position = new Vector3( targetPos.x, targetPos.y, -10f);
    }
}