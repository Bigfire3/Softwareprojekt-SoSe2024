using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject instructionPanelObject;
    public GameObject playerObject;
    PlayerManagerTutorial playerManager;
    TypeWriting typeWriter;
    public Animation intructionAnimation;
    int jumpsDone = 0;

    public enum State
    {
        start, // intro IEnumerator
        waitForAim_0,
        waitForTouchEnd_0,
        waitForMoreJumps,
        waitForPassObstacles,
        text_1,
        touch_1
    }
    static public State state = State.start;

    void Start()
    {
        playerManager = playerObject.GetComponent<PlayerManagerTutorial>();
        typeWriter = GetComponent<TypeWriting>();
        //typeWriter.OnTextTyped += OnTextTyped;

        StartCoroutine(TutorialStart());
    }
    void Update()
    {
        switch (state)
        {
            case State.waitForAim_0:
                playerManager.TouchMethod(
                    doInTouchPhaseAim: () =>
                    {
                        typeWriter.StartTypingText("Perfectly\nNow let go to jump");
                        state = State.waitForTouchEnd_0;
                    });
                break;
            
            case State.waitForTouchEnd_0:
                playerManager.TouchMethod(doInTouchPhaseEndedWithVelocity: () =>
                {
                    typeWriter.StartTypingText("Do more Jumps");
                    intructionAnimation.gameObject.SetActive(false);
                    state++;
                });
                break;
            case State.waitForMoreJumps:
                playerManager.TouchMethod(doInTouchPhaseEndedWithVelocity: () =>
                {
                    jumpsDone++;
                });
                if (jumpsDone > 2)
                {
                    typeWriter.StartTypingText("Don't collide with the obstacles");
                    state++;
                }
                break;
            case State.waitForPassObstacles:
                Debug.Log("Spawn enemies");
                playerManager.TouchMethod();
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
        typeWriter.StartTypingText("You play this ball", mute: true);
        yield return new WaitForSeconds(2);
        playerObject.SetActive(true);
        yield return new WaitForSeconds(1);
        intructionAnimation.gameObject.SetActive(true);
        intructionAnimation.Play("FingerDrag_0");
        typeWriter.StartTypingText("Tap and drag to aim", mute: true);
        state = State.waitForAim_0;
    }

    public void OnCancelButtonPress()
    {
        SceneManager.LoadScene(1);
    }
    // falls Tutorial completed successfully PlayerPrefs.SetInt("TutorialDone", 1);
}
