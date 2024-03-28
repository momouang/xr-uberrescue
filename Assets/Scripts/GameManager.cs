using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public FaderScreen faderScreen;

    public GameObject Menu;
    public Timer timer;

    public Rigidbody playerTransform;
    public GameObject boatTransform;
    public Transform gameOverSpawnPoint;
    public Transform boatSpawnPoint;

    private void OnEnable()
    {
        Timer.OnTimeUp += GameOver;
    }

    private void OnDisable()
    {
        Timer.OnTimeUp -= GameOver;
    }


    private void Start()
    {
        Menu.SetActive(true);
    }

    public void OnPressStartButton()
    {
        timer.isCounting = true;
        Menu.SetActive(false);
    }

    public void Retry()
    {
        StartCoroutine(GoToSceneRoutine()); 
        SceneManager.LoadScene(0);

    }

    void GameOver()
    {
        StartCoroutine(GoToSceneRoutine());
        playerTransform.transform.position = gameOverSpawnPoint.position;
        boatTransform.transform.position = boatSpawnPoint.position;
    }

    IEnumerator GoToSceneRoutine()
    {
        faderScreen.FadeOut();
        yield return new WaitForSeconds(faderScreen.fadeDuration);

        faderScreen.FadeIn();
    }

}
