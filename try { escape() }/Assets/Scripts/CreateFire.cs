using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = System.Random;

public class CreateFire : MonoBehaviour
{
    public GameObject[] blocks;
    public GameObject fire;
    private bool waiting = true;
    private float waitingTime = 0f;
    private const float Interval = 2.5f;
    private bool activitySpawned = false;
    private List<GameObject> activeFire;
    private Random rnd;
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        print("start");
        rnd = new Random();
        animator = fire.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waiting)
        {
            waitingTime += Time.deltaTime;
            if (waitingTime >= Interval)
            {
                waitingTime = 0;
                waiting = false;
            }
        }

        else
        {
            if (!activitySpawned)
            {
                var blockNum = rnd.Next(0, blocks.Length);
                var firingBlock = blocks[blockNum];
                var firingBlockPos = firingBlock.transform.position;
                activeFire = new List<GameObject>();

                for (var dx = -4; dx <= 4; dx += rnd.Next(1, 3))
                {
                    var obj = Instantiate(fire, firingBlockPos + new Vector3(dx, -0.8f, 0),
                        firingBlock.transform.rotation);
                    activeFire.Add(obj);
                }

                activitySpawned = true;
            }

            else
            {
                if (CheckClick(activeFire))
                {
                    foreach (var fire in activeFire)
                    {
                        fire.GetComponent<Animator>().SetBool("OnDestroying", true);
                    }
                    
                    var animationTime = 0f;
                    while (animationTime < 2f)
                    {
                        animationTime += Time.deltaTime;
                    }

                    foreach (var fire in activeFire)
                    {
                        Destroy(fire);
                    }

                    waiting = true;
                    activitySpawned = false;
                }
            }
        }
    }


    private bool CheckClick(List<GameObject> fires)
    {
        if (!Input.GetMouseButtonDown(0)) return false;

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var hit = Physics2D.Raycast(mousePos, Vector2.zero);


        if (hit.collider == null) return false;
        foreach (var fire in fires)
        {
            if (hit.collider.gameObject == fire) return true;
        }

        return false;
    }
}