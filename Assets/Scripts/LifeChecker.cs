using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeChecker : MonoBehaviour
{
    private Rigidbody _body;
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead && transform.position.y < -1)
        {
            isDead = true;
            Die();
        }
    }

    private void Die()
    {
        Invoke(nameof(this.ReloadLevel), 1.0f);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
