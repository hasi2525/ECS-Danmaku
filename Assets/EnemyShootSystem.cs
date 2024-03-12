//using Unity.Entities;
//using Unity.Burst;
//using Unity.Transforms;
//using Unity.Mathematics;
//using Unity.Collections;

//[BurstCompile]
//public partial struct EnemyShootSystem : ISystem
//{
//    public void OnCreate(ref SystemState state)
//    {
//        // �V�X�e���̏���������
//    }

//    public void OnDestroy(ref SystemState state)
//    {
//        // �V�X�e���̔j������
//    }

//    [BurstCompile]
//    public void OnUpdate(ref SystemState state)
//    {
//        // EntityManager�̎擾
//        var entityManager = state.EntityManager;

//        // EntityCommandBuffer�̎擾
//        var entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);

//        // �v���C���[��Entity��������i���̃R�[�h�A�v���C���[�������Tag�R���|�[�l���g�������Ă���Ɖ���j
//        EntityQuery playerQuery = entityManager.CreateEntityQuery(typeof(PlayerTag));
//        Entity playerEntity = playerQuery.GetSingletonEntity();
//        LocalToWorld playerLtw = entityManager.GetComponentData<LocalToWorld>(playerEntity);

//        // �G���e�𔭎˂��鏈��
//        Entities
//            .WithName("EnemyShoot")
//            .WithAll<EnemyTag>()
//            .ForEach((Entity enemyEntity, in LocalToWorld enemyLtw) =>
//            {
//                // �v���C���[�̕������v�Z
//                float3 direction = math.normalize(playerLtw.Position - enemyLtw.Position);

//                // �e�̃v���n�u��Entity�𐶐��i����Entity�A���ۂɂ̓v���n�u����ϊ�����Entity���w��j
//                Entity bulletPrefabEntity = Entity.Null; // ���̒l

//                // �eEntity�̐���
//                Entity bulletEntity = entityCommandBuffer.Instantiate(bulletPrefabEntity);

//                // �e�̈ʒu�ƌ�����ݒ�
//                LocalToWorld bulletLtw = new LocalToWorld
//                {
//                    Value = float4x4.TRS(
//                        enemyLtw.Position, // �e�̏����ʒu�͓G�̈ʒu
//                        quaternion.LookRotationSafe(direction, math.up()), // �e�̌����̓v���C���[�̕���
//                        new float3(1.0f, 1.0f, 1.0f)) // �X�P�[����1
//                };

//                // �eEntity��LocalToWorld�R���|�[�l���g��ݒ�
//                entityCommandBuffer.SetComponent(bulletEntity, bulletLtw);

//            }).Schedule();

//        // EntityCommandBuffer�̎��s
//        entityCommandBuffer.Playback(entityManager);
//        entityCommandBuffer.Dispose();
//    }
//}

////public struct EnemyTag : IComponentData { }
////public struct PlayerTag : IComponentData { }