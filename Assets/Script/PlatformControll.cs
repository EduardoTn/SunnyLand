using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControll : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform plataforma, ptA, ptB;
    public float speed = 0.02f;
    public Vector3 pontoDestino;
    public GameObject player;
    void Start()
    {
        plataforma.position = ptA.position;
        pontoDestino= ptB.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (plataforma.position == ptA.position)
        {
            pontoDestino= ptB.position;
        } else if(plataforma.position == ptB.position)
        {
            pontoDestino = ptA.position;
        }
        plataforma.position = Vector3.MoveTowards(plataforma.position, pontoDestino, speed);
    }
}
