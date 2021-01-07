using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour
{
    private Transform spawn;
    private Animator fadeSystem;

    private void Awake()
    {
        spawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(replace(collision));
        }
    }

    private IEnumerator replace(Collider2D collision)
    {
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        collision.transform.position = spawn.position;

    }
}
