using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

public class BulletSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {

        float dt = Time.DeltaTime;

        var jobHandle = Entities.WithName("BulletSystem")
            .ForEach((ref PhysicsVelocity physics,
                ref Translation position,
                ref Rotation rotation,
                ref BulletData bulletData) =>
            {
                physics.Angular = float3.zero;
                physics.Linear += dt * bulletData.Speed * math.forward(rotation.Value);

            })
            .Schedule(inputDeps);

        jobHandle.Complete();

        return jobHandle;
    }
}

