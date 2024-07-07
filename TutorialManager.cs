using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject instructionPanelObject;
    public GameObject playerObject;
    PlayerManagerTutorial playerManager;
    TypeWriting typeWriter;
    public Animation intructionAnimator;

    public enum State
    {
        start,
        text_0,
        touch_0,
        text_1,
        touch_1
    }
    static public State state = State.start;

    void Start()
    {
        playerManager = playerObject.GetComponent<PlayerManagerTutorial>();
        typeWriter = GetComponent<TypeWriting>();
        typeWriter.OnTextTyped += OnTextTyped;

        StartCoroutine(TutorialStart());
    }
    void Update()
    {
        switch (state)
        {
            case State.touch_0:
                
                break;
            
            case State.touch_1:
                Debug.Log("touch_1");
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
        typeWriter.startTypingText("You play this ball", mute: true, invokeEvent: false);
        yield return new WaitForSeconds(2);
        playerObject.SetActive(true);
        yield return new WaitForSeconds(1);
        intructionAnimator.gameObject.SetActive(true);
        intructionAnimator.Play("FingerDrag_0");
        typeWriter.startTypingText("Tap and drag to aim", mute: true, invokeEvent: false);
        state = State.text_0;
    }

    public void OnCancelButtonPress()
    {
        SceneManager.LoadScene(1);
    }
    // falls Tutorial completed successfully PlayerPrefs.SetInt("TutorialDone", 1);
}
