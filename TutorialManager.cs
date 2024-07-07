using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject instructionPanelObject;
    public GameObject playerObject;
    TypeWriting typeWriter;
    public Animation intructionAnimator;

    enum State
    {
        text_0,
        waitForText_0,
        playerFadeIn,
    }
    State state;

    public GameObject enemyObject;
    void Start()
    {
        typeWriter = GetComponent<TypeWriting>();
        typeWriter.OnTextTyped += OnTextTyped;

        StartCoroutine(TutorialStart());
        
        enemyObject.transform.localScale = new Vector2(0, 0);
    }
    void Update()
    {
        switch (state)
        {
            case State.text_0:
                
                break;
            
            case State.playerFadeIn:
                
                break;
        }
    }
    void OnTextTyped()
    {
        state++;
    }

    IEnumerator TutorialStart()
    {
        instructionPanelObject.LeanMoveLocalY(-1170, 1).setEaseOutElastic();
        yield return new WaitForSeconds(1);
        typeWriter.startTypingText("You play this ball", true);
        yield return new WaitForSeconds(2);
        playerObject.SetActive(true);
        yield return new WaitForSeconds(1);
        intructionAnimator.gameObject.SetActive(true);
        intructionAnimator.Play("FingerDrag_0");
        typeWriter.startTypingText("Tap and drag to aim", true);
    }

    public void OnCancelButtonPress()
    {
        SceneManager.LoadScene(1);
    }
    // falls Tutorial completed successfully PlayerPrefs.SetInt("TutorialDone", 1);
}
