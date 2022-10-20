using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public Vector3 startPosition;
    public Vector3 endPosition;
    public int damage;

    private SpriteRenderer sprite;
    private bool movingToEnd;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.transform.Find("crab-idle-1").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
    }

    void EnemyMove()
    {
        //calculate the destination in order to movingToEnd
        Vector3 targetPosition = (movingToEnd) ? startPosition : endPosition;

        //Enemy movement
        transform.position = Vector3.MoveTowards
                (transform.position, targetPosition, speed * Time.deltaTime);
        //when arrive to end
        if (transform.position == targetPosition)
            movingToEnd = !movingToEnd;

        if (!movingToEnd) sprite.flipX = false;
        else if (movingToEnd) sprite.flipX = true;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
}
