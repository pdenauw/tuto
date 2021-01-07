using UnityEngine;

public class Ladder : MonoBehaviour
{
    private bool isInRange;
    private PlayerMove playerMove;
    public BoxCollider2D topCollider;

    void Awake()
    {
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isInRange && playerMove.isClimbing && Input.GetKeyDown(KeyCode.E))
        {
            playerMove.isClimbing = false;
            topCollider.isTrigger = false;
            return;
        }
        if(isInRange && Input.GetKeyDown(KeyCode.E))
        {
            playerMove.isClimbing = true;
            topCollider.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            playerMove.isClimbing = false;
            topCollider.isTrigger = false;
        }
    }
}
