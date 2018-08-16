using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Player player;
    public GameObject[] waves;
    
    public float scrollSpeed = 0.3f;

    Vector2 targetScrollPosition;
    float nextSpawnHeight;

    public Text scoreUI;
    public Text chainUI;
    public GameObject gameOver;

    int score;
    int chain;

    void Update()
    {
        if (!player)
        {
            gameOver.SetActive(true);
            Invoke("ReturnTitle", 5f);

            return;
        }

        targetScrollPosition.y += scrollSpeed * Time.deltaTime;

        if (player.transform.position.y > targetScrollPosition.y + 6f)
        {
            targetScrollPosition.y += 4f;
        }

        transform.position = Vector2.Lerp(transform.position, targetScrollPosition, Time.deltaTime);

        while (transform.position.y + 16f > nextSpawnHeight)
        {
            GenerateWaves();
        }
    }

    void GenerateWaves()
    {
        Instantiate(
            waves[Random.Range(0, waves.Length)],
            new Vector3(Random.Range(-1, 2), nextSpawnHeight),
            Quaternion.identity
        );

        nextSpawnHeight += 4f;
        scrollSpeed += 0.01f;
    }

    public void AddScore(bool isChaining)
    {
        chain = isChaining ? chain + 1 : 0;

        score += Mathf.Min(chain + 1, 16) * 10;

        scoreUI.text = "Score " + score.ToString("0,0");
        chainUI.text = chain.ToString() + " chain";
    }

    void ReturnTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
