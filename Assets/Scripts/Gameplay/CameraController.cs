using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform followed = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (followed != null) transform.position = new Vector3(followed.position.x, followed.position.y, -10.0f);
    }

    public void SetFollowed(Transform followed)
    {
        this.followed = followed;
    }
}
