using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneScript : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private Transform cameraMarker;
    [SerializeField] private FrogMovement frogMovement;
    private float distance;
    void Awake()
    {
        distance = (pointA.position - pointB.position).magnitude;
        cameraMarker.position = pointA.position;
        StartCoroutine(PlayScene());
    }
    private IEnumerator PlayScene()
    {
        yield return new WaitForSeconds(1);
        while(pointB.position != cameraMarker.position)
        {
            cameraMarker.position = Vector2.MoveTowards(cameraMarker.position, pointB.position, distance / 5 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        frogMovement.isControllEnabled = true;
    }
}
