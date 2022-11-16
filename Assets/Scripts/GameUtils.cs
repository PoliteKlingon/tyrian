using UnityEngine;

public class GameUtils : MonoBehaviour
{
    public static Vector3 ComputeEulerStep(
        Vector3 x0, Vector3 dx_dt, float delta_t
    )
    {
        return x0 + delta_t * dx_dt;
    }
    
    public static Vector3 ComputeSeekAccel(
        Vector3 pos, float maxAccel, Vector3 targetPos
        )
    {
        Vector3 dir = targetPos - pos;
// NOTE: We add 1e-6f to handle the case when pos == targetPos.
        return (maxAccel / (dir.magnitude + 1e-6f)) * dir;
    }
    public static Vector3 ComputeSeekVelocity(
        Vector3 pos, Vector3 velocity,
        float maxSpeed, float maxAccel,
        Vector3 targetPos, float dt
        )
    {
        Vector3 seek_accel = ComputeSeekAccel(pos, maxAccel, targetPos);
        velocity = ComputeEulerStep(velocity, seek_accel, dt);
        if (velocity.sqrMagnitude > maxSpeed * maxSpeed)
            velocity *= (maxSpeed / velocity.magnitude);
        return velocity;
    }
}
