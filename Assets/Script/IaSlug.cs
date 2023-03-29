using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IaSlug : MonoBehaviour
{

    public Transform enemie;
    public Transform[] position;
    public float speed;
    public bool isRight;

    private int idTarget;
    private SpriteRenderer enmSprite;
    // Start is called before the first frame update
    void Start()
    {
        enmSprite= enemie.gameObject.GetComponent<SpriteRenderer>();
        enemie.position = position[0].position;
        idTarget = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemie != null)
        {
            Move();
        }
    }

    void Move()
    {
        enemie.position = Vector3.MoveTowards(enemie.position, position[idTarget].position, speed * Time.deltaTime);
        if (enemie.position == position[idTarget].position)
        {
            idTarget += 1;

            if (idTarget == position.Length)
            {
                idTarget = 0;
            }
            Flip();
        }
    }


    void Flip()
    {
        isRight = !isRight;
        enmSprite.flipX = !isRight;
    }
}
