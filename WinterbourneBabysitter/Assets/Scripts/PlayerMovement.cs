using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    // Player Speed
    public float speed;
    public Image UIKarlImage;
    public Sprite[] spriteArray;

    private Rigidbody2D playerRB;
    private NavMeshAgent playerNMA;
    private Vector2 velocity;
    private Vector2 movementInput;
    private Vector3 target;
    private bool usingMouse;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerNMA = GetComponent<NavMeshAgent>();
        target = transform.position;
        usingMouse = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            usingMouse = true;
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
        }
        else if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0)
        {
            // Get player's Key-press movement input
            usingMouse = false;
            movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            velocity = movementInput.normalized * speed;
            playerRB.MovePosition(playerRB.position + velocity * Time.fixedDeltaTime);
        }

        if (usingMouse) { transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime); }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        velocity = velocity * 0;
        target = transform.position;

        if (collision.gameObject.tag == "Egg") {
            UIKarlImage.sprite = spriteArray[0];
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        UIKarlImage.sprite = spriteArray[1];
    }
}
