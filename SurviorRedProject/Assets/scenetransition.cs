using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // for handling scnes

public class scenetransition : MonoBehaviour
{
    private Animator transanim;
    // Start is called before the first frame update
    void Start()
    {
        transanim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void loadscreen(string scenename)
    {
        StartCoroutine(Transition(scenename));
    }


    IEnumerator Transition(string scenename)
    {
        transanim.SetTrigger("end");
        yield return new WaitForSeconds(1); //wait for scene to transition
        SceneManager.LoadScene(scenename);
    }

}
