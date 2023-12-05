using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct CandySpawnSystem : ISystem
{
    public void OnCreate(ref SystemState state)
      => state.RequireForUpdate<Config>();

    public void OnUpdate(ref SystemState state)
    {
        var config = SystemAPI.GetSingleton<Config>();

        var instances = state.EntityManager.Instantiate
          (config.Prefab, config.SpawnCount, Allocator.Temp);

        var rand = new Random(1000);
        foreach (var entity in instances)
        {
            var xform = SystemAPI.GetComponentRW<LocalTransform>(entity);

            var v = rand.NextFloat2(-1, 1);
            xform.ValueRW = LocalTransform.FromPosition(math.float3(v.x, 0, v.y) * config.SpawnRadius);
        }

        state.Enabled = false;
    }
}