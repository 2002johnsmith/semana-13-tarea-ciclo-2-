using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    public float speedX;
    public float horizontal;
    private Rigidbody2D _componentRigibody2D;
    private Animator _componentAnimator;
    private SpriteRenderer _componentSpriteRenderer;
    public bool canJump;
    public bool isJump;
    public float jumpForce;
    public AudioSource sfxAudio;
    public GameObject prefabBullet;
    // Start is called before the first frame update
    void Awake()
    {
        _componentRigibody2D = GetComponent<Rigidbody2D>();
        _componentAnimator = GetComponent<Animator>();
        _componentSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        _componentAnimator.SetInteger("walking",(int)horizontal);
        if (horizontal > 0)
        {
            _componentSpriteRenderer.flipX = false;
        }
        else if (horizontal < 0)
        {
            _componentSpriteRenderer.flipX = true;
        }
        if (Input.GetButtonDown("Jump") && canJump == true)
        {
            isJump = true;
            sfxAudio.Play();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(prefabBullet,transform.position,transform.rotation);
        }
    }
    public void DestroyPlayer()
    {
        Destroy(this.gameObject);
    }
    void FixedUpdate()
    {
        _componentRigibody2D.velocity = new Vector2(horizontal * speedX, _componentRigibody2D.velocity.y);
        if (isJump == true)
        {
            _componentRigibody2D.AddForce(new Vector2(0, jumpForce),ForceMode2D.Impulse);
        }
        if (_componentRigibody2D.velocity.y<0&&canJump==false)
        {
            _componentAnimator.SetBool("falling", true);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            canJump = true;
        }
        _componentAnimator.SetBool("falling", false);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            canJump = false;
            isJump = false;
            _componentAnimator.SetBool("falling", true);
        }
    }
}
