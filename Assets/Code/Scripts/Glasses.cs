using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Glasses : MonoBehaviour
{
     
     Animator anim;

    [SerializeField]private GameObject Lentes_Mesh;
    [SerializeField] private Component[] Plataformas;
    [SerializeField] private ThirdPersonMovement character;

    [SerializeField] private ParticleSystem Puf_Inicio;
    [SerializeField] private ParticleSystem Puf_Fin;
    public bool LentesOn;

    // Start is called before the first frame update
    void Start()
    {

        anim = character.GetComponent<Animator>();
        Lentes_Mesh.SetActive(false);
        Puf_Fin.Stop();

        Plataformas = gameObject.GetComponentsInChildren<Renderer>();


        foreach (var r in Plataformas)
        {
            r.GetComponentInChildren<Renderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.E) && character.isGrounded && LentesOn == false)
        {
            
            
            StartCoroutine(Lentes());
            
            
        }
    }

    IEnumerator Lentes()
    {
        Puf_Inicio.Play();
        FindObjectOfType<AudioManager>().Play("LentesOn");
        anim.SetBool("Lentes", true);
        Lentes_Mesh.SetActive(true);
        LentesOn = true;
        foreach (var r in Plataformas) 
        {
            r.GetComponentInChildren<Renderer>().enabled = true;
        }

        yield return new WaitForSeconds(3);

        Puf_Fin.Play();
        FindObjectOfType<AudioManager>().Play("LentesOff");
        anim.SetBool("Lentes", false);
        Lentes_Mesh.SetActive(false);

        foreach (var r in Plataformas)
        {
            r.GetComponentInChildren<Renderer>().enabled = false;
        }
        
        LentesOn = false;
        

    }
}
