using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TargetFollower : MonoBehaviour
{
    [System.Serializable]
    class Axis
    {
        public bool X = true;
        public bool Y = true;
        public bool Z = true;
    }

    [Header("Parameters")]
    [SerializeField] public Transform Target;
    [SerializeField] public Transform Follower;

    [Space(10)]

    [SerializeField] bool copyPosition;
    [SerializeField] Axis PositionAxis;
    [SerializeField] Vector3 offsetPosition;

    [Space(10)]

    [SerializeField] bool copyRotation;
    [SerializeField] Vector3 offsetRotation;

    [Space(10)]

    [SerializeField] bool copyScale;
    [SerializeField] Vector3 offsetScale;

    void Update()
    {
        Follow();
    }

    [ContextMenu("Follow")]
    public void Follow()
    {
        if (Target == null) return;

        if (copyPosition)
        {
            if (Follower)
            {
                Vector3 tmp = Target.position + offsetPosition;

                Vector3 target = Follower.position;

                if (PositionAxis.X) target.x = tmp.x;
                if (PositionAxis.Y) target.y = tmp.y;
                if (PositionAxis.Z) target.z = tmp.z;

                Follower.position = target;
            }
            else
            {
                Vector3 tmp = Target.position + offsetPosition;

                Vector3 target = transform.position;

                if (PositionAxis.X) target.x = tmp.x;
                if (PositionAxis.Y) target.y = tmp.y;
                if (PositionAxis.Z) target.z = tmp.z;

                transform.position = target;
            }
        }
        if (copyRotation)
        {
            if (Follower)
            {
                Follower.rotation = Target.rotation * Quaternion.Euler(offsetRotation.x, offsetRotation.y, offsetRotation.z);
            }
            else
            {
                transform.rotation = Target.rotation * Quaternion.Euler(offsetRotation.x, offsetRotation.y, offsetRotation.z);
            }
            
        }
        if (copyScale)
        {
            if (Follower)
            {
                Follower.localScale = Target.lossyScale + offsetScale;
            }
            else
            {
                transform.localScale = Target.lossyScale + offsetScale;
            }
        }
    } 

    public void SetCopyPosition(bool newCopyPosition)
    {
        copyPosition = newCopyPosition;
    }

    public void SetCopyRotation(bool newCopyRotation)
    {
        copyRotation = newCopyRotation;
    }

    public void SetTarget(Transform tr)
    {
        Target = tr;
    }

    public void SetFollower(Transform tr)
    {
        Follower = tr;
    }

    public void SetOffsetPosition(Vector3 newOffset)
    {
        offsetPosition = newOffset;
    }

    public Vector3 GetCurrentOffsetRotation()
    {
        return offsetRotation;
    }

    public void SetOffsetRotation(Vector3 newOffset)
    {
        offsetRotation = newOffset;
    }
}
