using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: GreenCapsule and Player movement will be improve
//TODO: Win-Lose state will be create   
//TODO: Play, HealthBar, next level and retry button will be add project

public class InGame : MonoBehaviour
{
    [SerializeField]
    private GameObject player ;

    [SerializeField]
    private GameObject[] greenCapsule;

    [SerializeField]
    private GameObject[] redCapsule;

    private int score = 0;
    private int health = 3;
    public int clickCount = 0;

    public Vector3 playerFirstPosition { get; set; }
    public CapsuleCollider capsuleCollider { get; set; }

    void Start()
    {
        playerFirstPosition = gameObject.transform.position;
    }

    void Update()
    {
        OnMouseDown();
        SecondMouseButton();               
    }

    void CapsuleCollider()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            CapsuleCollider capsuleCollider = hit.collider as CapsuleCollider;
            this.capsuleCollider = capsuleCollider;
        }
    }

    void OnMouseDown()
    {

        if (Input.GetMouseButtonDown(0))
        {           
            if (clickCount == 0)
            {

                CapsuleCollider();

                if (capsuleCollider != null)
                {
                    StartCoroutine(Move(player, playerFirstPosition, capsuleCollider.gameObject.transform.position));
                }
            }
        }
    }

    void SecondMouseButton()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (capsuleCollider.gameObject.CompareTag("Red"))
            {
                player.SetActive(false);
                health -= 1;
                Debug.Log("Health: " + health);

                if (health != 0)
                {
                    player.transform.position = playerFirstPosition;
                    player.SetActive(true);
                }
                if(health == 0)
                {
                    Debug.Log("GAME OVER");
                }

            }

            if (capsuleCollider.gameObject.CompareTag("Green"))
            {
                score += 10;
                capsuleCollider.gameObject.transform.position = playerFirstPosition;
                Debug.Log("SCORE: " + score);
            }
        }
    }

    public void Click()
    {   //TODO: Refactor
        clickCount++;
    }
    

    IEnumerator Move(GameObject _gameObject, Vector3 start, Vector3 end)
    {
        float elapsedTime = 0;
        float duration = 1f;       

        while (elapsedTime < duration)
        {
            _gameObject.transform.localPosition = Vector3.Lerp(start, end, (elapsedTime / duration));
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        player.transform.localPosition = end;
    }
    
}
