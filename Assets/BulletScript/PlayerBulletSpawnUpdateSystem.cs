using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Transforms;
using UnityEngine;

// Burst�R���p�C�����g�p���āA���̃V�X�e���̎��s���œK��
[BurstCompile]
//[UpdateInGroup(typeof(SimulationCharaSystemGroup))]
public partial struct PlayerBulletSpawnUpdateSystem : ISystem
{
    // �G���e�B�e�B�N�G�����i�[���邽�߂̃v���C�x�[�g�t�B�[���h
    private EntityQuery _entityQuery; 

    // �V�X�e���̍쐬���Ɉ�x�����Ăяo����܂��B���̃V�X�e���ŕK�v�ȃR���|�[�l���g�^�C�v���w�肵�ăN�G�����\�z
    [BurstCompile]
    void ISystem.OnCreate(ref SystemState state)
    {
        // �K�v�ȃR���|�[�l���g�^�C�v�̔z����쐬
        var components = new NativeArray<ComponentType>(2, Allocator.Temp);
        // �e�۔��˃f�[�^�̓ǂݏ������\�ȃR���|�[�l���g
        components[0] = ComponentType.ReadWrite<PlayerBulletSpawnData>();
        // �ǂݎ���p�̃��[�J�����烏�[���h�ւ̕ϊ��R���|�[�l���g
        components[1] = ComponentType.ReadOnly<LocalToWorld>();

        // �w�肳�ꂽ�R���|�[�l���g�����G���e�B�e�B�ɑ΂���N�G�����쐬
        _entityQuery = state.GetEntityQuery(components);
        components.Dispose(); // �ꎞ�z���j�����āA�����������
    }

    [BurstCompile]
    void ISystem.OnUpdate(ref SystemState state)
    {
        // EntityManager���擾���āA�G���e�B�e�B�̃R���|�[�l���g���Ǘ�
        EntityManager entityManager = state.EntityManager;
        // EntityCommandBuffer���g�p���āA�G���e�B�e�B�̍쐬��R���|�[�l���g�̕ύX���L���[�ɓ���A�ꊇ�ŏ���
        EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp, PlaybackPolicy.MultiPlayback);

        // ���݂̃t���[���̃f���^�^�C�����擾
        float deltaTime = SystemAPI.Time.DeltaTime;
        // �N�G���Ɉ�v���邷�ׂẴG���e�B�e�B��z��Ƃ��Ď擾
        var entities = _entityQuery.ToEntityArray(Allocator.Temp);
        foreach (Entity entity in entities)
        {
            // �e�̔��˃f�[�^�����G���e�B�e�B����A���̃f�[�^���擾
            var spawner = entityManager.GetComponentData<PlayerBulletSpawnData>(entity);

            //���N���b�N�������Ă���Ԓe�𔭎�
            if (Input.GetMouseButton(0))
            {
                // �e�̔��ˊԊu���J�E���g�_�E�����܂��B
                if (spawner.BulletSpawnCurrentTime > 0f)
                {
                    spawner.BulletSpawnCurrentTime -= deltaTime;
                    entityManager.SetComponentData(entity, spawner);
                    // �܂����ˎ��ԂɒB���Ă��Ȃ��ꍇ�́A���̃G���e�B�e�B�Ɉڂ�
                    continue;
                }

                // ���ˎ��ԂɒB������A���̔��˂܂ł̎��Ԃ����Z�b�g
                spawner.BulletSpawnCurrentTime = spawner.BulletSpawnInterval;
                entityManager.SetComponentData(entity, spawner);

                // �e�̃G���e�B�e�B�𐶐����܂��B�e�̌��^�i�v���g�^�C�v�j����V�����G���e�B�e�B���C���X�^���X��
                LocalToWorld spawnerLtw = entityManager.GetComponentData<LocalToWorld>(entity);
                Entity bulletEntity = entityCommandBuffer.Instantiate(spawner.BulletPrototype);

                // �e�̈ʒu�Ɖ�]���A���ˌ��̃G���e�B�e�B�Ɋ�Â��Đݒ�
                var bulletTransform = LocalTransform.FromPositionRotation(spawnerLtw.Position, spawnerLtw.Rotation);
                entityCommandBuffer.AddComponent(bulletEntity, bulletTransform);
            }

        }

        // EntityCommandBuffer�ɗ��܂����R�}���h�i�G���e�B�e�B�̐�����R���|�[�l���g�̒ǉ��Ȃǁj�����s
        entityCommandBuffer.Playback(entityManager);
        // �g�p�������\�[�X�����
        entities.Dispose();
        entityCommandBuffer.Dispose();
    }
}