using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZakuskaScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool isMazeQuestCompleted;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.collider.tag);
        
        if (collision.collider.tag == "mouse")
        {
            var mouse = collision.gameObject.transform.GetChild(0);
            mouse.GetComponent<SpriteRenderer>().color = Color.black;
            isMazeQuestCompleted = false;
            var particularSystem = transform.GetChild(0).GetComponent<ParticleSystem>();
            var emission = particularSystem.emission;
            emission.rate = new ParticleSystem.MinMaxCurve(500);
            Destroy(mouse.parent.gameObject);
        }
    }
}
