using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{

    [SerializeField] private GameObject nextlvl;

    //private Animator anim;

    private bool levelCompleted = false;







    void Start()
    {

        //anim = GetComponent<Animator>();
        nextlvl.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !levelCompleted)
        {
            levelCompleted = true;
            //Touched();
            //AudioManager.Instance.musicSource.Stop();
            //AudioManager.Instance.PlaySFX("Finish");
            Invoke("nextlvlcanvas", 1f);

        }
    }

    private void nextlvlcanvas()
    {
        levelCompleted = false;
        nextlvl.SetActive(true);
    }

    public void nextlevel()
    {

        UnlockNewLevel();
        Invoke("CompleteLevel", 1f);
    }

    //private void Touched()
    //{
    //    anim.SetTrigger("touched");
    //}

    private void CompleteLevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }






}
