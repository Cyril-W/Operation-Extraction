using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    [System.Serializable]
    public class CameraParameters
    {
        public float DampeningSpeed = 2f;

        [Space(10)]

        public float CamDistance = .5f;
        public float CamHeight = .15f;

        public float LookAhead = .05f;

        [Space(10)]

        public float Side = .5f;
    }

    public GameObject Player;

    public CameraParameters Parameters;

    float d, h, l, s;

    [SerializeField] Vector2 ZTranslation = new Vector2(-.2f, .2f);

    void OnValidate()
    {
        SetParameters(Parameters, true);

        SetCamera();
    }

    void OnEnable()
    {
        d = Parameters.CamDistance;
        h = Parameters.CamHeight;
        l = Parameters.LookAhead;
        s = Mathf.Clamp01(Parameters.Side);
    }

    void Update()
    {
        if (!Player) return;

        SetCamera();
    }

    public void SetParameters(CameraParameters camP, bool NoLerp = false)
    {
        if (NoLerp)
        {
            d = camP.CamDistance;
            h = camP.CamHeight;
            l = camP.LookAhead;
            s = camP.Side;
        }
        else
        {
            d = Mathf.Lerp(d, camP.CamDistance, Time.deltaTime * Parameters.DampeningSpeed);
            h = Mathf.Lerp(h, camP.CamHeight, Time.deltaTime * Parameters.DampeningSpeed);
            l = Mathf.Lerp(l, camP.LookAhead, Time.deltaTime * Parameters.DampeningSpeed);
            s = Mathf.Clamp01(Mathf.Lerp(s, camP.Side, Time.deltaTime * Parameters.DampeningSpeed));
        }
    }


    [ContextMenu("SetCamera")]
    void SetCamera()
    {
        if (!Player) return;

        Vector3 pos = Player.transform.position;

        pos.z = Mathf.Clamp(pos.z, ZTranslation.x, ZTranslation.y);

        pos += Quaternion.Lerp(Quaternion.AngleAxis(90, Vector3.up), Quaternion.AngleAxis(-90, Vector3.up), s) * Vector3.forward * -d;

        pos += Vector3.up * h;

        //transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * Parameters.DampeningSpeed);
        transform.position = pos;

        Vector3 lookAt = Player.transform.position + Player.transform.forward * l;

        Quaternion rot = Quaternion.LookRotation(lookAt - pos);

        transform.rotation = rot;
    }

    public void SetCamDist(float f) { Parameters.CamDistance = f; }
    public void SetCamHeight(float f) { Parameters.CamHeight = f; }
    public void SetLookAhead(float f) { Parameters.LookAhead = f; }
    public void SetSide(float f) { Parameters.Side = f; }
}
