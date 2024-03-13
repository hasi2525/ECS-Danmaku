using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public partial class LookAtPlayerSystem : SystemBase
{
    private EntityQuery playerQuery;

    protected override void OnCreate()
    {
        // プレイヤーを検索するためのクエリを作成
        playerQuery = GetEntityQuery(ComponentType.ReadOnly<PlayerTag>(), ComponentType.ReadOnly<LocalTransform>());
    }

    protected override void OnUpdate()
    {
        // プレイヤーの位置を取得
        float3 playerPosition = float3.zero;
        if (playerQuery.CalculateEntityCount() > 0)
        {
            var playerTranslations = playerQuery.ToComponentDataArray<LocalTransform>(Allocator.TempJob);
            playerPosition = playerTranslations[0].Position;
            playerTranslations.Dispose();
        }

        // 向きを調整するエンティティのジョブをスケジュール
        Entities
            .WithAll<LookAtPlayerTag>()
            .ForEach((ref LocalTransform localTransform) =>
        {
            float3 direction = math.normalize(playerPosition - localTransform.Position);
            localTransform.Rotation = quaternion.LookRotationSafe(direction, math.up());
        }).Run();
    }
}

//using Unity.Entities;
//using Unity.Burst;
//using Unity.Mathematics;
//using Unity.Transforms;

//public partial struct LookAtPlayerSystem : ISystem
//{
//    [BurstCompile]
//    public void OnUpdate(ref SystemState state)
//    {
//        // プレイヤーのエンティティを取得
//        Entity playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();

//        // プレイヤーの位置を取得
//        float3 playerPosition = SystemAPI.GetComponentLookup<LocalTransform>(true)[playerEntity].Position;

//        // プレイヤーを向くようにLocalTransformのRotationを更新する
//        foreach ((RefRO<PlayerTag> playerParam, RefRW<LocalTransform> localTransform) in SystemAPI.Query<RefRO<PlayerTag>, RefRW<LocalTransform>>())
//        {

//        }

//    }
//}