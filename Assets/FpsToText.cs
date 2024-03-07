/*
 * FPS��TextMeshPro�ɕ\������N���X
 * FpsToText.cs : Ver. 1.0.0
 * Written by Takashi Sowa
 * ���g����
 * TextMeshPro�œK����Text�����
 * ���̃N���X��K���ȃI�u�W�F�N�g�ɃA�^�b�`���A�C���X�y�N�^�����L�ō����Text�R���|�[�l���g��o�^���邾��
 * �ʒu��t�H���g��t�H���g�T�C�Y��Text�̃C���X�y�N�^���玩���Ŏ��R�ɐݒ�
 * ���ׂ��C�ɂȂ�ꍇ�͏����^�C�~���O�𒲐����鎖����
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FpsToText : MonoBehaviour
{

	[SerializeField, Header("��TextMeshPro�R���|�[�l���g")]
	TextMeshProUGUI text;

	[SerializeField, Header("�����b���ƂɃe�L�X�g�X�V���邩�F���׌y���p"), Range(0, 5)]
	int second = 1;

	int frameCount = 0;//Update���Ă΂ꂽ�񐔃J�E���g�p
	float oldTime = 0.0f;//�O��t���[�����[�g��\�����Ă���̌o�ߎ��Ԍv�Z�p

	/*------------------------------------------------------------
	1�t���[����1��Ăяo�����AGameObject�ƃR���|�[�l���g���L���Ȏ��Ɏ��s�����
	------------------------------------------------------------*/
	void Update()
	{
		//Update���Ă΂ꂽ�񐔂����Z
		frameCount++;

		//�O�t���[������̌o�ߎ��Ԃ��v�Z�FTime.realtimeSinceStartup�̓Q�[���J�n������̌o�ߎ��ԁi�b�j
		float time = Time.realtimeSinceStartup - oldTime;

		//�w�莞�Ԃ𒴂�����e�L�X�g�X�V
		if (time >= second)
		{
			//�t���[�����[�g���v�Z
			float fps = frameCount / time;

			//�v�Z�����t���[�����[�g�������_2���܂Ŋۂ߂ăe�L�X�g�\���FSetText()���g�p���ăG�f�B�^�ȊO�ł�GC�𔭐������Ȃ�
			text.SetText("{0:2} FPS", fps);

			//�J�E���g�ƌo�ߎ��Ԃ����Z�b�g
			frameCount = 0;
			oldTime = Time.realtimeSinceStartup;
		}
	}

}