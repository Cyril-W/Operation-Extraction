using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    [SerializeField] GameObject Player;

    [SerializeField, Range(0, 10)] float DampeningSpeed = 2;

    [SerializeField] float CamDistance = .5f;
    [SerializeField] float CamHeight = .15f;

    [SerializeField] float LookAhead = .05f;

    [SerializeField, Range(0, 1)] float Side = .5f;

    float d, h, l, s;

    void OnEnable()
    {
        SetValues(true);
    }

    void Update()
    {
        if (!Player) return;

        SetValues();

        SetCamera();
    }

    void SetValues(bool OnEnable = false)
    {
        if (OnEnable)
        {
            d = CamDistance;
            h = CamHeight;
            l = LookAhead;
            s = Mathf.Clamp01(Side);
        }
        else
        {
            d = Mathf.Lerp(d, CamDistance, Time.deltaTime * DampeningSpeed);
            h = Mathf.Lerp(h, CamHeight, Time.deltaTime * DampeningSpeed);
            l = Mathf.Lerp(l, LookAhead, Time.deltaTime * DampeningSpeed);
            s = Mathf.Clamp01(Mathf.Lerp(s, Side, Time.deltaTime * DampeningSpeed));
        }
    }


    [ContextMenu("SetCamera")]
    void SetCamera()
    {
        Vector3 pos = Player.transform.position;

        pos.z = 0;

        pos += Quaternion.Lerp(Quaternion.AngleAxis(90, Vector3.up), Quaternion.AngleAxis(-90, Vector3.up), s) * Vector3.forward * -d;

        pos += Vector3.up * h;

        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * DampeningSpeed);

        Vector3 lookAt = Player.transform.position + Player.transform.forward * l;

        Quaternion rot = Quaternion.LookRotation(lookAt - pos);

        transform.rotation = rot;
    }

    public void SetCamDist(float f) { CamDistance = f; }
    public void SetCamHeight(float f) { CamHeight = f; }
    public void SetLookAhead(float f) { LookAhead = f; }
    public void SetSide(float f) { Side = f; }
}
