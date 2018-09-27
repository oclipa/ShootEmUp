using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour {


    // the RigidBody2D of the wolf
    protected Rigidbody2D rb;

    // half the width of the projectile
    protected float projectileHalfWidth;


    // stop moving
    protected bool stopMoving;

    // Use this for initialization
    protected virtual void Start () {
    }

    // Update is called once per frame
    protected virtual void Update () {

	}

    private void FixedUpdate()
    {
        move();
    }

    public void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        projectileHalfWidth = GetComponent<BoxCollider2D>().size.x / 2;

        EventManager.AddGameOverListener(GameOver);

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

    private void OnBecameInvisible()
    {
        Spawner.ReturnProjectile(gameObject);
    }

    private void GameOver()
    {
        Spawner.ReturnProjectile(gameObject);
    }

    protected abstract void move();

    public abstract ProjectileType Type();
}
