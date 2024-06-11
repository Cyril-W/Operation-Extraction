using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    enum SwitchType { Horizontal, Vertical}

    [SerializeField] SwitchType Type;

    [SerializeField] CameraPlayer CamPlayer;

    bool inside;

    [SerializeField] CameraPlayer.CameraParameters CamIn;

    [SerializeField] CameraPlayer.CameraParameters CamOut;

    CameraPlayer.CameraParameters lerpParameters = new CameraPlayer.CameraParameters();

    float lerp = 0;

    void OnEnable()
    {
        if (CamPlayer) lerpParameters = CamPlayer.Parameters;
    }

    void Update()
    {
        if (!CamPlayer || !inside) return;

        Vector3 player = CamPlayer.Player.transform.position;

        Vector3 limitA = transform.position;
        Vector3 limitB = transform.position;

        if (Type == SwitchType.Horizontal)
        {
            limitA -= (transform.right * transform.lossyScale.x / 2);
            limitB += (transform.right * transform.lossyScale.x / 2);

            player.y = player.z = limitA.y = limitA.z = limitB.y = limitB.z = 0;
        }
        else if (Type == SwitchType.Vertical)
        {
            limitA -= (transform.up * transform.lossyScale.y / 2);
            limitB += (transform.up * transform.lossyScale.y / 2);

            player.x = player.z = limitA.x = limitA.z = limitB.x = limitB.z = 0;
        }

        float distance = Vector3.Distance(limitA, limitB);

        lerp = Mathf.Clamp01(Vector3.Distance(player, limitA) / distance);

        lerpParameters.CamDistance = Mathf.Lerp(CamIn.CamDistance, CamOut.CamDistance, lerp);
        lerpParameters.CamHeight = Mathf.Lerp(CamIn.CamHeight, CamOut.CamHeight, lerp);
        lerpParameters.LookAhead = Mathf.Lerp(CamIn.LookAhead, CamOut.LookAhead, lerp);
        lerpParameters.Side = Mathf.Lerp(CamIn.Side, CamOut.Side, lerp);

        CamPlayer.SetParameters(lerpParameters);
    }

    void OnTriggerEnter(Collider col)
    {
        if (CamPlayer) lerpParameters = CamPlayer.Parameters;
        inside = true;
    }

    void OnTriggerExit(Collider col)
    {
        inside = false;
    }

    void OnDrawGizmos()
    {
        Vector3 pos = transform.position;

        float size = .15f;

        if (Type == SwitchType.Horizontal)
        {
            Vector3 posA = pos + (transform.right * transform.lossyScale.x / 2);
            Vector3 posB = pos - (transform.right * transform.lossyScale.x / 2);


            Gizmos.color = Color.blue;

            Gizmos.DrawLine(posA + (transform.forward * size) + (transform.up * size), posA + (transform.forward * size) - (transform.up * size));
            Gizmos.DrawLine(posA + (transform.forward * size) + (transform.up * size), posA - (transform.forward * size) + (transform.up * size));
            Gizmos.DrawLine(posA - (transform.forward * size) + (transform.up * size), posA - (transform.forward * size) - (transform.up * size));
            Gizmos.DrawLine(posA - (transform.forward * size) - (transform.up * size), posA + (transform.forward * size) - (transform.up * size));


            Gizmos.color = Color.red;

            Gizmos.DrawLine(posB + (transform.forward * size) + (transform.up * size), posB + (transform.forward * size) - (transform.up * size));
            Gizmos.DrawLine(posB + (transform.forward * size) + (transform.up * size), posB - (transform.forward * size) + (transform.up * size));
            Gizmos.DrawLine(posB - (transform.forward * size) + (transform.up * size), posB - (transform.forward * size) - (transform.up * size));
            Gizmos.DrawLine(posB - (transform.forward * size) - (transform.up * size), posB + (transform.forward * size) - (transform.up * size));
        }
        else if (Type == SwitchType.Vertical)
        {
            Vector3 posA = pos + (transform.up * transform.lossyScale.y / 2);
            Vector3 posB = pos - (transform.up * transform.lossyScale.y / 2);


            Gizmos.color = Color.blue;

            Gizmos.DrawLine(posA + (transform.forward * size) + (transform.right * size), posA + (transform.forward * size) - (transform.right * size));
            Gizmos.DrawLine(posA + (transform.forward * size) + (transform.right * size), posA - (transform.forward * size) + (transform.right * size));
            Gizmos.DrawLine(posA - (transform.forward * size) + (transform.right * size), posA - (transform.forward * size) - (transform.right * size));
            Gizmos.DrawLine(posA - (transform.forward * size) - (transform.right * size), posA + (transform.forward * size) - (transform.right * size));


            Gizmos.color = Color.red;

            Gizmos.DrawLine(posB + (transform.forward * size) + (transform.right * size), posB + (transform.forward * size) - (transform.right * size));
            Gizmos.DrawLine(posB + (transform.forward * size) + (transform.right * size), posB - (transform.forward * size) + (transform.right * size));
            Gizmos.DrawLine(posB - (transform.forward * size) + (transform.right * size), posB - (transform.forward * size) - (transform.right * size));
            Gizmos.DrawLine(posB - (transform.forward * size) - (transform.right * size), posB + (transform.forward * size) - (transform.right * size));
        }
    }
}
