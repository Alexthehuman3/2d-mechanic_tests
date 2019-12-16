using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private GameObject player_obj;
    private BoxCollider2D player_touch_zone;
    public Vector3 new_position;

    private bool out_of_bound;
    public bool is_paused;

//Prefab Settings---------------------------------------------------------------------------------
    public float move_speed;

    // Start is called before the first frame update
    void Start()
    {
        player_obj = this.gameObject;
        player_touch_zone = this.GetComponent<BoxCollider2D>();
        new_position = transform.position;

        out_of_bound = false;
        is_paused = false;
        StartCoroutine(DebugPosition(this.gameObject, 2000));
    }

    private void Update()
    {
        if (transform.position.x <= 0 || transform.position.x >= Screen.width)
        {
            out_of_bound = true;
        }
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            //hmmm
            RaycastHit hit;
            if(!out_of_bound || !is_paused)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    new_position = hit.point;
                    this.gameObject.transform.position = new_position;
                }
                //move player toward clicked position
                Debug.Log("player clicked.");
            }
        }
    }

    IEnumerator DebugPosition(GameObject obj, int amount_of_loops)
    {
        for (int x = 0; x < amount_of_loops + 1; x++)
        {
            Debug.Log("x: " + obj.transform.position.x + " Y: "+ obj.transform.position.y);
            yield return new WaitForSeconds(2);
        }
        
    }
}
