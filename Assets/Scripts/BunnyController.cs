using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BunnyController : MonoBehaviour {

    private Rigidbody2D myRigidBody;
    private Animator myAnim;
    private Collider2D myCollider;
    private float bunnyHurtTime = -1;
    public float bunnyJumpForce = 500f;
    public Text scoreText;
    private float startTIme;

	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();

        startTIme = Time.time;
    }
	
	// Update is called once per frame
	void Update () {

        if (bunnyHurtTime == -1)
        {
            if (Input.GetButtonUp("Jump"))
            {
                myRigidBody.AddForce(transform.up * bunnyJumpForce);
            }

            myAnim.SetFloat("vVelocity", myRigidBody.velocity.y);
            scoreText.text = (Time.time - startTIme).ToString("0.0");

        }
        else
        {
            if (Time.time >bunnyHurtTime + 2)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("onCollisionEnter2D");
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            foreach (PrefabSpawner spawner in FindObjectsOfType<PrefabSpawner>())
            {
                spawner.enabled = false;
            }
            foreach (MoveLeft moveLefter in FindObjectsOfType<MoveLeft>())
            {
                moveLefter.enabled = false;
            }
            //Debug.Log("Here");
            //Application.LoadLevel(Application.loadedLevel);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            bunnyHurtTime = Time.time;
            myAnim.SetBool("bunnyHurt", true);

            myRigidBody.velocity = Vector2.zero;
            myRigidBody.AddForce(transform.up * bunnyJumpForce);
            myCollider.enabled = false; 
        }
    }
}
