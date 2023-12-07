using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField]private Animator anim;

    private Vector3 respawnPoint;
    private void Start()
    {
        anim= GetComponent<Animator>();
        respawnPoint= transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Death")
        {
            StartCoroutine(Respawn());
        }
        else if (other.gameObject.tag == "CheckPoint")
        {
            respawnPoint = transform.position;
        }
        else if (other.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene(0);
        }
       

    }

    IEnumerator Respawn()
    {
        FindObjectOfType<AudioManager>().Play("Death");
        anim.CrossFade("Death", 0.2f);

        yield return new WaitForSeconds(2);

        transform.position = respawnPoint;
        anim.CrossFade("IDLE", 0.02f);
    }

}
