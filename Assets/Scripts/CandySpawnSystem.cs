using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct CandySpawnSystem : ISystem
{
  public void OnCreate(ref SystemState state)
    => state.RequireForUpdate<Config>();

  [BurstCompile]
  public void OnUpdate(ref SystemState state)
  {
    var config = SystemAPI.GetSingleton<Config>();

    var instances = state.EntityManager.Instantiate
      (config.Prefab, 1, Allocator.Temp);
    var rand = new Random((uint)math.floor(SystemAPI.Time.ElapsedTime * 1000) + 100);
    foreach (var entity in instances)
    {
      var xform = SystemAPI.GetComponentRW<LocalTransform>(entity);

      var v = rand.NextFloat2(-1, 1);
      var h = rand.NextFloat(.5f, 1);
      var size = rand.NextFloat(.1f, .5f);
      xform.ValueRW = LocalTransform
        .FromPosition(math.float3(v.x, h, v.y) * config.SpawnRadius)
        .WithScale(size);
    }
  }
}