using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject instructionPanelObject;
    public GameObject playerObject;
    public GameObject enemyPrefab;
    public GameObject flyPrefab;
    public GameObject rotateStarPrefab;
    public GameObject boostPrefab;
    public GameObject zoneObject;
    PlayerManagerTutorial playerManager;
    TypeWriting typeWriter;
    public Animation intructionAnimation;
    int jumpsDone = 0;

    public enum State
    {
        start, // intro IEnumerator
        waitForAim,
        waitForTouchEnd_0,
        waitForMoreJumps,
        waitForPassEnemy,
        waitForPassFly,
        waitForCollectBoost,
        waitForBoostEnd,
        doInBoostEnd,
        waitForZoneText,
        startScoreText,
        waitForScoreText,
        speedUpZone,
        doOnZoneFullScreen,
        waitForChanceText,
        startEndText,
        waitForEndText,
        end,
        playerDeath
    }
    static public State state = State.start;

    void Start()
    {
        playerManager = playerObject.GetComponent<PlayerManagerTutorial>();
        typeWriter = GetComponent<TypeWriting>();

        BoostScript.OnBoostCollect += IncreaseState;
        ZoneTutorial.OnZoneFullScreen += IncreaseState;
        playerManager.OnPlayerDeath += HandleOnPlayerDeath;

        StartCoroutine(TutorialStart());
    }
    void Update()
    {
        switch (state)
        {
            case State.waitForAim:
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
                    typeWriter.StartTypingText("Repeat");
                    intructionAnimation.gameObject.SetActive(false);
                    zoneObject.SetActive(true);
                    zoneObject.GetComponent<ZoneTutorial>().speed = 0;
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
            case State.waitForPassEnemy:
                playerManager.TouchMethod(doInTouchPhaseEndedWithVelocity: () =>
                {
                    InstantiateEnemy(new Vector2(Random.Range(-2.0f, 2.0f), playerObject.transform.position.y + 7));
                    jumpsDone++;
                });
                if (jumpsDone > 2)
                {
                    Instantiate(flyPrefab, new Vector2(Random.Range(-1.2f, 1.2f), playerObject.transform.position.y + 7), Quaternion.identity);
                    this.Invoke(() => { Instantiate(rotateStarPrefab, new Vector2(Random.Range(-2f, 2f), playerObject.transform.position.y + 7), Quaternion.identity); }, 3f);                                 
                    state++;
                }
                break;
            case State.waitForPassFly:
                playerManager.TouchMethod();
                if (GameObject.Find("Fly(Clone)") == null)
                {
                    typeWriter.StartTypingText("Collect the boost");
                    Instantiate(boostPrefab, new Vector2(0, playerObject.transform.position.y + 9), Quaternion.identity);
                    InstantiateEnemy(new Vector2(1.5f, playerObject.transform.position.y + 20));
                    state++;
                }
                break;
            case State.waitForCollectBoost:
                playerManager.TouchMethod();
                break;
            case State.waitForBoostEnd:
                playerManager.BoostMethod();
                break;
            case State.doInBoostEnd:
                zoneObject.GetComponent<ZoneTutorial>().speed = 2.5f;
                state++;
                break;
            case State.waitForZoneText:
                playerManager.TouchMethod(doInTouchPhaseEndedWithVelocity: () => { state++; });
                break;
            case State.startScoreText:
                typeWriter.OnTextTyped += IncreaseState;
                typeWriter.StartTypingText("The higher you get the higher is your score");
                state++;
                break;
            case State.waitForScoreText:
                playerManager.TouchMethod();
                break;
            case State.speedUpZone:
                zoneObject.GetComponent<ZoneTutorial>().speed += 8 * Time.deltaTime;
                playerManager.TouchMethod();
                break;
            case State.doOnZoneFullScreen:
                typeWriter.StartTypingText("You always have a second chance", delay: 1);
                state++;
                break;
            case State.waitForChanceText:
                break;
            case State.startEndText:
                typeWriter.OnTextTyped -= IncreaseState;
                typeWriter.StartTypingText("Good luck!\n(Tap)");
                state++;
                break;
            case State.waitForEndText:
                playerManager.TouchMethod(doInTouchPhaseBegan: () => {
                    typeWriter.textObject.transform.LeanMoveLocalX(-1200, 0.7f).setEaseInBack().setOnComplete(() => {
                        PlayerPrefs.SetInt("TutorialDone", 1);
                        SceneManager.LoadScene(0);
                    });
                    state++; });
                break;
            case State.end:
                break;

            case State.playerDeath:
                break;
            
        }
    }
    void IncreaseState()
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
        state = State.waitForAim;
    }

    public void OnCancelButtonPress()
    {
        SceneManager.LoadScene(1);
    }
    void InstantiateEnemy(Vector2 position)
    {
        GameObject enemyObject = Instantiate(enemyPrefab, position, Quaternion.identity);
        Destroy(enemyObject.GetComponent<Enemy>());
    }
    void HandleOnPlayerDeath()
    {
        typeWriter.OnTextTyped -= IncreaseState;
        ZoneTutorial.OnZoneFullScreen -= IncreaseState;
        typeWriter.StartTypingText("Do not collide");
        Debug.Log("Player die.");
    }
}
