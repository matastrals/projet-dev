using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SetStart : MonoBehaviour
{
    void Start()
    {
        float positionX = PlayerPrefs.GetFloat("Position X");
        float positionY = PlayerPrefs.GetFloat("Position Y");

        Vector3 position = new Vector3(positionX, positionY, 0);

        transform.position = position;
    }
}
