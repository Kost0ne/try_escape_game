using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float Speed = 2;
    public GameObject mousePivot;
    private Rigidbody2D componentRigidbody;

    private void Start()
    {
        componentRigidbody = mousePivot.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetComponent<Animator>().SetBool("IsMoving", true);
        componentRigidbody.velocity = Vector2.zero;
        var localRotation = mousePivot.transform.localRotation.eulerAngles;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            componentRigidbody.velocity += Vector2.left * Speed;
            mousePivot.transform.localRotation = Quaternion.Euler(localRotation.x, localRotation.y, 90);
            
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            componentRigidbody.velocity += Vector2.right * Speed;
            mousePivot.transform.localRotation = Quaternion.Euler(localRotation.x, localRotation.y, -90);
        }

        else if (Input.GetKey(KeyCode.UpArrow))
        {
            componentRigidbody.velocity += Vector2.up * Speed;
            mousePivot.transform.localRotation = Quaternion.Euler(localRotation.x, localRotation.y, 0);
        }

        else if (Input.GetKey(KeyCode.DownArrow))
        {
            componentRigidbody.velocity += Vector2.down * Speed;
            mousePivot.transform.localRotation = Quaternion.Euler(localRotation.x, localRotation.y, 180);
        }

        else
        {
            GetComponent<Animator>().SetBool("IsMoving", false);
        }
    }
}