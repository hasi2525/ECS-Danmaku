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
        // �v���C���[���������邽�߂̃N�G�����쐬
        playerQuery = GetEntityQuery(ComponentType.ReadOnly<PlayerTag>(), ComponentType.ReadOnly<LocalTransform>());
    }

    protected override void OnUpdate()
    {
        // �v���C���[�̈ʒu���擾
        float3 playerPosition = float3.zero;
        if (playerQuery.CalculateEntityCount() > 0)
        {
            var playerTranslations = playerQuery.ToComponentDataArray<LocalTransform>(Allocator.TempJob);
            playerPosition = playerTranslations[0].Position;
            playerTranslations.Dispose();
        }

        // �����𒲐�����G���e�B�e�B�̃W���u���X�P�W���[��
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
//        // �v���C���[�̃G���e�B�e�B���擾
//        Entity playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();

//        // �v���C���[�̈ʒu���擾
//        float3 playerPosition = SystemAPI.GetComponentLookup<LocalTransform>(true)[playerEntity].Position;

//        // �v���C���[�������悤��LocalTransform��Rotation���X�V����
//        foreach ((RefRO<PlayerTag> playerParam, RefRW<LocalTransform> localTransform) in SystemAPI.Query<RefRO<PlayerTag>, RefRW<LocalTransform>>())
//        {

//        }

//    }
//}