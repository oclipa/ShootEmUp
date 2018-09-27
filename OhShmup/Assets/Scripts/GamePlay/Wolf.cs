using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : GameOverInvoker
{
    // the RigidBody2D of the wolf
    Rigidbody2D rb;

    // half the width of the wolf
    float wolfHalfWidth;

    // initial position
    Vector2 startingPosition;

    // bark timer
    Timer barkTimer;

    // stop moving
    bool stopMoving;

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
        if (barkTimer.Finished)
        {
            GameObject projectile = Spawner.SpawnProjectile(ProjectileType.BARK, transform.position);
            if (projectile != null)
            {
                projectile.SetActive(true);
                projectile.GetComponent<Projectile>().StartMoving();
            }
            barkTimer.Run();
        }
    }

    private void FixedUpdate()
    {
        if (!stopMoving)
        {
            Vector2 position = rb.position;
            float x = position.x;
            x += -1 * GameConstants.WolfMoveUnitsPerSecond * Time.deltaTime;
            position.x = x;

            rb.MovePosition(position);
        }
    }

    public void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();

        wolfHalfWidth = GetComponent<BoxCollider2D>().size.x / 2;

        EventManager.AddGameOverInvoker(this);
        AddEvent(EventName.GameOverEvent);
        EventManager.AddGameOverListener(GameOver);

        barkTimer = gameObject.AddComponent<Timer>();
        barkTimer.Duration = GameConstants.WolfBarkDuration;
        barkTimer.Run();

        stopMoving = false;
    }

    public void StartMoving()
    {
        stopMoving = false;
    }

    public void StopMoving()
    {
        stopMoving = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pellet"))
        {
            Spawner.ReturnWolf(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        if (transform.position.x < ScreenUtils.ScreenLeft)
        {
            Spawner.ReturnWolf(gameObject);
            Invoke(EventName.GameOverEvent);
        }
    }

    private void GameOver()
    {
        Spawner.ReturnWolf(gameObject);
    }
}
