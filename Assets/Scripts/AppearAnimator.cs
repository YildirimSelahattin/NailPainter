using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearAnimator : MonoBehaviour {
    public int ball_index;
    [SerializeField] Vector3 max_scale; // Target scale
    [SerializeField] float grow_speed; // Speed of growing
    [SerializeField] Vector3 start_scale; // Start scale (dont set here 0 for axises, use very small value but not 0)

    private void OnEnable() 
    {
        StartCoroutine("Scale"); // When object becomes enabled - need to scale it smoothly
    }

    IEnumerator Scale() 
    {
        transform.localScale = start_scale; // Assign start scale to start from
        while(max_scale.x > transform.localScale.x || max_scale.y > transform.localScale.y || max_scale.z > transform.localScale.z) { // While object not reached maximum scale by each axis
            transform.localScale += new Vector3(start_scale.x, start_scale.y, start_scale.z) * Time.deltaTime * grow_speed; // Increase size of this object
            yield return null;
        }
        if(max_scale.x < transform.localScale.x || max_scale.y < transform.localScale.y || max_scale.z < transform.localScale.z) { // Size accuracy correction
            transform.localScale = max_scale; // If object growed too much by some axis - set max size to object
        }
    }
}