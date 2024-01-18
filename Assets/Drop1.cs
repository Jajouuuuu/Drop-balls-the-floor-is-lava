using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop1 : MonoBehaviour {

    public Vector3 scaleChange;

    void Start() {
        
    }

    void Update() {
        transform.localScale += scaleChange;
    }
}
