using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISpaceMan : MonoBehaviour
{
    Vector3 direction;
    void Start()
    {
        Destroy(gameObject, 15);
        var x = Random.Range(0, 2);
        var y = Random.Range(0, 2);
        if (x==0)
        {
            transform.position = new Vector3((transform.position.x + (Screen.width - transform.position.x + 10))*y,transform.position.y, transform.position.z);
        }
        if (x==1)
        {
            transform.position = new Vector3(transform.position.x, (transform.position.y + (Screen.height - transform.position.y + 10))*y, transform.position.z);
        }
        direction = (Vector2)transform.position - new Vector2(Screen.width / Random.Range(1,10), Screen.height / Random.Range(1,10));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += -direction.normalized * Random.Range(1f,3f);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z+ Random.Range(0f,1f));

    }
}
