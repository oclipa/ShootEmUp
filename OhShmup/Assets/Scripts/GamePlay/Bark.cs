using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bark : Projectile {

    int direction = -1;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void move()
    {
        if (!stopMoving)
        {
            Vector2 position = rb.position;
            float x = position.x;
            x += direction * GameConstants.BulletMoveUnitsPerSecond * Time.deltaTime;
            position.x = x;

            rb.MovePosition(position);
        }
    }

    public override ProjectileType Type()
    {
        return ProjectileType.BARK;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sheep"))
        {
            Spawner.ReturnProjectile(gameObject);
        }
    }
}
