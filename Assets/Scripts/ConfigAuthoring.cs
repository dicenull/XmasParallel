using Unity.Entities;
using UnityEngine;

public struct Config : IComponentData
{
    public Entity Prefab;
    public int SpawnRadius;
    public int SpawnCount;
}

public class ConfigAuthoring : MonoBehaviour
{
    public GameObject Prefab = null;
    public int SpawnRadius = 1;
    public int SpawnCount = 10;


    class Baker : Baker<ConfigAuthoring>
    {
        public override void Bake(ConfigAuthoring src)
        {
            var data = new Config()
            {
                Prefab = GetEntity(src.Prefab, TransformUsageFlags.Dynamic),
                SpawnRadius = src.SpawnRadius,
                SpawnCount = src.SpawnCount,
            };
            AddComponent(GetEntity(TransformUsageFlags.None), data);
        }
    }
}