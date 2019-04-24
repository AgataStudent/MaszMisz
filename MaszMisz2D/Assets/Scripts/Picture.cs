using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Picture : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        GameManager.FolderName = gameObject.name;
        if(gameObject.name == "Animal")
            SceneManager.LoadScene("GameScene");
        if(gameObject.name == "Forest")
            SceneManager.LoadScene("GameScene");

        if (gameObject.name == "Nature")
            SceneManager.LoadScene("HardScene");
        if(gameObject.name == "Wather")
            SceneManager.LoadScene("HardScene");

    }

}
