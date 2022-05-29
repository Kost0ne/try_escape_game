using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Secret : MonoBehaviour
{
    // Start is called before the first frame update
    public string name;
    private int pointer = 0;
    public ParticleSystem system;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.anyKeyDown && !Input.GetMouseButtonDown(0)) print("lalalalal");
        
        if (Input.anyKeyDown && !Input.GetMouseButtonDown(0))
        {
            if (!Input.GetKeyDown(name[pointer].ToString()))
            {
                pointer = 0;
            }
            else pointer++;
        }
        

        if (pointer == name.Length)
        {
            MakeClass(out var system1, out var system2);
            pointer = 0;
        }
    }

    private void MakeClass(out ParticleSystem system1, out ParticleSystem system2)
    {
        system1 = Instantiate(system, transform.parent);
        system2 = Instantiate(system, transform.parent);
        system1.transform.position += new Vector3(6, -3, 0);
        system2.transform.position += new Vector3(-6, -3, 0);
        system2.transform.rotation = Quaternion.Euler(-150, 270, 90);
        system1.Play();
        system2.Play();
        //system2.Play();
    }
    
}