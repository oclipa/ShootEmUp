using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Sheep : GameOverInvoker {

    // the RigidBody2D of the sheep
    Rigidbody2D rb;

    // half dimensions of the sheep
    float sheepHalfWidth;
    float sheepHalfHeight;

    // fear level
    int fear;
    int maxFear = 4;
    LineRenderer lineRenderer;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        sheepHalfWidth = GetComponent<BoxCollider2D>().size.x / 2;
        sheepHalfHeight = GetComponent<BoxCollider2D>().size.y / 2;

        //GameObject.FindGameObjectWithTag("FearBarBackground").SetActive(false);
        lineRenderer = GameObject.FindGameObjectWithTag("FearBar").GetComponent<LineRenderer>();
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;

        EventManager.AddGameOverInvoker(this);
        AddEvent(EventName.GameOverEvent);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.Play(AudioClipName.Baa);
            GameObject projectile = Spawner.SpawnProjectile(ProjectileType.PELLET, transform.position);
            if (projectile != null)
            {
                projectile.SetActive(true);
                projectile.GetComponent<Projectile>().StartMoving();
            }
        }
    }

    private void FixedUpdate()
    {
        float verticalInput = Input.GetAxis("Vertical");
        if (verticalInput != 0f)
        {
            Vector2 position = rb.position;
            float y = position.y;
            y += verticalInput * GameConstants.SheepMoveUnitsPerSecond * Time.deltaTime;
            position.y = CalculateClampedY(y);

            rb.MovePosition(position);
        }
    }

    private float CalculateClampedY(float y)
    {
        if (y < ScreenUtils.ScreenBottom + sheepHalfHeight)
        {
            y = ScreenUtils.ScreenBottom + sheepHalfHeight;
        }
        else if (y > ScreenUtils.ScreenTop - sheepHalfHeight * 2)
        {
            y = ScreenUtils.ScreenTop - sheepHalfHeight * 2;
        }

        return y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bark"))
        {
            AudioManager.Play(AudioClipName.Woof);

            fear++;

            Vector3[] linePositions = new Vector3[2];
            lineRenderer.GetPositions(linePositions);

            float diff = (sheepHalfWidth * ((float)fear / (float)maxFear));
            float result = linePositions[1].x + diff;
            linePositions[1].x = result;
            lineRenderer.SetPositions(linePositions);

            if (fear > maxFear)
            {
                Invoke(EventName.GameOverEvent);
                Destroy(gameObject);
            }
        }
    }
}
