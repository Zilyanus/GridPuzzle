using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform CMvcam;
    [SerializeField] float Speed;

    [SerializeField] string CamControlIndex;
    public bool isPaused;

    [SerializeField] AnimationCurve SpeedCurve;

    [SerializeField] float CurveSpeed;

    float pressedTime = 0;

    [SerializeField] List<Transform> Edges;
    // Start is called before the first frame update
    void Start()
    {
        CamControlIndex = ES3.Load<string>("CamControl",defaultValue: "mk");
    }

    // Update is called once per frame
    void Update()
    {
        CurveSpeed = EvalSpeed(pressedTime);

        float MouseX = Input.mousePosition.x;
        if ((CamControlIndex.Contains("m") && MouseX < Screen.width * 1 / 10) || (CamControlIndex.Contains("k") && Input.GetKey(KeyCode.A)))
        {
            pressedTime += Time.deltaTime;
            CMvcam.position -= transform.right * Time.deltaTime * CurveSpeed;
        }
        else if ((CamControlIndex.Contains("m") && MouseX > Screen.width * 9 / 10) || (CamControlIndex.Contains("k") && Input.GetKey(KeyCode.D)))
        {
            float MouseXInvert = Screen.width - MouseX;

            pressedTime += Time.deltaTime;
            CMvcam.position += transform.right * Time.deltaTime * CurveSpeed;
        }
        else
        {
            pressedTime = 0;
        }
        CMvcam.position = new Vector3(Mathf.Clamp(CMvcam.position.x, Edges[0].position.x, Edges[1].position.x), CMvcam.position.y, CMvcam.position.z);
    }

    private float EvalSpeed(float presseDuratıon)
    {
        return Speed / Mathf.Clamp(SpeedCurve.Evaluate(presseDuratıon), 1f, 15f);
    }
}
