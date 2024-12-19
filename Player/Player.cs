using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    private Rigidbody2D _rigid;
    [SerializeField]
    private float _jumpForce = 5.0f;
    private bool _resetJump = false;
    [SerializeField]
    private float _speed = 5.0f;
    private bool _grounded = false; 
    private PlayerAnimation _playerAnim;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite; 

    public int Health { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (Input.GetMouseButtonDown(0) && IsGrounded() == true)
        {
            _playerAnim.Attack(); 
        }
    }

    void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");
        _grounded = IsGrounded();


        /* Vector2 rayOrigin = transform.position;
        Vector2 rayDirection = Vector2.down * 0.6f;
        Vector2 rayEnd = rayOrigin + rayDirection;
        */

        if (move > 0)
        {
            Flip(true);
        }
        else if (move < 0)
        {
            Flip(false);
        }
        //Debug.DrawLine(rayOrigin, rayEnd, Color.red); // Change the color as needed
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true)
        {
            Debug.Log("Jump!");
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutine());
            _playerAnim.Jump(true);
        }

        _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);

        _playerAnim.Move(move); 
    }

    bool IsGrounded()
    {       
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1f, 1 << 8);
        Debug.DrawRay(transform.position, Vector2.down, Color.green); 

        if (hitInfo.collider != null)
        {
            if (_resetJump == false)
            {
                Debug.Log("Grounded"); 
                _playerAnim.Jump(false); 
                return true;
            }

        }
        return false; 
    }

    void Flip(bool faceRight)
    {
        if (faceRight == true)
        {
            _playerSprite.flipX = false;
            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newPos; 
        }
        else if (faceRight == false)
        {
            _playerSprite.flipX = true;
            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
    }

    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false; 
    }

    public void Damage()
    {
        Debug.Log("Player::Damage");
    }
}
