using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    #region Animation
    bool animationTrigger;
    public Sprite player_0;
    public Sprite player_1;
    public Sprite player_2;
    public Sprite player_3;
    public Sprite player_4;
    public Sprite player_5;
    public Sprite player_6;
    #endregion
    InputManager _InputManager;
    SpriteRenderer _PlayerSpriteRenderer;
    Rigidbody2D _PlayerRigidBody;
    private void Start()
    {
        _PlayerSpriteRenderer = GetComponent<SpriteRenderer>();
        _InputManager = GetComponent<InputManager>();
        _PlayerRigidBody = GetComponent<Rigidbody2D>();
        animationTrigger = true;
    }
    IEnumerator jumpBegin()
    {
        _PlayerSpriteRenderer.sprite = player_0;
        yield return new WaitForSeconds(0.1f);
        _PlayerSpriteRenderer.sprite = player_1;
        yield return new WaitForSeconds(0.1f);
        _PlayerSpriteRenderer.sprite = player_2;
        yield return new WaitForSeconds(0.1f);
        _PlayerSpriteRenderer.sprite = player_3;
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator jumpAir()
    {
        _PlayerSpriteRenderer.sprite = player_4;
        yield return new WaitForSeconds(0.05f);
        _PlayerSpriteRenderer.sprite = player_5;
    }

    private void Update()
    {
        touchStatus inputStatus = _InputManager.currentTouchStatus;
        Vector2 PlayerVelocity = _PlayerRigidBody.velocity;

        if (PlayerVelocity.y == 0 && animationTrigger)
        {
            _PlayerSpriteRenderer.sprite = player_0;
            PlayerVelocity = Vector2.zero;//BU MOVEMENT MANAGERDA OLMALI>!
        }

        if (PlayerVelocity.y == 0 && animationTrigger)
        {
            _PlayerSpriteRenderer.sprite = player_0;
        }
        if (PlayerVelocity.y < 0)
        {
            StopCoroutine(jumpAir());
            _PlayerSpriteRenderer.sprite = player_6;
            animationTrigger = true;
        }

        if (inputStatus == touchStatus.touchOnScreen && animationTrigger)
        if (inputStatus == touchStatus.touchOnScreen&&animationTrigger)
        {
            animationTrigger = false;
            StartCoroutine(jumpBegin());
        }
        else if (inputStatus == touchStatus.touchReleased)
        {
            StopCoroutine(jumpBegin());
            StartCoroutine(jumpAir());
        }

    }
}
