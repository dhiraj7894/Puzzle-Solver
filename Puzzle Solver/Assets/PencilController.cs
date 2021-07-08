using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PencilController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject confetti;
    [SerializeField] CinemachineVirtualCamera finishCam;
    [SerializeField] Animator levelComplete;
    [SerializeField] GameObject objectiveText;
    [SerializeField] GameObject continueButton;
    
    // Start is called before the first frame update
    void Start()
    {
        animator.SetTrigger("Draw");
    }

    // Update is called once per frame
    void Update()
    {
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Finish"))
        {
            // Avoid any reload.
            finishCam.Priority = 100;
            StartCoroutine(LevelComplete());
        }
    }

    private IEnumerator LevelComplete()
    {
        objectiveText.SetActive(false);
        yield return new WaitForSeconds(1f);

        continueButton.SetActive(true);
        levelComplete.gameObject.SetActive(true);
        levelComplete.SetTrigger("PopUp");
        confetti.SetActive(true);
    }
}
