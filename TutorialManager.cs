using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject instructionPanelObject;
    public GameObject playerObject;
    public GameObject enemyPrefab;
    public GameObject rotateStarPrefab;
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
        waitForPassObstacle_0,
        waitForPassObstacle_1,
        waitForCollectGadget_0,
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
                        state++;
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
                if (jumpsDone > 1)
                {
                    typeWriter.StartTypingText("Pass the obstacles");
                    state++;
                }
                break;
            case State.waitForPassObstacle_0:
                playerManager.TouchMethod(doInTouchPhaseEndedWithVelocity: () =>
                {
                    GameObject enemyObject = Instantiate(enemyPrefab, new Vector2(Random.Range(-2.0f, 2.0f), playerObject.transform.position.y + 7), Quaternion.identity);
                    Destroy(enemyObject.GetComponent<Enemy>());
                    jumpsDone++;
                });
                if (jumpsDone > 2) state++;
                break;
            case State.waitForPassObstacle_1:
                playerManager.TouchMethod(doInTouchPhaseEndedWithVelocity: () =>
                {
                    Instantiate(rotateStarPrefab, new Vector2(Random.Range(-2.0f, 2.0f), playerObject.transform.position.y + 7), Quaternion.identity);
                    jumpsDone++;
                });
                if (jumpsDone > 3)
                {
                    typeWriter.StartTypingText("Collect the gadget");
                    state++;
                }
                break;
            case State.waitForCollectGadget_0:
                playerManager.TouchMethod(doInTouchPhaseEndedWithVelocity: () =>
                {
                    Instantiate(rotateStarPrefab, new Vector2(Random.Range(-2.0f, 2.0f), playerObject.transform.position.y + 7), Quaternion.identity);
                    jumpsDone++;
                });
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
