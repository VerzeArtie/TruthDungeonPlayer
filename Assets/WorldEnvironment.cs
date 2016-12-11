using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace DungeonPlayer
{
    public class WorldEnvironment : MonoBehaviour
    {
        // 640 x 480���C�A�E�g����1024 x 768�֕ύX�����ہA�v���C���[�̃_���W�����ʒu���̌݊�����
        // �ۂĂȂ��Ȃ��Ă��܂������߁A����o�[�W�����Ǘ����s�����ƂŌ݊���ۂB
        protected int version = 0; // ��Ғǉ�

        // �_���W�����A�Ҍ�A�_���W�����A�Ҍ�̃C�x���g������A�h���h����̃Z�[�u��A�_���W�����o�����O�̃Z�[�u��A
        // �ȂǁA�Z�[�u�E���[�h���s�����ŃC�x���g���i��ł��܂������C�����邽�߂̃t���O
        protected bool alreadyShownEvent = false; // ��Ғǉ�

        #region "�O��"
        protected int gameDay = 1; // �Q�[�����Ōo�߂�������
        protected bool saveByDungeon; // �_���W�������ŃZ�[�u
        protected int dungeonPosX; // �_���W�������ŃZ�[�u�����ꏊX
        protected int dungeonPosY; // �_���W�������ŃZ�[�u�����ꏊY
        protected int dungeonPosX2; // �_���W�����ړ����P��̃Z�[�u�����ꏊX
        protected int dungeonPosY2; // �_���W�����ړ����P��̃Z�[�u�����ꏊY
        protected bool alreadyRest; // �z�[���^�E���ŋx���ς�
        protected bool alreadyCommunicate; // �z�[���^�E���Ń��i�Ɖ�b��
        protected bool alreadyEquipShop; // �z�[���^�E���ŃK���c�Ɖ�b��
        protected bool oneDeny; // ���i�C�x���g�P
        protected bool communicationFirstContact2; // ���i�C�x���g�Q
        protected bool communicationSuccess2; // ���i�C�x���g�Q�|�Q
        protected bool availableEquipShop; // �K���c����I�[�v���i�_���W�����P�K�p�����ցj
        protected bool availableEquipShop2; // �K���c����_���W�����Q�K�p������
        protected bool availableEquipShop3; // �K���c����_���W�����R�K�p������
        protected bool availableEquipShop4; // �K���c����_���W�����S�K�p������
        protected bool availableEquipShop5; // �K���c����_���W�����T�K�p������
        protected bool availableFirstCharacter = true; // �P�l�ڂ̃p�[�e�B�ǉ�
        protected bool availableSecondCharacter; // �Q�l�ڂ̃p�[�e�B�ǉ�
        protected bool availableThirdCharacter; // �R�l�ڂ̃p�[�e�B�ǉ�
        protected bool communicationThirdChara1; // ���F���[�C�x���g�P
        protected bool communicationEnterFourArea; // �S�K�w�n�߂Č��������̃C�x���g
        protected bool availableItemSort; // �A�C�e���\�[�g�\

        // [���]�F�z�񂪗ǂ������m��Ȃ����A�w�l�k�A�g�Ɖǐ����d�����鎖�Ƃ���B
        #region "��ʉ�b�p"
        // ���i��ʉ�b�C�x���g
        public bool CommunicationLana1 { get; set; }
        public bool CommunicationLana2 { get; set; }
        public bool CommunicationLana3 { get; set; }
        public bool CommunicationLana4 { get; set; }
        public bool CommunicationLana5 { get; set; }
        public bool CommunicationLana6 { get; set; }
        public bool CommunicationLana7 { get; set; }
        public bool CommunicationLana8 { get; set; }
        public bool CommunicationLana9 { get; set; }
        public bool CommunicationLana10 { get; set; }
        public bool CommunicationLana11 { get; set; }
        public bool CommunicationLana12 { get; set; }
        public bool CommunicationLana13 { get; set; }
        public bool CommunicationLana14 { get; set; }
        public bool CommunicationLana15 { get; set; }
        public bool CommunicationLana16 { get; set; }
        public bool CommunicationLana17 { get; set; }
        public bool CommunicationLana18 { get; set; }
        public bool CommunicationLana19 { get; set; }
        public bool CommunicationLana20 { get; set; }
        public bool CommunicationLana21 { get; set; }
        public bool CommunicationLana22 { get; set; }
        public bool CommunicationLana23 { get; set; }
        public bool CommunicationLana24 { get; set; }
        public bool CommunicationLana25 { get; set; }
        public bool CommunicationLana26 { get; set; }
        public bool CommunicationLana27 { get; set; }
        public bool CommunicationLana28 { get; set; }
        public bool CommunicationLana29 { get; set; }
        public bool CommunicationLana30 { get; set; }
        public bool CommunicationLana31 { get; set; }
        public bool CommunicationLana32 { get; set; }
        public bool CommunicationLana33 { get; set; }
        public bool CommunicationLana34 { get; set; }
        public bool CommunicationLana35 { get; set; }
        public bool CommunicationLana36 { get; set; }
        public bool CommunicationLana37 { get; set; }
        public bool CommunicationLana38 { get; set; }
        public bool CommunicationLana39 { get; set; }
        public bool CommunicationLana40 { get; set; }
        public bool CommunicationLana41 { get; set; }
        public bool CommunicationLana42 { get; set; }
        public bool CommunicationLana43 { get; set; }
        public bool CommunicationLana44 { get; set; }
        public bool CommunicationLana45 { get; set; }
        public bool CommunicationLana46 { get; set; }
        public bool CommunicationLana47 { get; set; }
        public bool CommunicationLana48 { get; set; }
        public bool CommunicationLana49 { get; set; }
        public bool CommunicationLana50 { get; set; }
        public bool CommunicationLana51 { get; set; }
        public bool CommunicationLana52 { get; set; }
        public bool CommunicationLana53 { get; set; }
        public bool CommunicationLana54 { get; set; }
        public bool CommunicationLana55 { get; set; }
        public bool CommunicationLana56 { get; set; }
        public bool CommunicationLana57 { get; set; }
        public bool CommunicationLana58 { get; set; }
        public bool CommunicationLana59 { get; set; }
        public bool CommunicationLana60 { get; set; }
        public bool CommunicationLana61 { get; set; }
        public bool CommunicationLana62 { get; set; }
        public bool CommunicationLana63 { get; set; }
        public bool CommunicationLana64 { get; set; }
        public bool CommunicationLana65 { get; set; }
        public bool CommunicationLana66 { get; set; }
        public bool CommunicationLana67 { get; set; }
        public bool CommunicationLana68 { get; set; }
        public bool CommunicationLana69 { get; set; }
        public bool CommunicationLana70 { get; set; }
        public bool CommunicationLana71 { get; set; }
        public bool CommunicationLana72 { get; set; }
        public bool CommunicationLana73 { get; set; }
        public bool CommunicationLana74 { get; set; }
        public bool CommunicationLana75 { get; set; }
        public bool CommunicationLana76 { get; set; }
        public bool CommunicationLana77 { get; set; }
        public bool CommunicationLana78 { get; set; }
        public bool CommunicationLana79 { get; set; }
        public bool CommunicationLana80 { get; set; }
        public bool CommunicationLana81 { get; set; }
        public bool CommunicationLana82 { get; set; }
        public bool CommunicationLana83 { get; set; }
        public bool CommunicationLana84 { get; set; }
        public bool CommunicationLana85 { get; set; }
        public bool CommunicationLana86 { get; set; }
        public bool CommunicationLana87 { get; set; }
        public bool CommunicationLana88 { get; set; }
        public bool CommunicationLana89 { get; set; }
        public bool CommunicationLana90 { get; set; }
        public bool CommunicationLana91 { get; set; }
        public bool CommunicationLana92 { get; set; }
        public bool CommunicationLana93 { get; set; }
        public bool CommunicationLana94 { get; set; }
        public bool CommunicationLana95 { get; set; }
        public bool CommunicationLana96 { get; set; }
        public bool CommunicationLana97 { get; set; }
        public bool CommunicationLana98 { get; set; }
        public bool CommunicationLana99 { get; set; }
        public bool CommunicationLana100 { get; set; }

        // �K���c��ʉ�b�C�x���g
        public bool CommunicationGanz1 { get; set; }
        public bool CommunicationGanz2 { get; set; }
        public bool CommunicationGanz3 { get; set; }
        public bool CommunicationGanz4 { get; set; }
        public bool CommunicationGanz5 { get; set; }
        public bool CommunicationGanz6 { get; set; }
        public bool CommunicationGanz7 { get; set; }
        public bool CommunicationGanz8 { get; set; }
        public bool CommunicationGanz9 { get; set; }
        public bool CommunicationGanz10 { get; set; }
        public bool CommunicationGanz11 { get; set; }
        public bool CommunicationGanz12 { get; set; }
        public bool CommunicationGanz13 { get; set; }
        public bool CommunicationGanz14 { get; set; }
        public bool CommunicationGanz15 { get; set; }
        public bool CommunicationGanz16 { get; set; }
        public bool CommunicationGanz17 { get; set; }
        public bool CommunicationGanz18 { get; set; }
        public bool CommunicationGanz19 { get; set; }
        public bool CommunicationGanz20 { get; set; }
        public bool CommunicationGanz21 { get; set; }
        public bool CommunicationGanz22 { get; set; }
        public bool CommunicationGanz23 { get; set; }
        public bool CommunicationGanz24 { get; set; }
        public bool CommunicationGanz25 { get; set; }
        public bool CommunicationGanz26 { get; set; }
        public bool CommunicationGanz27 { get; set; }
        public bool CommunicationGanz28 { get; set; }
        public bool CommunicationGanz29 { get; set; }
        public bool CommunicationGanz30 { get; set; }
        public bool CommunicationGanz31 { get; set; }
        public bool CommunicationGanz32 { get; set; }
        public bool CommunicationGanz33 { get; set; }
        public bool CommunicationGanz34 { get; set; }
        public bool CommunicationGanz35 { get; set; }
        public bool CommunicationGanz36 { get; set; }
        public bool CommunicationGanz37 { get; set; }
        public bool CommunicationGanz38 { get; set; }
        public bool CommunicationGanz39 { get; set; }
        public bool CommunicationGanz40 { get; set; }
        public bool CommunicationGanz41 { get; set; }
        public bool CommunicationGanz42 { get; set; }
        public bool CommunicationGanz43 { get; set; }
        public bool CommunicationGanz44 { get; set; }
        public bool CommunicationGanz45 { get; set; }
        public bool CommunicationGanz46 { get; set; }
        public bool CommunicationGanz47 { get; set; }
        public bool CommunicationGanz48 { get; set; }
        public bool CommunicationGanz49 { get; set; }
        public bool CommunicationGanz50 { get; set; }
        public bool CommunicationGanz51 { get; set; }
        public bool CommunicationGanz52 { get; set; }
        public bool CommunicationGanz53 { get; set; }
        public bool CommunicationGanz54 { get; set; }
        public bool CommunicationGanz55 { get; set; }
        public bool CommunicationGanz56 { get; set; }
        public bool CommunicationGanz57 { get; set; }
        public bool CommunicationGanz58 { get; set; }
        public bool CommunicationGanz59 { get; set; }
        public bool CommunicationGanz60 { get; set; }
        public bool CommunicationGanz61 { get; set; }
        public bool CommunicationGanz62 { get; set; }
        public bool CommunicationGanz63 { get; set; }
        public bool CommunicationGanz64 { get; set; }
        public bool CommunicationGanz65 { get; set; }
        public bool CommunicationGanz66 { get; set; }
        public bool CommunicationGanz67 { get; set; }
        public bool CommunicationGanz68 { get; set; }
        public bool CommunicationGanz69 { get; set; }
        public bool CommunicationGanz70 { get; set; }
        public bool CommunicationGanz71 { get; set; }
        public bool CommunicationGanz72 { get; set; }
        public bool CommunicationGanz73 { get; set; }
        public bool CommunicationGanz74 { get; set; }
        public bool CommunicationGanz75 { get; set; }
        public bool CommunicationGanz76 { get; set; }
        public bool CommunicationGanz77 { get; set; }
        public bool CommunicationGanz78 { get; set; }
        public bool CommunicationGanz79 { get; set; }
        public bool CommunicationGanz80 { get; set; }
        public bool CommunicationGanz81 { get; set; }
        public bool CommunicationGanz82 { get; set; }
        public bool CommunicationGanz83 { get; set; }
        public bool CommunicationGanz84 { get; set; }
        public bool CommunicationGanz85 { get; set; }
        public bool CommunicationGanz86 { get; set; }
        public bool CommunicationGanz87 { get; set; }
        public bool CommunicationGanz88 { get; set; }
        public bool CommunicationGanz89 { get; set; }
        public bool CommunicationGanz90 { get; set; }
        public bool CommunicationGanz91 { get; set; }
        public bool CommunicationGanz92 { get; set; }
        public bool CommunicationGanz93 { get; set; }
        public bool CommunicationGanz94 { get; set; }
        public bool CommunicationGanz95 { get; set; }
        public bool CommunicationGanz96 { get; set; }
        public bool CommunicationGanz97 { get; set; }
        public bool CommunicationGanz98 { get; set; }
        public bool CommunicationGanz99 { get; set; }
        public bool CommunicationGanz100 { get; set; }

        // �n���i��ʉ�b�C�x���g
        public bool CommunicationHanna1 { get; set; }
        public bool CommunicationHanna2 { get; set; }
        public bool CommunicationHanna3 { get; set; }
        public bool CommunicationHanna4 { get; set; }
        public bool CommunicationHanna5 { get; set; }
        public bool CommunicationHanna6 { get; set; }
        public bool CommunicationHanna7 { get; set; }
        public bool CommunicationHanna8 { get; set; }
        public bool CommunicationHanna9 { get; set; }
        public bool CommunicationHanna10 { get; set; }
        public bool CommunicationHanna11 { get; set; }
        public bool CommunicationHanna12 { get; set; }
        public bool CommunicationHanna13 { get; set; }
        public bool CommunicationHanna14 { get; set; }
        public bool CommunicationHanna15 { get; set; }
        public bool CommunicationHanna16 { get; set; }
        public bool CommunicationHanna17 { get; set; }
        public bool CommunicationHanna18 { get; set; }
        public bool CommunicationHanna19 { get; set; }
        public bool CommunicationHanna20 { get; set; }
        public bool CommunicationHanna21 { get; set; }
        public bool CommunicationHanna22 { get; set; }
        public bool CommunicationHanna23 { get; set; }
        public bool CommunicationHanna24 { get; set; }
        public bool CommunicationHanna25 { get; set; }
        public bool CommunicationHanna26 { get; set; }
        public bool CommunicationHanna27 { get; set; }
        public bool CommunicationHanna28 { get; set; }
        public bool CommunicationHanna29 { get; set; }
        public bool CommunicationHanna30 { get; set; }
        public bool CommunicationHanna31 { get; set; }
        public bool CommunicationHanna32 { get; set; }
        public bool CommunicationHanna33 { get; set; }
        public bool CommunicationHanna34 { get; set; }
        public bool CommunicationHanna35 { get; set; }
        public bool CommunicationHanna36 { get; set; }
        public bool CommunicationHanna37 { get; set; }
        public bool CommunicationHanna38 { get; set; }
        public bool CommunicationHanna39 { get; set; }
        public bool CommunicationHanna40 { get; set; }
        public bool CommunicationHanna41 { get; set; }
        public bool CommunicationHanna42 { get; set; }
        public bool CommunicationHanna43 { get; set; }
        public bool CommunicationHanna44 { get; set; }
        public bool CommunicationHanna45 { get; set; }
        public bool CommunicationHanna46 { get; set; }
        public bool CommunicationHanna47 { get; set; }
        public bool CommunicationHanna48 { get; set; }
        public bool CommunicationHanna49 { get; set; }
        public bool CommunicationHanna50 { get; set; }
        public bool CommunicationHanna51 { get; set; }
        public bool CommunicationHanna52 { get; set; }
        public bool CommunicationHanna53 { get; set; }
        public bool CommunicationHanna54 { get; set; }
        public bool CommunicationHanna55 { get; set; }
        public bool CommunicationHanna56 { get; set; }
        public bool CommunicationHanna57 { get; set; }
        public bool CommunicationHanna58 { get; set; }
        public bool CommunicationHanna59 { get; set; }
        public bool CommunicationHanna60 { get; set; }
        public bool CommunicationHanna61 { get; set; }
        public bool CommunicationHanna62 { get; set; }
        public bool CommunicationHanna63 { get; set; }
        public bool CommunicationHanna64 { get; set; }
        public bool CommunicationHanna65 { get; set; }
        public bool CommunicationHanna66 { get; set; }
        public bool CommunicationHanna67 { get; set; }
        public bool CommunicationHanna68 { get; set; }
        public bool CommunicationHanna69 { get; set; }
        public bool CommunicationHanna70 { get; set; }
        public bool CommunicationHanna71 { get; set; }
        public bool CommunicationHanna72 { get; set; }
        public bool CommunicationHanna73 { get; set; }
        public bool CommunicationHanna74 { get; set; }
        public bool CommunicationHanna75 { get; set; }
        public bool CommunicationHanna76 { get; set; }
        public bool CommunicationHanna77 { get; set; }
        public bool CommunicationHanna78 { get; set; }
        public bool CommunicationHanna79 { get; set; }
        public bool CommunicationHanna80 { get; set; }
        public bool CommunicationHanna81 { get; set; }
        public bool CommunicationHanna82 { get; set; }
        public bool CommunicationHanna83 { get; set; }
        public bool CommunicationHanna84 { get; set; }
        public bool CommunicationHanna85 { get; set; }
        public bool CommunicationHanna86 { get; set; }
        public bool CommunicationHanna87 { get; set; }
        public bool CommunicationHanna88 { get; set; }
        public bool CommunicationHanna89 { get; set; }
        public bool CommunicationHanna90 { get; set; }
        public bool CommunicationHanna91 { get; set; }
        public bool CommunicationHanna92 { get; set; }
        public bool CommunicationHanna93 { get; set; }
        public bool CommunicationHanna94 { get; set; }
        public bool CommunicationHanna95 { get; set; }
        public bool CommunicationHanna96 { get; set; }
        public bool CommunicationHanna97 { get; set; }
        public bool CommunicationHanna98 { get; set; }
        public bool CommunicationHanna99 { get; set; }
        public bool CommunicationHanna100 { get; set; }
        #endregion

        public bool AlreadyLvUpEmpty11 { get; set; }
        public bool AlreadyLvUpEmpty12 { get; set; }
        public bool AlreadyLvUpEmpty13 { get; set; }
        public bool AlreadyLvUpEmpty14 { get; set; }
        public bool AlreadyLvUpEmpty15 { get; set; }
        public bool AlreadyLvUpEmpty21 { get; set; }
        public bool AlreadyLvUpEmpty22 { get; set; }
        public bool AlreadyLvUpEmpty23 { get; set; }
        public bool AlreadyLvUpEmpty24 { get; set; }
        public bool AlreadyLvUpEmpty25 { get; set; }
        public bool AlreadyLvUpEmpty31 { get; set; }
        public bool AlreadyLvUpEmpty32 { get; set; }
        public bool AlreadyLvUpEmpty33 { get; set; }
        public bool AlreadyLvUpEmpty34 { get; set; }
        public bool AlreadyLvUpEmpty35 { get; set; }

        public bool Treasure1 { get; set; } // �P�K�F�󔠂P
        public bool Treasure2 { get; set; } // �P�K�F�󔠂Q
        public bool Treasure3 { get; set; } // �P�K�F�󔠂R
        public bool Treasure4 { get; set; } // �Q�K�F�󔠂P
        public bool Treasure5 { get; set; } // �Q�K�F�󔠂Q
        public bool Treasure6 { get; set; } // �Q�K�F�󔠂R
        public bool Treasure7 { get; set; } // �Q�K�F�󔠂S
        public bool Treasure8 { get; set; } // �R�K�F�󔠂P
        public bool Treasure9 { get; set; } // �R�K�F�󔠂Q
        public bool Treasure10 { get; set; } // �R�K�F�󔠂R
        public bool Treasure11 { get; set; } // �R�K�F�󔠂S
        public bool Treasure12 { get; set; } // �R�K�F�󔠂T
        public bool Treasure121 { get; set; } // �R�K�F�󔠂U
        public bool Treasure122 { get; set; } // �R�K�F�󔠂V
        public bool Treasure123 { get; set; } // �R�K�F�󔠂W
        public bool Treasure41 { get; set; } // �S�K�F�󔠂P
        public bool Treasure42 { get; set; } // �S�K�F�󔠂Q
        public bool Treasure43 { get; set; } // �S�K�F�󔠂R
        public bool Treasure44 { get; set; } // �S�K�F�󔠂S
        public bool Treasure45 { get; set; } // �S�K�F�󔠂T
        public bool Treasure46 { get; set; } // �S�K�F�󔠂U
        public bool Treasure47 { get; set; } // �S�K�F�󔠂V
        public bool Treasure48 { get; set; } // �S�K�F�󔠂W
        public bool Treasure49 { get; set; } // �S�K�F�󔠂X
        public bool Treasure51 { get; set; } // �T�K�F�󔠂P
        public bool Treasure52 { get; set; } // �T�K�F�󔠂Q
        public bool Treasure53 { get; set; } // �T�K�F�󔠂R
        public bool Treasure54 { get; set; } // �T�K�F�󔠂S
        public bool Treasure55 { get; set; } // �T�K�F�󔠂T
        public bool Treasure56 { get; set; } // �T�K�F�󔠂U
        public bool Treasure57 { get; set; } // �T�K�F�󔠂V

        public int DungeonArea { get; set; } // �_���W�������K�ŃZ�[�u�������������l
        public bool CompleteSlayBoss1 { get; set; } // �_���W�����P�K�̃{�X�����j
        public bool CompleteSlayBoss2 { get; set; } // �_���W�����Q�K�̃{�X�����j
        public bool CompleteSlayBoss3 { get; set; } // �_���W�����R�K�̃{�X�����j
        public bool CompleteSlayBoss4 { get; set; } // �_���W�����S�K�̃{�X�����j
        public bool CompleteSlayBoss5 { get; set; } // �_���W�����T�K�̃{�X�����j
        public bool CompleteArea1 { get; set; } // �_���W�����P�K�𐧔e��
        public bool CompleteArea2 { get; set; } // �_���W�����Q�K�𐧔e��
        public bool CompleteArea3 { get; set; } // �_���W�����R�K�𐧔e��
        public bool CompleteArea4 { get; set; } // �_���W�����S�K�𐧔e��
        public bool CompleteArea5 { get; set; } // �_���W�����T�K�𐧔e��
        public int CompleteArea1Day { get; set; } // �_���W�����P�K���e������
        public int CompleteArea2Day { get; set; } // �_���W�����Q�K���e������
        public int CompleteArea3Day { get; set; } // �_���W�����R�K���e������
        public int CompleteArea4Day { get; set; } // �_���W�����S�K���e������
        public int CompleteArea5Day { get; set; } // �_���W�����T�K���e������
        public bool CommucationCompArea1 { get; set; } // �_���W�����P�K���e�ɂ�鋭����b�C�x���g
        public bool CommucationCompArea2 { get; set; } // �_���W�����Q�K���e�ɂ�鋭����b�C�x���g
        public bool CommucationCompArea3 { get; set; } // �_���W�����R�K���e�ɂ�鋭����b�C�x���g
        public bool CommucationCompArea4 { get; set; } // �_���W�����S�K���e�ɂ�鋭����b�C�x���g
        public bool CommucationCompArea5 { get; set; } // �_���W�����T�K���e�ɂ�鋭����b�C�x���g
        public bool CommucationFirstHomeTown { get; set; } // ���߂ăz�[���^�E���ɖ߂������̋�����b�C�x���g

        public bool TruthWord1 { get; set; } // �^���̌��t�P���{�����Ă��邩�ǂ���
        public bool TruthWord2 { get; set; } // �^���̌��t�Q���{�����Ă��邩�ǂ���
        public bool TruthWord3 { get; set; } // �^���̌��t�R���{�����Ă��邩�ǂ���
        public bool TruthWord4 { get; set; } // �^���̌��t�S���{�����Ă��邩�ǂ���
        public bool TruthWord5 { get; set; } // �^���̌��t�T���{�����Ă��邩�ǂ���
        public bool TrueEnding1 { get; set; } // �^�G���f�B���O�̂��߂̉�b�A�N���A�ςłP�K���e�Ŕ����\

        public bool InfoArea11 { get; set; } // �_���W�����P�K�̉B���ʘH������
        public bool InfoArea21 { get; set; } // �_���W�����Q�K�̏�񂻂̂P
        public bool InfoArea22 { get; set; } // �_���W�����Q�K�̏�񂻂̂Q
        public bool InfoArea221 { get; set; }// �_���W�����Q�K�̏�񂻂̂Q�|�P
        public bool InfoArea222 { get; set; }// �_���W�����Q�K�̏�񂻂̂Q�|�Q
        public bool InfoArea23 { get; set; } // �_���W�����Q�K�̏�񂻂̂R
        public bool InfoArea24 { get; set; } // �_���W�����Q�K�̏�񂻂̂S
        public bool InfoArea240 { get; set; } // �_���W�����Q�K�̏�񂻂̂S�|�O
        public bool InfoArea25 { get; set; } // �_���W�����Q�K�̏�񂻂̂T
        public bool InfoArea26 { get; set; } // �_���W�����Q�K�̏�񂻂̂U
        public bool SolveArea21 { get; set; } // �_���W�����Q�K�̃G���A���̂P�N���A
        public bool SolveArea22 { get; set; } // �_���W�����Q�K�̃G���A���̂Q�N���A
        public bool SolveArea221 { get; set; } // �_���W�����Q�K�̃G���A���̂Q�|�P�N���A
        public bool SolveArea222 { get; set; } // �_���W�����Q�K�̃G���A���̂Q�|�Q�N���A
        public bool SolveArea23 { get; set; } // �_���W�����Q�K�̃G���A���̂R�N���A
        public bool SolveArea24 { get; set; } // �_���W�����Q�K�̃G���A���̂S�N���A
        public bool SolveArea25 { get; set; } // �_���W�����Q�K�̃G���A���̂T�N���A
        public bool SolveArea26 { get; set; } // �_���W�����Q�K�̃G���A���̂U�N���A
        public bool FailArea221 { get; set; } // �_���W�����Q�K�̃G���A���̂Q�|�P���s�o������
        public bool FailArea222 { get; set; } // �_���W�����Q�K�̃G���A���̂Q�|�Q���s�o������
        public bool FailArea23 { get; set; } // �_���W�����Q�K�̃G���A���̂R���s�o������
        public bool FailArea24 { get; set; } // �_���W�����Q�K�̃G���A���̂S���s�o������
        public bool FailArea241 { get; set; } // �_���W�����Q�K�̃G���A���̂S���s�o���Q���
        public bool FailArea242 { get; set; } // �_���W�����Q�K�̃G���A���̂S���s�o���R���
        public bool FailArea26 { get; set; } // �_���W�����Q�K�̃G���A���̂U���s�o���P���
        public bool FailArea261 { get; set; } //  �_���W�����Q�K�̃G���A���̂U���s�o���Q���
        public bool FailArea262 { get; set; } //  �_���W�����Q�K�̃G���A���̂U���s�o���R���
        public bool FailArea263 { get; set; } //  �_���W�����Q�K�̃G���A���̂U���s�o���S���
        public bool FailArea264 { get; set; } //  �_���W�����Q�K�̃G���A���̂U���s�o���T���
        public bool ProgressArea241 { get; set; } // �_���W�����Q�K�̒ʉߌo�H�S�P
        public bool ProgressArea2412 { get; set; } // �_���W�����Q�K�̒ʉߌo�H�S�P
        public bool ProgressArea2413 { get; set; } // �_���W�����Q�K�̒ʉߌo�H�S�P
        public bool ProgressArea242 { get; set; } // �_���W�����Q�K�̒ʉߌo�H�S�Q
        public bool ProgressArea2422 { get; set; } // �_���W�����Q�K�̒ʉߌo�H�S�Q
        public bool ProgressArea243 { get; set; } // �_���W�����Q�K�̒ʉߌo�H�S�R
        public bool ProgressArea2432 { get; set; } // �_���W�����Q�K�̒ʉߌo�H�S�R
        public bool ProgressArea244 { get; set; } // �_���W�����Q�K�̒ʉߌo�H�S�S
        public bool ProgressArea2442 { get; set; } // �_���W�����Q�K�̒ʉߌo�H�S�S
        public bool ProgressArea245 { get; set; } // �_���W�����Q�K�̒ʉߌo�H�S�T
        public bool ProgressArea2452 { get; set; } // �_���W�����Q�K�̒ʉߌo�H�S�T
        public bool ProgressArea246 { get; set; } // �_���W�����Q�K�̒ʉߌo�H�S�U
        public bool ProgressArea26 { get; set; } // �_���W�����Q�K�̒ʉߌo�H�U
        public bool ProgressArea261 { get; set; } // �_���W�����Q�K�̒ʉߌo�H�U�P
        public bool ProgressArea262 { get; set; } // �_���W�����Q�K�̒ʉߌo�H�U�Q
        public bool ProgressArea263 { get; set; } // �_���W�����Q�K�̒ʉߌo�H�U�R
        public bool ProgressArea264 { get; set; } // �_���W�����Q�K�̒ʉߌo�H�U�S
        public bool ProgressArea265 { get; set; } // �_���W�����Q�K�̒ʉߌo�H�U�T
        public bool ProgressArea266 { get; set; } // �_���W�����Q�K�̒ʉߌo�H�U�U
        public bool ProgressArea267 { get; set; } // �_���W�����Q�K�̒ʉߌo�H�U�V
        public bool ProgressArea268 { get; set; } // �_���W�����Q�K�̒ʉߌo�H�U�W
        public bool ProgressArea269 { get; set; } // �_���W�����Q�K�̒ʉߌo�H�U�X
        public bool ProgressArea2610 { get; set; } // �_���W�����Q�K�̒ʉߌo�H�U�P�O
        public bool ProgressArea2611 { get; set; } // �_���W�����Q�K�̒ʉߌo�H�U�P�P
        public bool ProgressArea2612 { get; set; } // �_���W�����Q�K�̒ʉߌo�H�U�P�Q
        public bool ProgressArea2613 { get; set; } // �_���W�����Q�K�̒ʉߌo�H�U�P�R
        public bool ProgressArea2614 { get; set; } // �_���W�����Q�K�̒ʉߌo�H�U�P�S
        public bool ProgressArea2615 { get; set; } // �_���W�����Q�K�̒ʉߌo�H�U�P�T
        public bool ProgressArea2616 { get; set; } // �_���W�����Q�K�̒ʉߌo�H�U�P�U
        public bool FirstProcessArea24 { get; set; } // �_���W�����Q�K�̒ʉߌo�H�T���P���
        public bool CompleteArea21 { get; set; } // �_���W�����Q�K�̃G���A���̂P����
        public bool CompleteArea22 { get; set; } // �_���W�����Q�K�̃G���A���̂Q����
        public bool CompleteArea23 { get; set; } // �_���W�����Q�K�̃G���A���̂R����
        public bool CompleteArea24 { get; set; } // �_���W�����Q�K�̃G���A���̂S����
        public bool CompleteArea25 { get; set; } // �_���W�����Q�K�̃G���A���̂T����
        public bool CompleteArea26 { get; set; } // �_���W�����Q�K�̃G���A���̂U����
        public bool InfoArea27 { get; set; } // �_���W�����Q�K�̉B���ʘH������
        public bool InfoArea31 { get; set; } // �_���W�����R�K�̃��[�v���P
        public bool InfoArea311s { get; set; }  // �_���W�����R�K�̃��[�v�J�n�|�C���g���P�P
        public bool InfoArea311e { get; set; }  // �_���W�����R�K�̃��[�v�I���|�C���g���P�P
        public bool InfoArea312s { get; set; }  // �_���W�����R�K�̃��[�v�J�n�|�C���g���P�Q
        public bool InfoArea312e { get; set; }  // �_���W�����R�K�̃��[�v�I���|�C���g���P�Q
        public bool InfoArea313s { get; set; }  // �_���W�����R�K�̃��[�v�J�n�|�C���g���P�R
        public bool InfoArea313e { get; set; }  // �_���W�����R�K�̃��[�v�I���|�C���g���P�R
        public bool InfoArea324s { get; set; }  // �_���W�����R�K�̃��[�v�J�n�|�C���g���Q�S
        public bool InfoArea324e { get; set; }  // �_���W�����R�K�̃��[�v�I���|�C���g���Q�S
        public bool InfoArea325s { get; set; }  // �_���W�����R�K�̃��[�v�J�n�|�C���g���Q�T
        public bool InfoArea325e { get; set; }  // �_���W�����R�K�̃��[�v�I���|�C���g���Q�T
        public bool InfoArea326s { get; set; }  // �_���W�����R�K�̃��[�v�J�n�|�C���g���Q�U
        public bool InfoArea326e { get; set; }  // �_���W�����R�K�̃��[�v�I���|�C���g���Q�U
        public bool InfoArea327s { get; set; }  // �_���W�����R�K�̃��[�v�J�n�|�C���g���Q�V
        public bool InfoArea327e { get; set; }  // �_���W�����R�K�̃��[�v�I���|�C���g���Q�V
        public bool InfoArea328s { get; set; }  // �_���W�����R�K�̃��[�v�J�n�|�C���g���Q�W
        public bool InfoArea328e { get; set; }  // �_���W�����R�K�̃��[�v�I���|�C���g���Q�W
        public bool InfoArea329s { get; set; }  // �_���W�����R�K�̃��[�v�J�n�|�C���g���Q�X
        public bool InfoArea329e { get; set; }  // �_���W�����R�K�̃��[�v�I���|�C���g���Q�X
        public bool InfoArea3210s { get; set; } // �_���W�����R�K�̃��[�v�J�n�|�C���g���Q�P�O
        public bool InfoArea3210e { get; set; } // �_���W�����R�K�̃��[�v�I���|�C���g���Q�P�O
        public bool InfoArea3211s { get; set; } // �_���W�����R�K�̃��[�v�J�n�|�C���g���Q�P�P
        public bool InfoArea3211e { get; set; } // �_���W�����R�K�̃��[�v�I���|�C���g���Q�P�P
        public bool InfoArea3212s { get; set; } // �_���W�����R�K�̃��[�v�J�n�|�C���g���Q�P�Q
        public bool InfoArea3212e { get; set; } // �_���W�����R�K�̃��[�v�I���|�C���g���Q�P�Q
        public bool InfoArea3213s { get; set; } // �_���W�����R�K�̃��[�v�J�n�|�C���g���Q�P�R
        public bool InfoArea3213e { get; set; } // �_���W�����R�K�̃��[�v�I���|�C���g���Q�P�R
        public bool InfoArea3214s { get; set; } // �_���W�����R�K�̃��[�v�J�n�|�C���g���Q�P�S
        public bool InfoArea3214e { get; set; } // �_���W�����R�K�̃��[�v�I���|�C���g���Q�P�S
        public bool FailArea321 { get; set; } // �_���W�����R�K�̃��[�v���֖厸�s�P
        public bool FailArea322 { get; set; } // �_���W�����R�K�̃��[�v���֖厸�s�Q
        public bool FailArea323 { get; set; } // �_���W�����R�K�̃��[�v���֖厸�s�R
        public bool FailArea3241 { get; set; } // �_���W�����R�K�̃��[�v���֖厸�s�S�P
        public bool FailArea3242 { get; set; } // �_���W�����R�K�̃��[�v���֖厸�s�S�Q
        public bool FailArea3243 { get; set; } // �_���W�����R�K�̃��[�v���֖厸�s�S�R
        public bool CompleteArea32 { get; set; } // �_���W�����R�K�̃��[�v�S���B�p�Ŕ�
        public bool InfoArea3315s { get; set; } // �_���W�����R�K�̃��[�v�J�n�|�C���g�R�P�T
        public bool InfoArea3315e { get; set; } // �_���W�����R�K�̃��[�v�I���|�C���g�R�P�T
        public bool InfoArea3316s { get; set; } // �_���W�����R�K�̃��[�v�J�n�|�C���g�R�P�U
        public bool InfoArea3316e { get; set; } // �_���W�����R�K�̃��[�v�I���|�C���g�R�P�U
        public bool InfoArea3317s { get; set; } // �_���W�����R�K�̃��[�v�J�n�|�C���g�R�P�V
        public bool InfoArea3317e { get; set; } // �_���W�����R�K�̃��[�v�I���|�C���g�R�P�V
        public bool InfoArea3318s { get; set; } // �_���W�����R�K�̃��[�v�J�n�|�C���g�R�P�W
        public bool InfoArea3318e { get; set; } // �_���W�����R�K�̃��[�v�I���|�C���g�R�P�W
        public bool InfoArea3319s { get; set; } // �_���W�����R�K�̃��[�v�J�n�|�C���g�R�P�X
        public bool InfoArea3319e { get; set; } // �_���W�����R�K�̃��[�v�I���|�C���g�R�P�X
        public bool InfoArea3320s { get; set; } // �_���W�����R�K�̃��[�v�J�n�|�C���g�R�Q�O
        public bool InfoArea3320e { get; set; } // �_���W�����R�K�̃��[�v�I���|�C���g�R�Q�O
        public bool InfoArea3321s { get; set; } // �_���W�����R�K�̃��[�v�J�n�|�C���g�R�Q�P
        public bool InfoArea3321e { get; set; } // �_���W�����R�K�̃��[�v�I���|�C���g�R�Q�P
        public bool InfoArea3322s { get; set; } // �_���W�����R�K�̃��[�v�J�n�|�C���g�R�Q�Q
        public bool InfoArea3322e { get; set; } // �_���W�����R�K�̃��[�v�I���|�C���g�R�Q�Q
        public bool InfoArea3323s { get; set; } // �_���W�����R�K�̃��[�v�J�n�|�C���g�R�Q�R
        public bool InfoArea3323e { get; set; } // �_���W�����R�K�̃��[�v�I���|�C���g�R�Q�R
        public bool InfoArea3324s { get; set; } // �_���W�����R�K�̃��[�v�J�n�|�C���g�R�Q�S
        public bool InfoArea3324e { get; set; } // �_���W�����R�K�̃��[�v�I���|�C���g�R�Q�S
        public bool InfoArea3325s { get; set; } // �_���W�����R�K�̃��[�v�J�n�|�C���g�R�Q�T
        public bool InfoArea3325e { get; set; } // �_���W�����R�K�̃��[�v�I���|�C���g�R�Q�T
        public bool InfoArea3326s { get; set; } // �_���W�����R�K�̃��[�v�J�n�|�C���g�R�Q�U
        public bool InfoArea3326e { get; set; } // �_���W�����R�K�̃��[�v�I���|�C���g�R�Q�U
        public bool InfoArea3327s { get; set; } // �_���W�����R�K�̃��[�v�J�n�|�C���g�R�Q�V
        public bool InfoArea3327e { get; set; } // �_���W�����R�K�̃��[�v�I���|�C���g�R�Q�V
        public bool InfoArea3328s { get; set; } // �_���W�����R�K�̃��[�v�J�n�|�C���g�R�Q�W
        public bool InfoArea3328e { get; set; } // �_���W�����R�K�̃��[�v�I���|�C���g�R�Q�W
        public bool ProgressArea3316 { get; set; } // �_���W�����R�K�̃��[�v�ʉߌo�H�R�P�U
        public bool ProgressArea3317 { get; set; } // �_���W�����R�K�̃��[�v�ʉߌo�H�R�P�V
        public bool ProgressArea3318 { get; set; } // �_���W�����R�K�̃��[�v�ʉߌo�H�R�P�W
        public bool ProgressArea3319 { get; set; } // �_���W�����R�K�̃��[�v�ʉߌo�H�R�P�X
        public bool ProgressArea3320 { get; set; } // �_���W�����R�K�̃��[�v�ʉߌo�H�R�Q�O
        public bool ProgressArea3321 { get; set; } // �_���W�����R�K�̃��[�v�ʉߌo�H�R�Q�P
        public bool ProgressArea3322 { get; set; } // �_���W�����R�K�̃��[�v�ʉߌo�H�R�Q�Q
        public bool ProgressArea3323 { get; set; } // �_���W�����R�K�̃��[�v�ʉߌo�H�R�Q�R
        public bool ProgressArea3324 { get; set; } // �_���W�����R�K�̃��[�v�ʉߌo�H�R�Q�S
        public bool ProgressArea3325 { get; set; } // �_���W�����R�K�̃��[�v�ʉߌo�H�R�Q�T
        public bool ProgressArea3326 { get; set; } // �_���W�����R�K�̃��[�v�ʉߌo�H�R�Q�U
        public bool ProgressArea3327 { get; set; } // �_���W�����R�K�̃��[�v�ʉߌo�H�R�Q�V
        public bool FailArea331 { get; set; } // �_���W�����R�K�̃��[�v��O�֖厸�s�P���
        public bool FailArea332 { get; set; } // �_���W�����R�K�̃��[�v��O�֖厸�s�Q���
        public bool FailArea333 { get; set; } // �_���W�����R�K�̃��[�v��O�֖厸�s�R���
        public bool FailArea334 { get; set; } // �_���W�����R�K�̃��[�v��O�֖厸�s�S���
        public bool FailArea335 { get; set; } // �_���W�����R�K�̃��[�v��O�֖厸�s�T���
        public bool FailArea336 { get; set; } // �_���W�����R�K�̃��[�v��O�֖厸�s�U���
        public bool FailArea337 { get; set; } // �_���W�����R�K�̃��[�v��O�֖厸�s�V���
        public bool CompleteArea33 { get; set; } // �_���W�����R�K�̃��[�v��O�֖听��
        public bool InfoArea34 { get; set; } // �_���W�����R�K�̃��[�v��S�֖���
        public bool SolveArea34 { get; set; } // �_���W�����R�K�̃��[�v��S�֖����
        public bool CompleteArea34 { get; set; } // �_���W�����R�K�̃��[�v��l�֖�I��
        public bool CompleteJumpArea34 { get; set; } // �_���W�����R�K�̃��[�v��l�֖�W�����v
        public bool InfoArea35 { get; set; } // �_���W�����R�K�̉B���ʘH������
        public bool InfoArea41 { get; set; } // �_���W�����S�K�̉�b�P
        public bool InfoArea42 { get; set; } // �_���W�����S�K�̉�b�Q
        public bool InfoArea43 { get; set; } // �_���W�����S�K�̉�b�R
        public bool InfoArea44 { get; set; } // �_���W�����S�K�̉�b�S
        public bool InfoArea45 { get; set; } // �_���W�����S�K�̉�b�T
        public bool InfoArea46 { get; set; } // �_���W�����S�K�̉�b�U
        public bool InfoArea47 { get; set; } // �_���W�����S�K�̉�b�V
        public bool InfoArea48 { get; set; } // �_���W�����S�K�̉�b�W
        public bool InfoArea49 { get; set; } // �_���W�����S�K�̉�b�X
        public bool InfoArea410 { get; set; } // �_���W�����S�K�̉�b�P�O
        public bool InfoArea411 { get; set; } // �_���W�����S�K�̉�b�P�P
        public bool InfoArea412 { get; set; } // �_���W�����S�K�̉�b�P�Q
        public bool InfoArea413 { get; set; } // �_���W�����S�K�̉�b�P�R
        public bool InfoArea414 { get; set; } // �_���W�����S�K�̉�b�P�S
        public bool InfoArea415 { get; set; } // �_���W�����S�K�̉�b�P�T
        public bool InfoArea416 { get; set; } // �_���W�����S�K�̉�b�P�U
        public bool InfoArea417 { get; set; } // �_���W�����S�K�̉�b�P�V
        public bool InfoArea418 { get; set; } // �_���W�����S�K�̉�b�P�W
        public bool InfoArea419 { get; set; } // �_���W�����S�K�̉�b�P�X
        public bool InfoArea420 { get; set; } // �_���W�����S�K�̉�b�Q�O
        public bool ProgressArea4211 { get; set; } // �_���W�����S�K�̋ߓ��I���ɂ�鎸�s�o�߂P
        public bool ProgressArea4212 { get; set; } // �_���W�����S�K�̋ߓ��I���ɂ�鎸�s�o�߂Q
        public bool FailArea4211 { get; set; } // �_���W�����S�K�̋ߓ��I���ɂ�鎸�s�P
        public bool FailArea4212 { get; set; } // �_���W�����S�K�̋ߓ��I���ɂ�鎸�s�Q
        public bool InfoArea51 { get; set; } // �_���W�����T�K���B���A�������߂�
        public bool InfoArea52 { get; set; } // �_���W�����T�K���X�{�X���O��b
        public bool InfoArea53 { get; set; } // �_���W�����T�K�B���ʘH������

        public bool SpecialInfo1 { get; set; } // �Q���ړ˓���A�s�q�t�d�d�m�c�g���K�[�ƂȂ�X�y�V�������P
        public bool SpecialInfo2 { get; set; } // �Q���ړ˓���A�s�q�t�d�d�m�c�g���K�[�ƂȂ�X�y�V�������Q
        public bool SpecialInfo3 { get; set; } // �Q���ړ˓���A�s�q�t�d�d�m�c�g���K�[�ƂȂ�X�y�V�������R
        public bool SpecialInfo4 { get; set; } // �Q���ړ˓���A�s�q�t�d�d�m�c�g���K�[�ƂȂ�X�y�V�������S
        public bool DefeatVerze { get; set; } // �Q���ړ˓���A���F���[�����j���Ă��鎖�ɂ��^�E���X�{�X�����̃t���O
        public bool SpecialTreasure1 { get; set; } // �Q���ړ˓���̃X�y�V�����A�C�e������t���O
        public bool TruthEventForLana { get; set; } // �_���W�����T�K�A�u���i�̃C�������O�v���擾��
        public bool EnterSecondGame { get; set; } // �Q���ړ˓��t���O

        public bool AlreadyUseSyperSaintWater { get; set; }
        public bool AlreadyUseRevivePotion { get; set; }
        public bool AlreadyUsePureWater { get; set; } // ��Ғǉ�

        // s ��Ғǉ�
        public int Version
        {
            get { return version; }
            set { version = value; }
        }
        // e ��Ғǉ�

        // s ��Ғǉ�
        public bool AlreadyShownEvent
        {
            get { return alreadyShownEvent; }
            set { alreadyShownEvent = value; }
        }
        // e ��Ғǉ�

        public int GameDay
        {
            get { return gameDay; }
            set { gameDay = value; }
        }
        public bool SaveByDungeon
        {
            get { return saveByDungeon; }
            set { saveByDungeon = value; }
        }
        public int DungeonPosX
        {
            get { return dungeonPosX; }
            set { dungeonPosX = value; }
        }
        public int DungeonPosY
        {
            get { return dungeonPosY; }
            set { dungeonPosY = value; }
        }
        public int DungeonPosX2
        {
            get { return dungeonPosX2; }
            set { dungeonPosX2 = value; }
        }
        public int DungeonPosY2
        {
            get { return dungeonPosY2; }
            set { dungeonPosY2 = value; }
        }
        public bool AlreadyCommunicate
        {
            get { return alreadyCommunicate; }
            set { alreadyCommunicate = value; }
        }
        public bool AlreadyRest
        {
            get { return alreadyRest; }
            set { alreadyRest = value; }
        }
        public bool AlreadyEquipShop
        {
            get { return alreadyEquipShop; }
            set { alreadyEquipShop = value; }
        }
        public bool OneDeny
        {
            get { return oneDeny; }
            set { oneDeny = value; }
        }
        public bool CommunicationFirstContact2
        {
            get { return communicationFirstContact2; }
            set { communicationFirstContact2 = value; }
        }
        public bool CommunicationSuccess2
        {
            get { return communicationSuccess2; }
            set { communicationSuccess2 = value; }
        }
                
        
        public bool AvailableEquipShop
        {
            get { return availableEquipShop; }
            set { availableEquipShop = value; }
        }
        public bool AvailableEquipShop2
        {
            get { return availableEquipShop2; }
            set { availableEquipShop2 = value; }
        }
        public bool AvailableEquipShop3
        {
            get { return availableEquipShop3; }
            set { availableEquipShop3 = value; }
        }
        public bool AvailableEquipShop4
        {
            get { return availableEquipShop4; }
            set { availableEquipShop4 = value; }
        }
        public bool AvailableEquipShop5
        {
            get { return availableEquipShop5; }
            set { availableEquipShop5 = value; }
        }

        public bool AvailableFirstCharacter
        {
            get { return availableFirstCharacter; }
            set { availableFirstCharacter = value; }
        }
        public bool AvailableSecondCharacter
        {
            get { return availableSecondCharacter; }
            set { availableSecondCharacter = value; }
        }
        public bool AvailableThirdCharacter
        {
            get { return availableThirdCharacter; }
            set { availableThirdCharacter = value; }
        }
        
        public bool CommunicationThirdChara1
        {
            get { return communicationThirdChara1; }
            set { communicationThirdChara1 = value; }
        }

        public bool CommunicationEnterFourArea
        {
            get { return communicationEnterFourArea; }
            set { communicationEnterFourArea = value; }
        }
        public bool AvailableItemSort
        {
            get { return availableItemSort; }
            set { availableItemSort = value; }
        }
        

        #endregion

        #region "���"

        public bool AlreadyDuelComplete { get; set; }

        public bool AlreadyGetOneDayItem { get; set; }
        public bool AlreadyGetMonsterHunt { get; set; }

        #region "�O�҂̏��������p�����t���O"
        public bool BeforeSpecialInfo1 { get; set; }
        public bool BeforeSpecialInfo2 { get; set; }
        public bool BeforeSpecialInfo3 { get; set; }
        public bool BeforeSpecialInfo4 { get; set; }
        public bool BeforeSpecialInfo5 { get; set; }    
        #endregion

        #region "��Ґ^�E�G���f�B���O��p�t���O"
        public bool CompleteTruth1 { get; set; }
        #endregion

        public bool TruthDuelMatch1 { get; set; } // DUEL�}�b�`1�l��
        public bool TruthDuelMatch2 { get; set; } // DUEL�}�b�`2�l��
        public bool TruthDuelMatch3 { get; set; } // DUEL�}�b�`3�l��
        public bool TruthDuelMatch4 { get; set; } // DUEL�}�b�`4�l��
        public bool TruthDuelMatch5 { get; set; } // DUEL�}�b�`5�l��
        public bool TruthDuelMatch6 { get; set; } // DUEL�}�b�`6�l��
        public bool TruthDuelMatch7 { get; set; } // DUEL�}�b�`7�l��
        public bool TruthDuelMatch8 { get; set; } // DUEL�}�b�`8�l��
        public bool TruthDuelMatch9 { get; set; } // DUEL�}�b�`9�l��
        public bool TruthDuelMatch10 { get; set; } // DUEL�}�b�`10�l��
        public bool TruthDuelMatch11 { get; set; } // DUEL�}�b�`11�l��
        public bool TruthDuelMatch12 { get; set; } // DUEL�}�b�`12�l��
        public bool TruthDuelMatch13 { get; set; } // DUEL�}�b�`13�l��
        public bool TruthDuelMatch14 { get; set; } // DUEL�}�b�`14�l��
        public bool TruthDuelMatch15 { get; set; } // DUEL�}�b�`15�l��
        public bool TruthDuelMatch16 { get; set; } // DUEL�}�b�`16�l��
        public bool TruthDuelMatch17 { get; set; } // DUEL�}�b�`17�l��
        public bool TruthDuelMatch18 { get; set; } // DUEL�}�b�`18�l��
        public bool TruthDuelMatch19 { get; set; } // DUEL�}�b�`19�l��
        public bool TruthDuelMatch20 { get; set; } // DUEL�}�b�`20�l��
        public bool TruthDuelMatch21 { get; set; } // DUEL�}�b�`21�l��

        public bool TruthCompleteSlayBoss1 { get; set; } // �_���W�����P�K�̃{�X�����j
        public bool TruthCompleteSlayBoss2 { get; set; } // �_���W�����Q�K�̃{�X�����j
        public bool TruthCompleteSlayBoss3 { get; set; } // �_���W�����R�K�̃{�X�����j
        public bool TruthCompleteSlayBoss4 { get; set; } // �_���W�����S�K�̃{�X�����j
        public bool TruthCompleteSlayBoss5 { get; set; } // �_���W�����T�K�̃{�X�����j
        public bool TruthCompleteArea1 { get; set; } // �_���W�����P�K�𐧔e��
        public bool TruthCompleteArea2 { get; set; } // �_���W�����Q�K�𐧔e��
        public bool TruthCompleteArea3 { get; set; } // �_���W�����R�K�𐧔e��
        public bool TruthCompleteArea4 { get; set; } // �_���W�����S�K�𐧔e��
        public bool TruthCompleteArea5 { get; set; } // �_���W�����T�K�𐧔e��
        public int TruthCompleteArea1Day { get; set; } // �_���W�����P�K���e������
        public int TruthCompleteArea2Day { get; set; } // �_���W�����Q�K���e������
        public int TruthCompleteArea3Day { get; set; } // �_���W�����R�K���e������
        public int TruthCompleteArea4Day { get; set; } // �_���W�����S�K���e������
        public int TruthCompleteArea5Day { get; set; } // �_���W�����T�K���e������
        public bool TruthCommunicationCompArea1 { get; set; }  // �_���W�����P�K���e�ɂ�鋭����b�C�x���g
        public bool TruthCommunicationCompArea2 { get; set; }  // �_���W�����Q�K���e�ɂ�鋭����b�C�x���g
        public bool TruthCommunicationCompArea3 { get; set; }  // �_���W�����R�K���e�ɂ�鋭����b�C�x���g
        public bool TruthCommunicationCompArea4 { get; set; }  // �_���W�����S�K���e�ɂ�鋭����b�C�x���g
        public bool TruthCommunicationCompArea5 { get; set; }  // �_���W�����T�K���e�ɂ�鋭����b�C�x���g

        #region "�S�K"
        public bool dungeonEvent489 { get; set; }
        public bool dungeonEvent488 { get; set; }
        public bool dungeonEvent487 { get; set; }
        public bool dungeonEvent486 { get; set; }
        public bool dungeonEvent485 { get; set; }
        public bool dungeonEvent4_key42_1 { get; set; }
        public bool dungeonEvent4_key42_2 { get; set; }
        public bool dungeonEvent4_key42_3 { get; set; }
        public bool dungeonEvent4_key42_4 { get; set; }
        public bool dungeonEvent4_key42_5 { get; set; }
        public bool dungeonEvent4_key42_6 { get; set; }
        public bool dungeonEvent4_key42_7 { get; set; }
        public bool dungeonEvent4_key42_8 { get; set; }
        public bool dungeonEvent484 { get; set; }
        public bool dungeonEvent483 { get; set; }
        public bool dungeonEvent482 { get; set; }
        public bool dungeonEvent481 { get; set; }
        public bool dungeonEvent480 { get; set; }
        public bool dungeonEvent4_key41_1 { get; set; }
        public bool dungeonEvent4_key41_2 { get; set; }
        public bool dungeonEvent4_key41_3 { get; set; }
        public bool dungeonEvent4_key41_4 { get; set; }
        public bool dungeonEvent4_key41_5 { get; set; }
        public bool dungeonEvent479 { get; set; }
        public bool dungeonEvent478 { get; set; }
        public bool dungeonEvent477 { get; set; }
        public bool dungeonEvent476 { get; set; }
        public bool dungeonEvent4_SlayBoss3 { get; set; }
        public bool dungeonEvent475 { get; set; }
        public bool dungeonEvent474 { get; set; }
        public bool dungeonEvent473 { get; set; }
        public bool dungeonEvent472 { get; set; }
        public bool dungeonEvent4_Area3_2_Fail { get; set; }
        public bool dungeonEvent471 { get; set; }
        public bool dungeonEvent470 { get; set; }
        public bool dungeonEvent469 { get; set; }
        public bool dungeonEvent468 { get; set; }
        public bool dungeonEvent467 { get; set; }
        public bool dungeonEvent466 { get; set; }
        public bool dungeonEvent465 { get; set; }
        public bool dungeonEvent464 { get; set; }
        public bool dungeonEvent463 { get; set; }
        public bool dungeonEvent462 { get; set; }
        public bool dungeonEvent461_storyok { get; set; }
        public bool dungeonEvent460 { get; set; }
        public bool dungeonEvent459 { get; set; }
        public bool dungeonEvent458 { get; set; }
        public bool dungeonEvent457 { get; set; }
        public bool dungeonEvent4_Area3_1_Fail_5 { get; set; }
        public bool dungeonEvent4_Area3_1_Fail_4 { get; set; }
        public bool dungeonEvent4_Area3_1_Fail_3 { get; set; }
        public bool dungeonEvent4_Area3_1_Fail_2 { get; set; }
        public bool dungeonEvent4_Area3_1_Fail { get; set; }
        public bool dungeonEvent456 { get; set; }
        public bool dungeonEvent455 { get; set; }
        public bool dungeonEvent454 { get; set; }
        public bool dungeonEvent453 { get; set; }
        public bool dungeonEvent452 { get; set; }
        public bool dungeonEvent451 { get; set; }
        public bool dungeonEvent450 { get; set; }
        public bool dungeonEvent449 { get; set; }
        public bool dungeonEvent448 { get; set; }
        public bool dungeonEvent447 { get; set; }
        public bool dungeonEvent446 { get; set; }
        public bool dungeonEvent445 { get; set; }
        public bool dungeonEvent444_storyok { get; set; }
        public bool dungeonEvent443 { get; set; }
        public bool dungeonEvent442 { get; set; }
        public bool dungeonEvent441 { get; set; }
        public bool dungeonEvent440 { get; set; }
        public bool dungeonEvent4_SlayBoss2 { get; set; }
        public bool dungeonEvent439 { get; set; }
        public bool dungeonEvent438 { get; set; }
        public bool dungeonEvent437 { get; set; }
        public bool dungeonEvent436 { get; set; }
        public bool dungeonEvent4_key23_1 { get; set; }
        public bool dungeonEvent4_key23_2 { get; set; }
        public bool dungeonEvent4_key23_3 { get; set; }
        public bool dungeonEvent4_key23_4 { get; set; }
        public bool dungeonEvent4_key23_5 { get; set; }
        public bool dungeonEvent435 { get; set; }
        public bool dungeonEvent434 { get; set; }
        public bool dungeonEvent433 { get; set; }
        public bool dungeonEvent432 { get; set; }
        public bool dungeonEvent4_key22_1 { get; set; }
        public bool dungeonEvent4_key22_2 { get; set; }
        public bool dungeonEvent4_key22_3 { get; set; }
        public bool dungeonEvent4_key22_4 { get; set; }
        public bool dungeonEvent4_key22_5 { get; set; }
        public bool dungeonEvent431 { get; set; }
        public bool dungeonEvent430 { get; set; }
        public bool dungeonEvent429 { get; set; }
        public bool dungeonEvent428 { get; set; }
        public bool dungeonEvent4_key2_1 { get; set; }
        public bool dungeonEvent4_key2_2 { get; set; }
        public bool dungeonEvent4_key2_3 { get; set; }
        public bool dungeonEvent4_key2_4 { get; set; }
        public bool dungeonEvent4_key2_5 { get; set; }
        public bool dungeonEvent427 { get; set; }
        public bool dungeonEvent426 { get; set; }
        public bool dungeonEvent425 { get; set; }
        public bool dungeonEvent4_key1_1 { get; set; }
        public bool dungeonEvent4_key1_1_open { get; set; }
        public bool dungeonEvent4_key1_2 { get; set; }
        public bool dungeonEvent4_key1_2_open { get; set; }
        public bool dungeonEvent4_key1_3 { get; set; }
        public bool dungeonEvent4_key1_3_open { get; set; }
        public bool dungeonEvent4_key1_4 { get; set; }
        public bool dungeonEvent4_key1_4_open { get; set; }
        public bool dungeonEvent4_key1_5 { get; set; }
        public bool dungeonEvent4_key1_5_open { get; set; }
        public bool dungeonEvent4_key1_6 { get; set; }
        public bool dungeonEvent4_key1_6_open { get; set; }
        public bool dungeonEvent4_key1_7 { get; set; }
        public bool dungeonEvent4_key1_7_open { get; set; }
        public bool dungeonEvent4_key1_8 { get; set; }
        public bool dungeonEvent4_key1_8_open { get; set; }
        public bool dungeonEvent4_key1_9 { get; set; }
        public bool dungeonEvent4_key1_9_open { get; set; }
        public bool dungeonEvent4_SlayBoss1 { get; set; }
        public bool dungeonEvent4_CommunicateBoss1 { get; set; }
        public bool dungeonEvent424 { get; set; }
        public bool dungeonEvent423 { get; set; }
        public bool dungeonEvent422 { get; set; }
        public bool dungeonEvent421 { get; set; }
        public bool dungeonEvent420 { get; set; }
        public bool dungeonEvent419 { get; set; }
        public bool dungeonEvent418 { get; set; }
        public bool dungeonEvent417 { get; set; }
        public bool dungeonEvent416 { get; set; }
        public bool dungeonEvent415 { get; set; }
        public bool dungeonEvent414 { get; set; }
        public bool dungeonEvent413 { get; set; }
        public bool dungeonEvent412 { get; set; }
        public bool dungeonEvent411 { get; set; }
        public bool dungeonEvent410 { get; set; }
        public bool dungeonEvent409 { get; set; }
        public bool dungeonEvent408 { get; set; }
        public bool dungeonEvent407 { get; set; }
        public bool dungeonEvent406 { get; set; }
        public bool dungeonEvent405 { get; set; }
        public bool dungeonEvent404 { get; set; }
        public bool dungeonEvent403 { get; set; }
        public bool dungeonEvent402 { get; set; }
        public bool dungeonEvent401 { get; set; }
        #endregion
        #region "�R�K"
        public bool dungeonEvent333 { get; set; }
        public bool dungeonEvent332 { get; set; }
        public bool dungeonEvent332_1 { get; set; }

        public bool dungeonEvent331 { get; set; }

        public bool dungeonEvent330 { get; set; }

        public bool dungeonEvent329 { get; set; }

        public bool dungeonEvent328 { get; set; }

        public bool dungeonEvent327 { get; set; }

        public bool dungeonEvent326 { get; set; }

        public bool dungeonEvent325 { get; set; }
        public bool dungeonEvent324 { get; set; }
        public bool dungeonEvent323 { get; set; }
        public bool dungeonEvent322 { get; set; }
        public bool dungeonEvent321 { get; set; }
        public bool dungeonEvent320 { get; set; }

        public bool dungeonEvent3_SlayBoss { get; set; }

        public bool dungeonEvent319KeyOpen { get; set; }
        public bool dungeonEvent319 { get; set; }

        public bool dungeonEvent318 { get; set; }
        public bool dungeonEvent317_2 { get; set; }
        public bool dungeonEvent317 { get; set; }
        public bool dungeonEvent316_2 { get; set; }
        public bool dungeonEvent316 { get; set; }
        public bool dungeonEvent315_2 { get; set; }
        public bool dungeonEvent315 { get; set; }
        public bool dungeonEvent314_2 { get; set; }
        public bool dungeonEvent314 { get; set; }

        public bool dungeonEvent313 { get; set; }
        public bool dungeonEvent312_2 { get; set; }
        public bool dungeonEvent312 { get; set; }
        public bool dungeonEvent311 { get; set; }
        public bool dungeonEvent310 { get; set; }
        public bool dungeonEvent309 { get; set; }
        public bool dungeonEvent308 { get; set; }
        public bool dungeonEvent307 { get; set; }

        public bool dungeonEvent306 { get; set; }
        public bool dungeonEvent305 { get; set; }
        public bool dungeonEvent304_6 { get; set; }
        public bool dungeonEvent304_5 { get; set; }
        public bool dungeonEvent304_4 { get; set; }
        public bool dungeonEvent304_3 { get; set; }
        public bool dungeonEvent304_2 { get; set; }
        public bool dungeonEvent304_1 { get; set; }
        public bool dungeonEvent303 { get; set; }
        public bool dungeonEvent302_3 { get; set; }
        public bool dungeonEvent302_2 { get; set; }
        public bool dungeonEvent302_1 { get; set; }
        public bool dungeonEvent302 { get; set; }
        public bool dungeonEvent301 { get; set; }
        #endregion
        #region "�Q�K"
        public bool dungeonEvent263_KeyOpen { get; set; }
        public bool dungeonEvent262 { get; set; }
        public bool dungeonEvent261 { get; set; }
        public bool dungeonEvent260 { get; set; }
        public bool dungeonEvent259 { get; set; }
        public bool dungeonEvent258 { get; set; }
        public bool dungeonEvent257 { get; set; }
        public bool dungeonEvent256 { get; set; }

        public bool dungeonEvent255_SlayBoss { get; set; }
        public bool dungeonEvent255 { get; set; }
        public bool dungeonEvent254_SlayBoss { get; set; }
        public bool dungeonEvent254 { get; set; }
        public bool dungeonEvent253_SlayBoss { get; set; }
        public bool dungeonEvent253 { get; set; }
        public bool dungeonEvent252_SlayBoss { get; set; }
        public bool dungeonEvent252 { get; set; }
        public bool dungeonEvent251_SlayBoss { get; set; }
        public bool dungeonEvent251 { get; set; }
        public bool dungeonEvent250_SlayBoss { get; set; }
        public bool dungeonEvent250 { get; set; }

        public bool dungeonEvent249 { get; set; }
        public bool dungeonEvent248 { get; set; }
        public bool dungeonEvent247 { get; set; }
        public bool dungeonEvent246 { get; set; }
        public bool dungeonEvent245 { get; set; }
        public bool dungeonEvent244 { get; set; }
        public bool dungeonEvent243 { get; set; }
        public bool dungeonEvent242 { get; set; }
        public bool dungeonEvent241 { get; set; }
        public bool dungeonEvent240 { get; set; }
        public bool dungeonEvent239 { get; set; }
        public bool dungeonEvent238 { get; set; }

        public bool dungeonEvent237 { get; set; }
        public bool dungeonEvent237_Complete { get; set; }
        public bool dungeonEvent237_Fail3 { get; set; }
        public bool dungeonEvent237_Fail2 { get; set; }
        public bool dungeonEvent237_Fail1 { get; set; }

        public bool dungeonEvent236 { get; set; }
        public bool dungeonEvent236_Complete { get; set; }
        public bool dungeonEvent236_Fail3 { get; set; }
        public bool dungeonEvent236_Fail2 { get; set; }
        public bool dungeonEvent236_Fail1 { get; set; }

        public bool dungeonEvent235 { get; set; }
        public bool dungeonEvent235_Complete { get; set; }
        public bool dungeonEvent235_Fail3 { get; set; }
        public bool dungeonEvent235_Fail2 { get; set; }
        public bool dungeonEvent235_Fail1 { get; set; }

        public bool dungeonEvent234 { get; set; }
        public bool dungeonEvent234_Complete { get; set; }
        public bool dungeonEvent234_Fail3 { get; set; }
        public bool dungeonEvent234_Fail2 { get; set; }
        public bool dungeonEvent234_Fail1 { get; set; }

        public bool dungeonEvent233_Complete { get; set; }
        public bool dungeonEvent233_Fail3 { get; set; }
        public bool dungeonEvent233_Fail2 { get; set; }
        public bool dungeonEvent233_Fail1 { get; set; }
        public bool dungeonEvent233 { get; set; }

        public bool dungeonEvent232 { get; set; }
        public bool dungeonEvent231 { get; set; }
        public bool dungeonEvent230_8 { get; set; }
        public bool dungeonEvent230_72 { get; set; }
        public bool dungeonEvent230_71 { get; set; }
        public bool dungeonEvent230_7 { get; set; }
        public bool dungeonEvent230_64 { get; set; }
        public bool dungeonEvent230_63 { get; set; }
        public bool dungeonEvent230_62 { get; set; }
        public bool dungeonEvent230_61 { get; set; }
        public bool dungeonEvent230_6 { get; set; }
        public bool dungeonEvent230_52 { get; set; }
        public bool dungeonEvent230_51 { get; set; }
        public bool dungeonEvent230_5 { get; set; }
        public bool dungeonEvent230_42 { get; set; }
        public bool dungeonEvent230_41 { get; set; }
        public bool dungeonEvent230_4 { get; set; }
        public bool dungeonEvent230_32 { get; set; }
        public bool dungeonEvent230_31 { get; set; }
        public bool dungeonEvent230_3 { get; set; }
        public bool dungeonEvent230_21 { get; set; }
        public bool dungeonEvent230_2 { get; set; }
        public bool dungeonEvent230_12 { get; set; }
        public bool dungeonEvent230_11 { get; set; }
        public bool dungeonEvent230_1 { get; set; }
        public bool dungeonEvent230_01 { get; set; }
        public bool dungeonEvent230_0 { get; set; }
        public bool dungeonEvent230 { get; set; }
        public bool dungeonEvent229 { get; set; }
        public bool dungeonEvent228 { get; set; }

        public bool dungeonEvent227 { get; set; }
        public bool dungeonEvent226 { get; set; }
        public bool dungeonEvent225 { get; set; }
        public bool dungeonEvent224 { get; set; }
        public bool dungeonEvent223 { get; set; }
        public bool dungeonEvent222 { get; set; }
        public bool dungeonEvent221 { get; set; }
        public bool dungeonEvent220 { get; set; }
        public bool dungeonEvent219 { get; set; }
        public bool dungeonEvent218_2 { get; set; }
        public bool dungeonEvent218 { get; set; }
        public bool dungeonEvent217_2 { get; set; }
        public bool dungeonEvent217 { get; set; }
        public bool dungeonEvent216_2 { get; set; }
        public bool dungeonEvent216 { get; set; }
        public bool dungeonEvent215 { get; set; }
        public bool dungeonEvent214 { get; set; }
        public bool dungeonEvent213 { get; set; }
        public bool dungeonEvent212 { get; set; }
        public bool dungeonEvent211FailEvent2 { get; set; }
        public bool dungeonEvent211FailEvent { get; set; }
        public bool dungeonEvent211Fail { get; set; }
        public bool dungeonEvent211 { get; set; }
        public bool dungeonEvent210 { get; set; }
        public bool dungeonEvent209FailEvent2 { get; set; }
        public bool dungeonEvent209FailEvent { get; set; }
        public bool dungeonEvent209Fail { get; set; }
        public bool dungeonEvent209 { get; set; }
        public bool dungeonEvent208 { get; set; }
        public bool dungeonEvent207FailEvent2 { get; set; }
        public bool dungeonEvent207FailEvent { get; set; }
        public bool dungeonEvent207Fail { get; set; }
        public bool dungeonEvent207 { get; set; }
        public bool dungeonEvent206 { get; set; }
        public bool dungeonEvent205 { get; set; }
        public bool dungeonEvent204 { get; set; }
        public bool dungeonEvent203 { get; set; }
        public bool dungeonEvent202 { get; set; }
        public bool dungeonEvent201 { get; set; }
        #endregion
        #region "�P�K"
        public bool dungeonEvent31 { get; set; }
        public bool dungeonEvent30 { get; set; }
        public bool dungeonEvent29 { get; set; }
        public bool dungeonEvent28KeyOpen { get; set; }
        public bool dungeonEvent27 { get; set; }
        public bool dungeonEvent26 { get; set; }
        public bool dungeonEvent25 { get; set; }
        public bool dungeonEvent24KeyOpen { get; set; }
        public bool dungeonEvent24NotOpen { get; set; }
        public bool dungeonEvent23KeyOpen { get; set; }
        public bool dungeonEvent23NotOpen { get; set; }
        public bool dungeonEvent22KeyOpen { get; set; }
        public bool dungeonEvent22NotOpen { get; set; }
        public bool dungeonEvent21KeyOpen { get; set; }
        public bool dungeonEvent21NotOpen { get; set; }
        public bool dungeonEvent20 { get; set; }
        public bool dungeonEvent19 { get; set; }
        public bool dungeonEvent18 { get; set; }
        public bool dungeonEvent17 { get; set; }
        public bool dungeonEvent16_4NotOpen { get; set; }
        public bool dungeonEvent16_3NotOpen { get; set; }
        public bool dungeonEvent16_2NotOpen { get; set; }
        public bool dungeonEvent16_1NotOpen { get; set; }
        public bool dungeonEvent16 { get; set; }
        public bool dungeonEvent15 { get; set; }
        public bool dungeonEvent14KeyOpen { get; set; }
        public bool dungeonEvent14NotOpen { get; set; }
        public bool dungeonEvent13KeyOpen { get; set; }
        public bool dungeonEvent13NotOpen { get; set; }
        public bool dungeonEvent12KeyOpen { get; set; }
        public bool dungeonEvent12NotOpen { get; set; }
        public bool dungeonEvent11KeyOpen { get; set; }
        public bool dungeonEvent11NotOpen { get; set; }
        #endregion
        #region "�`���[�g���A��"
        public bool dungeonEvent01 { get; set; }
        public bool dungeonEvent02 { get; set; }
        public bool dungeonEvent03 { get; set; }
        #endregion

        public bool TruthTreasure01 { get; set; } // �`���[�g���A��
        public bool TruthTreasure02 { get; set; } // �`���[�g���A��
        public bool TruthTreasure11 { get; set; } // �G���A�P
        public bool TruthTreasure12 { get; set; } // �G���A�P
        public bool TruthTreasure13 { get; set; } // �G���A�P
        public bool TruthTreasure14 { get; set; } // �G���A�P
        public bool TruthTreasure15 { get; set; } // �G���A�P
        public bool TruthTreasure121 { get; set; } // �G���A�Q
        public bool TruthTreasure122 { get; set; } // �G���A�Q
        public bool TruthTreasure123 { get; set; } // �G���A�Q
        public bool TruthTreasure124 { get; set; } // �G���A�Q
        public bool TruthTreasure125 { get; set; } // �G���A�Q
        public bool TruthTreasure126 { get; set; } // �G���A�Q
        public bool TruthTreasure127 { get; set; } // �G���A�Q
        public bool TruthTreasure128 { get; set; } // �G���A�Q
        public bool TruthTreasure129 { get; set; } // �G���A�Q
        public bool TruthTreasure1210 { get; set; } // �G���A�Q
        public bool TruthTreasure1211 { get; set; } // �G���A�Q
        public bool TruthTreasure1212 { get; set; } // �G���A�Q
        public bool TruthTreasure131 { get; set; } // �G���A�R
        public bool TruthTreasure132 { get; set; } // �G���A�R
        public bool TruthTreasure133 { get; set; } // �G���A�R
        public bool TruthTreasure134 { get; set; } // �G���A�R
        public bool TruthTreasure141 { get; set; } // �G���A�S
        public bool TruthTreasure142 { get; set; } // �G���A�S
        public bool TruthTreasure21 { get; set; } // �m�̕����P
        public bool TruthTreasure22 { get; set; } // �m�̕����Q
        public bool TruthTreasure23 { get; set; } // �m�̕����R
        public bool TruthTreasure24 { get; set; } // �Z�̕����P
        public bool TruthTreasure25 { get; set; } // �Z�̕����Q
        public bool TruthTreasure26 { get; set; } // �Z�̕����R
        public bool TruthTreasure27 { get; set; } // �Z�̕����S
        public bool TruthTreasure28 { get; set; } // �Z�̕����T
        public bool TruthTreasure29 { get; set; } // �Z�̕����U
        public bool TruthTreasure210 { get; set; } // �Z�̕����V
        public bool TruthTreasure211 { get; set; } // �S�̕����P
        public bool TruthTreasure212 { get; set; } // �S�̕����Q
        public bool TruthTreasure213 { get; set; } // �͂̕����P
        public bool TruthTreasure214 { get; set; } // �͂̕����Q
        public bool TruthTreasure215 { get; set; } // �͂̕����R
        public bool TruthTreasure216 { get; set; } // �͂̕����S
        public bool TruthTreasure217 { get; set; } // �͂̕����T
        public bool TruthTreasure218 { get; set; } // �͂̕����U
        public bool TruthTreasure301 { get; set; } // ���G���A�P
        public bool TruthTreasure302 { get; set; } // ���G���A�P
        public bool TruthTreasure303 { get; set; } // ���G���A�P
        public bool TruthTreasure304 { get; set; } // ���G���A�P
        public bool TruthTreasure305 { get; set; } // ���G���A�P
        public bool TruthTreasure306 { get; set; } // ���G���A�P
        public bool TruthTreasure307 { get; set; } // ���G���A�Q
        public bool TruthTreasure308 { get; set; } // ���G���A�Q
        public bool TruthTreasure309 { get; set; } // ���G���A�Q
        public bool TruthTreasure310 { get; set; } // ���G���A�Q
        public bool TruthTreasure311 { get; set; } // ���G���A�Q
        public bool TruthTreasure312 { get; set; } // ���G���A�Q
        public bool TruthTreasure401 { get; set; } // �G�X�~���A�������P
        public bool TruthTreasure402 { get; set; } // �G�X�~���A�������Q
        public bool TruthTreasure403 { get; set; } // �G�X�~���A�������R
        public bool TruthTreasure404 { get; set; } // �G�X�~���A�������S
        public bool TruthTreasure405 { get; set; } // �G�X�~���A�������T
        public bool TruthTreasure406 { get; set; } // �G�X�~���A�������U
        public bool TruthTreasure407 { get; set; } // �G�X�~���A�������V
        public bool TruthTreasure408 { get; set; } // �G�X�~���A�������W
        public bool TruthTreasure409 { get; set; } // �t�@�[�W���{�a�󕨌�X�P
        public bool TruthTreasure410 { get; set; } // �t�@�[�W���{�a�󕨌�X�Q
        public bool TruthTreasure411 { get; set; } // �t�@�[�W���{�a�󕨌�X�R
        public bool TruthTreasure412 { get; set; } // �t�@�[�W���{�a�󕨌�X�S
        public bool TruthTreasure413 { get; set; } // �t�@�[�W���{�a�󕨌�X�T
        public bool TruthTreasure414 { get; set; } // �t�@�[�W���{�a�󕨌�X�U
        public bool TruthTreasure415 { get; set; } // �t�@�[�W���{�a�󕨌�X�V
        public bool TruthTreasure416 { get; set; } // �t�@�[�W���{�a�󕨌�X�W
        public bool TruthTreasure417 { get; set; } // �t�@�[�W���{�a�󕨌�X�X
        public bool TruthTreasure418 { get; set; } // �t�@�[�W���{�a�󕨌�X�P�O
        public bool TruthTreasure419 { get; set; } // �t�@�[�W���{�a�󕨌�X�P�P
        public bool TruthTreasure420 { get; set; } // �t�@�[�W���{�a�󕨌�X�P�Q
        public bool TruthTreasure421 { get; set; } // �t�@�[�W���{�a�󕨌�X�P�R
        public bool TruthTreasure422 { get; set; } // �t�@�[�W���{�a�󕨌�X�P�S
        public bool TruthTreasure423 { get; set; } // �t�@�[�W���{�a�󕨌�Y�P
        public bool TruthTreasure424 { get; set; } // �t�@�[�W���{�a�󕨌�Y�Q
        public bool TruthTreasure425 { get; set; } // �t�@�[�W���{�a�󕨌�Y�R
        public bool TruthTreasure426 { get; set; } // �t�@�[�W���{�a�󕨌�Y�S
        public bool TruthTreasure427 { get; set; } // �t�@�[�W���{�a�󕨌�Y�T
        public bool TruthTreasure428 { get; set; } // �t�@�[�W���{�a�󕨌�Y�U
        public bool TruthTreasure429 { get; set; } // �t�@�[�W���{�a�󕨌�Y�V
        public bool TruthTreasure430 { get; set; } // �t�@�[�W���{�a�󕨌�Y�W
        public bool TruthTreasure431 { get; set; } // �t�@�[�W���{�a�󕨌�Y�X
        public bool TruthTreasure432 { get; set; } // �t�@�[�W���{�a�󕨌�Y�P�O
        public bool TruthTreasure433 { get; set; } // �t�@�[�W���{�a�󕨌�Y�P�P
        public bool TruthTreasure434 { get; set; } // �t�@�[�W���{�a�󕨌�Y�P�Q
        public bool TruthTreasure435 { get; set; } // �t�@�[�W���{�a�󕨌�Z�P
        public bool TruthTreasure436 { get; set; } // �t�@�[�W���{�a�󕨌�Z�Q
        public bool TruthTreasure437 { get; set; } // �t�@�[�W���{�a�󕨌�Z�R
        public bool TruthTreasure438 { get; set; } // �t�@�[�W���{�a�󕨌�Z�S
        public bool TruthTreasure439 { get; set; } // �t�@�[�W���{�a�󕨌�Z�T
        public bool TruthTreasure440 { get; set; } // �t�@�[�W���{�a�󕨌�Z�U
        public bool TruthTreasure441 { get; set; } // �t�@�[�W���{�a�󕨌�Z�V
        public bool TruthTreasure442 { get; set; } // �t�@�[�W���{�a�󕨌�Z�W
        public bool TruthTreasure443 { get; set; } // �����̌n���P
        public bool TruthTreasure444 { get; set; } // �����̌n���Q
        public bool TruthTreasure445 { get; set; } // �����̌n���R
        public bool TruthTreasure446 { get; set; } // �����̌n���S
        public bool TruthTreasure447 { get; set; } // �����̌n���T
        public bool TruthTreasure448 { get; set; } // �����̌n���U
        public bool TruthTreasure449 { get; set; } // �����̌n���V
        public bool TruthTreasure450 { get; set; } // �����̌n���W
        public bool TruthTreasure451 { get; set; } // ���X�g�t�F�C�Y�P
        public bool TruthTreasure452 { get; set; } // ���X�g�t�F�C�Y�Q
        public bool TruthTreasure453 { get; set; } // ���X�g�t�F�C�Y�R
        public bool TruthTreasure454 { get; set; } // ���X�g�t�F�C�Y�S
        public bool TruthTreasure455 { get; set; } // ���X�g�t�F�C�Y�T
        public bool TruthTreasure456 { get; set; } // ���X�g�t�F�C�Y�U
        public bool TruthTreasure457 { get; set; } // ���X�g�t�F�C�Y�V
        public bool TruthTreasure458 { get; set; } // ���X�g�t�F�C�Y�W
        public bool TruthTreasure459 { get; set; } // ���X�g�t�F�C�Y�X

        public bool GanzGift1 { get; set; } // �Ñ�h���̊��̒f�Бf�ނ������n����

        public bool TruthSpecialInfo1 { get; set; }
        public bool BoardInfo14 { get; set; }
        public bool BoardInfo13 { get; set; }
        public bool BoardInfo12 { get; set; }
        public bool BoardInfo11 { get; set; }
        public bool BoardInfo10 { get; set; }
        public bool MeetOlLandisBeforeGanz { get; set; }
        public bool MeetOlLandisBeforeHanna { get; set; }
        public bool MeetOlLandisBeforeLana { get; set; }
        public bool MeetOlLandis { get; set; }
        public bool AvailableDuelMatch { get; set; }
        public bool AvailableDuelColosseum { get; set; }
        public bool AvailablePotionshop { get; set; }
        public bool AvailableBattleSettingMenu { get; set; }
        public bool AvailableInstantCommand { get; set; }
        public bool AvailableMixSpellSkill { get; set; }
        public bool AvailableArchetypeCommand { get; set; } // ���݉��`�̔����\
        public bool AvailableBackGate { get; set; }

        public bool AlreadyCommunicateFazilCastle { get; set; } // �t�@�[�W���{�a�ŃC�x���g��
        public bool alreadyCommunicateCahlhanz { get; set; } // �z�[���^�E���ŃJ�[���݂Ɖ�b��

        public bool Truth_CommunicationFifthHomeTown { get; set; }
        public bool Truth_CommunicationFourthHomeTown { get; set; }
        public bool Truth_CommunicationThirdHomeTown { get; set; }
        public bool Truth_CommunicationSecondHomeTown { get; set; }

        public bool Truth_Communication_Dungeon11 { get; set; }
        public bool Truth_CommunicationJoinPartyLana { get; set; }
        public bool Truth_CommunicationNotJoinLana { get; set; }
        public bool Truth_CommunicationFirstHomeTown { get; set; }

        // �v���C���[�X�e�[�^�X��_���W�����i�s�󋵂ɉ����ĉςȃt���O
        public bool Truth_CommunicationLana1_1 { get; set; }
        public bool Truth_CommunicationLana1_2 { get; set; }

        // ����ȊO�̑O�ғ��l����
        public bool Truth_CommunicationLana1 { get; set; }
        public bool Truth_CommunicationLana2 { get; set; }
        public bool Truth_CommunicationLana3 { get; set; }
        public bool Truth_CommunicationLana4 { get; set; }
        public bool Truth_CommunicationLana5 { get; set; }
        public bool Truth_CommunicationLana6 { get; set; }
        public bool Truth_CommunicationLana7 { get; set; }
        public bool Truth_CommunicationLana8 { get; set; }
        public bool Truth_CommunicationLana9 { get; set; }
        public bool Truth_CommunicationLana10 { get; set; }
        public bool Truth_CommunicationLana11 { get; set; }
        public bool Truth_CommunicationLana21 { get; set; } // �Q�K�ȍ~
        public bool Truth_CommunicationLana22 { get; set; }
        public bool Truth_CommunicationLana31 { get; set; } // �R�K�ȍ~
        public bool Truth_CommunicationLana41 { get; set; } // �S�K�ȍ~

        public bool Truth_CommunicationGanz1 { get; set; }
        public bool Truth_CommunicationGanz2 { get; set; }
        public bool Truth_CommunicationGanz3 { get; set; }
        public bool Truth_CommunicationGanz4 { get; set; }
        public bool Truth_CommunicationGanz5 { get; set; }
        public bool Truth_CommunicationGanz6 { get; set; }
        public bool Truth_CommunicationGanz7 { get; set; }
        public bool Truth_CommunicationGanz8 { get; set; }
        public bool Truth_CommunicationGanz9 { get; set; }
        public bool Truth_CommunicationGanz10 { get; set; }
        public bool Truth_CommunicationGanz21 { get; set; } // �Q�K�ȍ~
        public bool Truth_CommunicationGanz31 { get; set; } // �R�K�ȍ~
        public bool Truth_CommunicationGanz32 { get; set; } // �Ñ�h���̊��̒f�Ђ𔄋p���鎞
        public bool Truth_CommunicationGanz41 { get; set; } // �S�K�ȍ~

        public bool Truth_CommunicationHanna1 { get; set; }
        public bool Truth_CommunicationHanna2 { get; set; }
        public bool Truth_CommunicationHanna3 { get; set; }
        public bool Truth_CommunicationHanna4 { get; set; }
        public bool Truth_CommunicationHanna5 { get; set; }
        public bool Truth_CommunicationHanna6 { get; set; }
        public bool Truth_CommunicationHanna7 { get; set; }
        public bool Truth_CommunicationHanna8 { get; set; }
        public bool Truth_CommunicationHanna9 { get; set; }
        public bool Truth_CommunicationHanna10 { get; set; }
        public bool Truth_CommunicationHanna21 { get; set; } // �Q�K�ȍ~
        public bool Truth_CommunicationHanna31 { get; set; } // �R�K�ȍ~
        public bool Truth_CommunicationHanna31_2 { get; set; }
        public bool Truth_CommunicationHanna41 { get; set; } // �S�K�ȍ~

        public bool Truth_CommunicationOl21 { get; set; }
        public bool Truth_CommunicationOl22 { get; set; }
        public bool Truth_CommunicationOl22Fail { get; set; }
        public bool Truth_CommunicationOl22Progress1 { get; set; }
        public bool Truth_CommunicationOl22Progress2 { get; set; }
        public bool Truth_CommunicationOl22DuelFail { get; set; }
        public int Truth_CommunicationOl22DuelFailCount { get; set; }
        public bool Truth_CommunicationOl31 { get; set; } // �R�K�ȍ~
        public bool Truth_CommunicationOl41 { get; set; } // �R�K�ȍ~

        public bool Truth_CommunicationSinikia31 { get; set; } // �R�K�ȍ~
        public bool Truth_CommunicationSinikia30DuelFail { get; set; }
        public int Truth_CommunicationSinikia30DuelFailCount { get; set; }
        public bool Truth_CommunicationSinikia41 { get; set; } // �R�K�ȍ~

        public bool Truth_Communication_FC31 { get; set; } // �t�@�[�W���{�a
        public bool Truth_Communication_FC32 { get; set; }

        public bool Truth_GiveLanaEarring { get; set; }

        public bool Truth_FirstOneDayItem { get; set; }

        public int dungeonViewPointX { get; set; } // �_���W�������ŃZ�[�u�����r���[�|�C���gX
        public int dungeonViewPointY { get; set; } // �_���W�������ŃZ�[�u�����r���[�|�C���gY

        //public bool AvailableFood1 { get; set; } // �n�߂�����ւ��Ă��邽�߁A�s�v�ȃt���O
        public bool AvailableFood2 { get; set; }
        public bool AvailableFood3 { get; set; }
        public bool AvailableFood4 { get; set; }
        public bool AvailableFood5 { get; set; }

        //public bool AvailablePotion1 { get; set; } // �n�߂�����ւ��Ă��邽�߁A�s�v�ȃt���O
        public bool AvailablePotion2 { get; set; }
        public bool AvailablePotion3 { get; set; }
        public bool AvailablePotion4 { get; set; }
        public bool AvailablePotion5 { get; set; }

        public bool AvailableItemBank { get; set; }
        public bool AvailableFazilCastle { get; set; }
        public bool AvailableOneDayItem { get; set; }

        public bool DuelWinZalge { get; set; } // �U���Q�Ƃ�DUEL��ŏ��������ꍇ�ATrue

        public string ItemBank1 { get; set; }
        public string ItemBank1Stack { get; set; }
        public string ItemBank2 { get; set; }
        public string ItemBank2Stack { get; set; }
        public string ItemBank3 { get; set; }
        public string ItemBank3Stack { get; set; }
        public string ItemBank4 { get; set; }
        public string ItemBank4Stack { get; set; }
        public string ItemBank5 { get; set; }
        public string ItemBank5Stack { get; set; }
        public string ItemBank6 { get; set; }
        public string ItemBank6Stack { get; set; }
        public string ItemBank7 { get; set; }
        public string ItemBank7Stack { get; set; }
        public string ItemBank8 { get; set; }
        public string ItemBank8Stack { get; set; }
        public string ItemBank9 { get; set; }
        public string ItemBank9Stack { get; set; }
        public string ItemBank10 { get; set; }
        public string ItemBank10Stack { get; set; }
        public string ItemBank11 { get; set; }
        public string ItemBank11Stack { get; set; }
        public string ItemBank12 { get; set; }
        public string ItemBank12Stack { get; set; }
        public string ItemBank13 { get; set; }
        public string ItemBank13Stack { get; set; }
        public string ItemBank14 { get; set; }
        public string ItemBank14Stack { get; set; }
        public string ItemBank15 { get; set; }
        public string ItemBank15Stack { get; set; }
        public string ItemBank16 { get; set; }
        public string ItemBank16Stack { get; set; }
        public string ItemBank17 { get; set; }
        public string ItemBank17Stack { get; set; }
        public string ItemBank18 { get; set; }
        public string ItemBank18Stack { get; set; }
        public string ItemBank19 { get; set; }
        public string ItemBank19Stack { get; set; }
        public string ItemBank20 { get; set; }
        public string ItemBank20Stack { get; set; }
        public string ItemBank21 { get; set; }
        public string ItemBank21Stack { get; set; }
        public string ItemBank22 { get; set; }
        public string ItemBank22Stack { get; set; }
        public string ItemBank23 { get; set; }
        public string ItemBank23Stack { get; set; }
        public string ItemBank24 { get; set; }
        public string ItemBank24Stack { get; set; }
        public string ItemBank25 { get; set; }
        public string ItemBank25Stack { get; set; }
        public string ItemBank26 { get; set; }
        public string ItemBank26Stack { get; set; }
        public string ItemBank27 { get; set; }
        public string ItemBank27Stack { get; set; }
        public string ItemBank28 { get; set; }
        public string ItemBank28Stack { get; set; }
        public string ItemBank29 { get; set; }
        public string ItemBank29Stack { get; set; }
        public string ItemBank30 { get; set; }
        public string ItemBank30Stack { get; set; }
        public string ItemBank31 { get; set; }
        public string ItemBank31Stack { get; set; }
        public string ItemBank32 { get; set; }
        public string ItemBank32Stack { get; set; }
        public string ItemBank33 { get; set; }
        public string ItemBank33Stack { get; set; }
        public string ItemBank34 { get; set; }
        public string ItemBank34Stack { get; set; }
        public string ItemBank35 { get; set; }
        public string ItemBank35Stack { get; set; }
        public string ItemBank36 { get; set; }
        public string ItemBank36Stack { get; set; }
        public string ItemBank37 { get; set; }
        public string ItemBank37Stack { get; set; }
        public string ItemBank38 { get; set; }
        public string ItemBank38Stack { get; set; }
        public string ItemBank39 { get; set; }
        public string ItemBank39Stack { get; set; }
        public string ItemBank40 { get; set; }
        public string ItemBank40Stack { get; set; }
        public string ItemBank41 { get; set; }
        public string ItemBank41Stack { get; set; }
        public string ItemBank42 { get; set; }
        public string ItemBank42Stack { get; set; }
        public string ItemBank43 { get; set; }
        public string ItemBank43Stack { get; set; }
        public string ItemBank44 { get; set; }
        public string ItemBank44Stack { get; set; }
        public string ItemBank45 { get; set; }
        public string ItemBank45Stack { get; set; }
        public string ItemBank46 { get; set; }
        public string ItemBank46Stack { get; set; }
        public string ItemBank47 { get; set; }
        public string ItemBank47Stack { get; set; }
        public string ItemBank48 { get; set; }
        public string ItemBank48Stack { get; set; }
        public string ItemBank49 { get; set; }
        public string ItemBank49Stack { get; set; }
        public string ItemBank50 { get; set; }
        public string ItemBank50Stack { get; set; }
        public string ItemBank51 { get; set; }
        public string ItemBank51Stack { get; set; }
        public string ItemBank52 { get; set; }
        public string ItemBank52Stack { get; set; }
        public string ItemBank53 { get; set; }
        public string ItemBank53Stack { get; set; }
        public string ItemBank54 { get; set; }
        public string ItemBank54Stack { get; set; }
        public string ItemBank55 { get; set; }
        public string ItemBank55Stack { get; set; }
        public string ItemBank56 { get; set; }
        public string ItemBank56Stack { get; set; }
        public string ItemBank57 { get; set; }
        public string ItemBank57Stack { get; set; }
        public string ItemBank58 { get; set; }
        public string ItemBank58Stack { get; set; }
        public string ItemBank59 { get; set; }
        public string ItemBank59Stack { get; set; }
        public string ItemBank60 { get; set; }
        public string ItemBank60Stack { get; set; }
        public string ItemBank61 { get; set; }
        public string ItemBank61Stack { get; set; }
        public string ItemBank62 { get; set; }
        public string ItemBank62Stack { get; set; }
        public string ItemBank63 { get; set; }
        public string ItemBank63Stack { get; set; }
        public string ItemBank64 { get; set; }
        public string ItemBank64Stack { get; set; }
        public string ItemBank65 { get; set; }
        public string ItemBank65Stack { get; set; }
        public string ItemBank66 { get; set; }
        public string ItemBank66Stack { get; set; }
        public string ItemBank67 { get; set; }
        public string ItemBank67Stack { get; set; }
        public string ItemBank68 { get; set; }
        public string ItemBank68Stack { get; set; }
        public string ItemBank69 { get; set; }
        public string ItemBank69Stack { get; set; }
        public string ItemBank70 { get; set; }
        public string ItemBank70Stack { get; set; }
        public string ItemBank71 { get; set; }
        public string ItemBank71Stack { get; set; }
        public string ItemBank72 { get; set; }
        public string ItemBank72Stack { get; set; }
        public string ItemBank73 { get; set; }
        public string ItemBank73Stack { get; set; }
        public string ItemBank74 { get; set; }
        public string ItemBank74Stack { get; set; }
        public string ItemBank75 { get; set; }
        public string ItemBank75Stack { get; set; }
        public string ItemBank76 { get; set; }
        public string ItemBank76Stack { get; set; }
        public string ItemBank77 { get; set; }
        public string ItemBank77Stack { get; set; }
        public string ItemBank78 { get; set; }
        public string ItemBank78Stack { get; set; }
        public string ItemBank79 { get; set; }
        public string ItemBank79Stack { get; set; }
        public string ItemBank80 { get; set; }
        public string ItemBank80Stack { get; set; }
        public string ItemBank81 { get; set; }
        public string ItemBank81Stack { get; set; }
        public string ItemBank82 { get; set; }
        public string ItemBank82Stack { get; set; }
        public string ItemBank83 { get; set; }
        public string ItemBank83Stack { get; set; }
        public string ItemBank84 { get; set; }
        public string ItemBank84Stack { get; set; }
        public string ItemBank85 { get; set; }
        public string ItemBank85Stack { get; set; }
        public string ItemBank86 { get; set; }
        public string ItemBank86Stack { get; set; }
        public string ItemBank87 { get; set; }
        public string ItemBank87Stack { get; set; }
        public string ItemBank88 { get; set; }
        public string ItemBank88Stack { get; set; }
        public string ItemBank89 { get; set; }
        public string ItemBank89Stack { get; set; }
        public string ItemBank90 { get; set; }
        public string ItemBank90Stack { get; set; }
        public string ItemBank91 { get; set; }
        public string ItemBank91Stack { get; set; }
        public string ItemBank92 { get; set; }
        public string ItemBank92Stack { get; set; }
        public string ItemBank93 { get; set; }
        public string ItemBank93Stack { get; set; }
        public string ItemBank94 { get; set; }
        public string ItemBank94Stack { get; set; }
        public string ItemBank95 { get; set; }
        public string ItemBank95Stack { get; set; }
        public string ItemBank96 { get; set; }
        public string ItemBank96Stack { get; set; }
        public string ItemBank97 { get; set; }
        public string ItemBank97Stack { get; set; }
        public string ItemBank98 { get; set; }
        public string ItemBank98Stack { get; set; }
        public string ItemBank99 { get; set; }
        public string ItemBank99Stack { get; set; }
        public string ItemBank100 { get; set; }
        public string ItemBank100Stack { get; set; }
        #endregion

        public void InitializeItemBankData()
        {
            ItemBank1 = string.Empty;
            ItemBank1Stack = "0";
            ItemBank2 = string.Empty;
            ItemBank2Stack = "0";
            ItemBank3 = string.Empty;
            ItemBank3Stack = "0";
            ItemBank4 = string.Empty;
            ItemBank4Stack = "0";
            ItemBank5 = string.Empty;
            ItemBank5Stack = "0";
            ItemBank6 = string.Empty;
            ItemBank6Stack = "0";
            ItemBank7 = string.Empty;
            ItemBank7Stack = "0";
            ItemBank8 = string.Empty;
            ItemBank8Stack = "0";
            ItemBank9 = string.Empty;
            ItemBank9Stack = "0";
            ItemBank10 = string.Empty;
            ItemBank10Stack = "0";
            ItemBank11 = string.Empty;
            ItemBank11Stack = "0";
            ItemBank12 = string.Empty;
            ItemBank12Stack = "0";
            ItemBank13 = string.Empty;
            ItemBank13Stack = "0";
            ItemBank14 = string.Empty;
            ItemBank14Stack = "0";
            ItemBank15 = string.Empty;
            ItemBank15Stack = "0";
            ItemBank16 = string.Empty;
            ItemBank16Stack = "0";
            ItemBank17 = string.Empty;
            ItemBank17Stack = "0";
            ItemBank18 = string.Empty;
            ItemBank18Stack = "0";
            ItemBank19 = string.Empty;
            ItemBank19Stack = "0";
            ItemBank20 = string.Empty;
            ItemBank20Stack = "0";
            ItemBank21 = string.Empty;
            ItemBank21Stack = "0";
            ItemBank22 = string.Empty;
            ItemBank22Stack = "0";
            ItemBank23 = string.Empty;
            ItemBank23Stack = "0";
            ItemBank24 = string.Empty;
            ItemBank24Stack = "0";
            ItemBank25 = string.Empty;
            ItemBank25Stack = "0";
            ItemBank26 = string.Empty;
            ItemBank26Stack = "0";
            ItemBank27 = string.Empty;
            ItemBank27Stack = "0";
            ItemBank28 = string.Empty;
            ItemBank28Stack = "0";
            ItemBank29 = string.Empty;
            ItemBank29Stack = "0";
            ItemBank30 = string.Empty;
            ItemBank30Stack = "0";
            ItemBank31 = string.Empty;
            ItemBank31Stack = "0";
            ItemBank32 = string.Empty;
            ItemBank32Stack = "0";
            ItemBank33 = string.Empty;
            ItemBank33Stack = "0";
            ItemBank34 = string.Empty;
            ItemBank34Stack = "0";
            ItemBank35 = string.Empty;
            ItemBank35Stack = "0";
            ItemBank36 = string.Empty;
            ItemBank36Stack = "0";
            ItemBank37 = string.Empty;
            ItemBank37Stack = "0";
            ItemBank38 = string.Empty;
            ItemBank38Stack = "0";
            ItemBank39 = string.Empty;
            ItemBank39Stack = "0";
            ItemBank40 = string.Empty;
            ItemBank40Stack = "0";
            ItemBank41 = string.Empty;
            ItemBank41Stack = "0";
            ItemBank42 = string.Empty;
            ItemBank42Stack = "0";
            ItemBank43 = string.Empty;
            ItemBank43Stack = "0";
            ItemBank44 = string.Empty;
            ItemBank44Stack = "0";
            ItemBank45 = string.Empty;
            ItemBank45Stack = "0";
            ItemBank46 = string.Empty;
            ItemBank46Stack = "0";
            ItemBank47 = string.Empty;
            ItemBank47Stack = "0";
            ItemBank48 = string.Empty;
            ItemBank48Stack = "0";
            ItemBank49 = string.Empty;
            ItemBank49Stack = "0";
            ItemBank50 = string.Empty;
            ItemBank50Stack = "0";
            ItemBank51 = string.Empty;
            ItemBank51Stack = "0";
            ItemBank52 = string.Empty;
            ItemBank52Stack = "0";
            ItemBank53 = string.Empty;
            ItemBank53Stack = "0";
            ItemBank54 = string.Empty;
            ItemBank54Stack = "0";
            ItemBank55 = string.Empty;
            ItemBank55Stack = "0";
            ItemBank56 = string.Empty;
            ItemBank56Stack = "0";
            ItemBank57 = string.Empty;
            ItemBank57Stack = "0";
            ItemBank58 = string.Empty;
            ItemBank58Stack = "0";
            ItemBank59 = string.Empty;
            ItemBank59Stack = "0";
            ItemBank60 = string.Empty;
            ItemBank60Stack = "0";
            ItemBank61 = string.Empty;
            ItemBank61Stack = "0";
            ItemBank62 = string.Empty;
            ItemBank62Stack = "0";
            ItemBank63 = string.Empty;
            ItemBank63Stack = "0";
            ItemBank64 = string.Empty;
            ItemBank64Stack = "0";
            ItemBank65 = string.Empty;
            ItemBank65Stack = "0";
            ItemBank66 = string.Empty;
            ItemBank66Stack = "0";
            ItemBank67 = string.Empty;
            ItemBank67Stack = "0";
            ItemBank68 = string.Empty;
            ItemBank68Stack = "0";
            ItemBank69 = string.Empty;
            ItemBank69Stack = "0";
            ItemBank70 = string.Empty;
            ItemBank70Stack = "0";
            ItemBank71 = string.Empty;
            ItemBank71Stack = "0";
            ItemBank72 = string.Empty;
            ItemBank72Stack = "0";
            ItemBank73 = string.Empty;
            ItemBank73Stack = "0";
            ItemBank74 = string.Empty;
            ItemBank74Stack = "0";
            ItemBank75 = string.Empty;
            ItemBank75Stack = "0";
            ItemBank76 = string.Empty;
            ItemBank76Stack = "0";
            ItemBank77 = string.Empty;
            ItemBank77Stack = "0";
            ItemBank78 = string.Empty;
            ItemBank78Stack = "0";
            ItemBank79 = string.Empty;
            ItemBank79Stack = "0";
            ItemBank80 = string.Empty;
            ItemBank80Stack = "0";
            ItemBank81 = string.Empty;
            ItemBank81Stack = "0";
            ItemBank82 = string.Empty;
            ItemBank82Stack = "0";
            ItemBank83 = string.Empty;
            ItemBank83Stack = "0";
            ItemBank84 = string.Empty;
            ItemBank84Stack = "0";
            ItemBank85 = string.Empty;
            ItemBank85Stack = "0";
            ItemBank86 = string.Empty;
            ItemBank86Stack = "0";
            ItemBank87 = string.Empty;
            ItemBank87Stack = "0";
            ItemBank88 = string.Empty;
            ItemBank88Stack = "0";
            ItemBank89 = string.Empty;
            ItemBank89Stack = "0";
            ItemBank90 = string.Empty;
            ItemBank90Stack = "0";
            ItemBank91 = string.Empty;
            ItemBank91Stack = "0";
            ItemBank92 = string.Empty;
            ItemBank92Stack = "0";
            ItemBank93 = string.Empty;
            ItemBank93Stack = "0";
            ItemBank94 = string.Empty;
            ItemBank94Stack = "0";
            ItemBank95 = string.Empty;
            ItemBank95Stack = "0";
            ItemBank96 = string.Empty;
            ItemBank96Stack = "0";
            ItemBank97 = string.Empty;
            ItemBank97Stack = "0";
            ItemBank98 = string.Empty;
            ItemBank98Stack = "0";
            ItemBank99 = string.Empty;
            ItemBank99Stack = "0";
            ItemBank100 = string.Empty;
            ItemBank100Stack = "0";
        }

        public void LoadItemBankData(ref string[] items, ref int[] stack)
        {
            items[0] = Convert.ToString(ItemBank1);
            stack[0] = Convert.ToInt32( ItemBank1Stack);
            items[1] = Convert.ToString(ItemBank2);
            stack[1] = Convert.ToInt32( ItemBank2Stack);
            items[2] = Convert.ToString(ItemBank3);
            stack[2] = Convert.ToInt32( ItemBank3Stack);
            items[3] = Convert.ToString(ItemBank4);
            stack[3] = Convert.ToInt32( ItemBank4Stack);
            items[4] = Convert.ToString(ItemBank5);
            stack[4] = Convert.ToInt32( ItemBank5Stack);
            items[5] = Convert.ToString(ItemBank6);
            stack[5] = Convert.ToInt32( ItemBank6Stack);
            items[6] = Convert.ToString(ItemBank7);
            stack[6] = Convert.ToInt32( ItemBank7Stack);
            items[7] = Convert.ToString(ItemBank8);
            stack[7] = Convert.ToInt32( ItemBank8Stack);
            items[8] = Convert.ToString(ItemBank9);
            stack[8] = Convert.ToInt32( ItemBank9Stack);
            items[9] = Convert.ToString(ItemBank10);
            stack[9] = Convert.ToInt32( ItemBank10Stack);

            items[10] = Convert.ToString(ItemBank11);
            stack[10] = Convert.ToInt32( ItemBank11Stack);
            items[11] = Convert.ToString(ItemBank12);
            stack[11] = Convert.ToInt32( ItemBank12Stack);
            items[12] = Convert.ToString(ItemBank13);
            stack[12] = Convert.ToInt32( ItemBank13Stack);
            items[13] = Convert.ToString(ItemBank14);
            stack[13] = Convert.ToInt32( ItemBank14Stack);
            items[14] = Convert.ToString(ItemBank15);
            stack[14] = Convert.ToInt32( ItemBank15Stack);
            items[15] = Convert.ToString(ItemBank16);
            stack[15] = Convert.ToInt32( ItemBank16Stack);
            items[16] = Convert.ToString(ItemBank17);
            stack[16] = Convert.ToInt32( ItemBank17Stack);
            items[17] = Convert.ToString(ItemBank18);
            stack[17] = Convert.ToInt32( ItemBank18Stack);
            items[18] = Convert.ToString(ItemBank19);
            stack[18] = Convert.ToInt32( ItemBank19Stack);
            items[19] = Convert.ToString(ItemBank20);
            stack[19] = Convert.ToInt32( ItemBank20Stack);

            items[20] = Convert.ToString(ItemBank21);
            stack[20] = Convert.ToInt32( ItemBank21Stack);
            items[21] = Convert.ToString(ItemBank22);
            stack[21] = Convert.ToInt32( ItemBank22Stack);
            items[22] = Convert.ToString(ItemBank23);
            stack[22] = Convert.ToInt32( ItemBank23Stack);
            items[23] = Convert.ToString(ItemBank24);
            stack[23] = Convert.ToInt32( ItemBank24Stack);
            items[24] = Convert.ToString(ItemBank25);
            stack[24] = Convert.ToInt32( ItemBank25Stack);
            items[25] = Convert.ToString(ItemBank26);
            stack[25] = Convert.ToInt32( ItemBank26Stack);
            items[26] = Convert.ToString(ItemBank27);
            stack[26] = Convert.ToInt32( ItemBank27Stack);
            items[27] = Convert.ToString(ItemBank28);
            stack[27] = Convert.ToInt32( ItemBank28Stack);
            items[28] = Convert.ToString(ItemBank29);
            stack[28] = Convert.ToInt32( ItemBank29Stack);
            items[29] = Convert.ToString(ItemBank30);
            stack[29] = Convert.ToInt32( ItemBank30Stack);

            items[30] = Convert.ToString(ItemBank31);
            stack[30] = Convert.ToInt32( ItemBank31Stack);
            items[31] = Convert.ToString(ItemBank32);
            stack[31] = Convert.ToInt32( ItemBank32Stack);
            items[32] = Convert.ToString(ItemBank33);
            stack[32] = Convert.ToInt32( ItemBank33Stack);
            items[33] = Convert.ToString(ItemBank34);
            stack[33] = Convert.ToInt32( ItemBank34Stack);
            items[34] = Convert.ToString(ItemBank35);
            stack[34] = Convert.ToInt32( ItemBank35Stack);
            items[35] = Convert.ToString(ItemBank36);
            stack[35] = Convert.ToInt32( ItemBank36Stack);
            items[36] = Convert.ToString(ItemBank37);
            stack[36] = Convert.ToInt32( ItemBank37Stack);
            items[37] = Convert.ToString(ItemBank38);
            stack[37] = Convert.ToInt32( ItemBank38Stack);
            items[38] = Convert.ToString(ItemBank39);
            stack[38] = Convert.ToInt32( ItemBank39Stack);
            items[39] = Convert.ToString(ItemBank40);
            stack[39] = Convert.ToInt32( ItemBank40Stack);

            items[40] = Convert.ToString(ItemBank41);
            stack[40] = Convert.ToInt32( ItemBank41Stack);
            items[41] = Convert.ToString(ItemBank42);
            stack[41] = Convert.ToInt32( ItemBank42Stack);
            items[42] = Convert.ToString(ItemBank43);
            stack[42] = Convert.ToInt32( ItemBank43Stack);
            items[43] = Convert.ToString(ItemBank44);
            stack[43] = Convert.ToInt32( ItemBank44Stack);
            items[44] = Convert.ToString(ItemBank45);
            stack[44] = Convert.ToInt32( ItemBank45Stack);
            items[45] = Convert.ToString(ItemBank46);
            stack[45] = Convert.ToInt32( ItemBank46Stack);
            items[46] = Convert.ToString(ItemBank47);
            stack[46] = Convert.ToInt32( ItemBank47Stack);
            items[47] = Convert.ToString(ItemBank48);
            stack[47] = Convert.ToInt32( ItemBank48Stack);
            items[48] = Convert.ToString(ItemBank49);
            stack[48] = Convert.ToInt32( ItemBank49Stack);
            items[49] = Convert.ToString(ItemBank50);
            stack[49] = Convert.ToInt32( ItemBank50Stack);

            items[50] = Convert.ToString(ItemBank51);
            stack[50] = Convert.ToInt32( ItemBank51Stack);
            items[51] = Convert.ToString(ItemBank52);
            stack[51] = Convert.ToInt32( ItemBank52Stack);
            items[52] = Convert.ToString(ItemBank53);
            stack[52] = Convert.ToInt32( ItemBank53Stack);
            items[53] = Convert.ToString(ItemBank54);
            stack[53] = Convert.ToInt32( ItemBank54Stack);
            items[54] = Convert.ToString(ItemBank55);
            stack[54] = Convert.ToInt32( ItemBank55Stack);
            items[55] = Convert.ToString(ItemBank56);
            stack[55] = Convert.ToInt32( ItemBank56Stack);
            items[56] = Convert.ToString(ItemBank57);
            stack[56] = Convert.ToInt32( ItemBank57Stack);
            items[57] = Convert.ToString(ItemBank58);
            stack[57] = Convert.ToInt32( ItemBank58Stack);
            items[58] = Convert.ToString(ItemBank59);
            stack[58] = Convert.ToInt32( ItemBank59Stack);
            items[59] = Convert.ToString(ItemBank60);
            stack[59] = Convert.ToInt32( ItemBank60Stack);

            items[60] = Convert.ToString(ItemBank61);
            stack[60] = Convert.ToInt32( ItemBank61Stack);
            items[61] = Convert.ToString(ItemBank62);
            stack[61] = Convert.ToInt32( ItemBank62Stack);
            items[62] = Convert.ToString(ItemBank63);
            stack[62] = Convert.ToInt32( ItemBank63Stack);
            items[63] = Convert.ToString(ItemBank64);
            stack[63] = Convert.ToInt32( ItemBank64Stack);
            items[64] = Convert.ToString(ItemBank65);
            stack[64] = Convert.ToInt32( ItemBank65Stack);
            items[65] = Convert.ToString(ItemBank66);
            stack[65] = Convert.ToInt32( ItemBank66Stack);
            items[66] = Convert.ToString(ItemBank67);
            stack[66] = Convert.ToInt32( ItemBank67Stack);
            items[67] = Convert.ToString(ItemBank68);
            stack[67] = Convert.ToInt32( ItemBank68Stack);
            items[68] = Convert.ToString(ItemBank69);
            stack[68] = Convert.ToInt32( ItemBank69Stack);
            items[69] = Convert.ToString(ItemBank70);
            stack[69] = Convert.ToInt32( ItemBank70Stack);

            items[70] = Convert.ToString(ItemBank71);
            stack[70] = Convert.ToInt32( ItemBank71Stack);
            items[71] = Convert.ToString(ItemBank72);
            stack[71] = Convert.ToInt32( ItemBank72Stack);
            items[72] = Convert.ToString(ItemBank73);
            stack[72] = Convert.ToInt32( ItemBank73Stack);
            items[73] = Convert.ToString(ItemBank74);
            stack[73] = Convert.ToInt32( ItemBank74Stack);
            items[74] = Convert.ToString(ItemBank75);
            stack[74] = Convert.ToInt32( ItemBank75Stack);
            items[75] = Convert.ToString(ItemBank76);
            stack[75] = Convert.ToInt32( ItemBank76Stack);
            items[76] = Convert.ToString(ItemBank77);
            stack[76] = Convert.ToInt32( ItemBank77Stack);
            items[77] = Convert.ToString(ItemBank78);
            stack[77] = Convert.ToInt32( ItemBank78Stack);
            items[78] = Convert.ToString(ItemBank79);
            stack[78] = Convert.ToInt32( ItemBank79Stack);
            items[79] = Convert.ToString(ItemBank80);
            stack[79] = Convert.ToInt32( ItemBank80Stack);

            items[80] = Convert.ToString(ItemBank81);
            stack[80] = Convert.ToInt32( ItemBank81Stack);
            items[81] = Convert.ToString(ItemBank82);
            stack[81] = Convert.ToInt32( ItemBank82Stack);
            items[82] = Convert.ToString(ItemBank83);
            stack[82] = Convert.ToInt32( ItemBank83Stack);
            items[83] = Convert.ToString(ItemBank84);
            stack[83] = Convert.ToInt32( ItemBank84Stack);
            items[84] = Convert.ToString(ItemBank85);
            stack[84] = Convert.ToInt32( ItemBank85Stack);
            items[85] = Convert.ToString(ItemBank86);
            stack[85] = Convert.ToInt32( ItemBank86Stack);
            items[86] = Convert.ToString(ItemBank87);
            stack[86] = Convert.ToInt32( ItemBank87Stack);
            items[87] = Convert.ToString(ItemBank88);
            stack[87] = Convert.ToInt32( ItemBank88Stack);
            items[88] = Convert.ToString(ItemBank89);
            stack[88] = Convert.ToInt32( ItemBank89Stack);
            items[89] = Convert.ToString(ItemBank90);
            stack[89] = Convert.ToInt32( ItemBank90Stack);

            items[90] = Convert.ToString(ItemBank91);
            stack[90] = Convert.ToInt32( ItemBank91Stack);
            items[91] = Convert.ToString(ItemBank92);
            stack[91] = Convert.ToInt32( ItemBank92Stack);
            items[92] = Convert.ToString(ItemBank93);
            stack[92] = Convert.ToInt32( ItemBank93Stack);
            items[93] = Convert.ToString(ItemBank94);
            stack[93] = Convert.ToInt32( ItemBank94Stack);
            items[94] = Convert.ToString(ItemBank95);
            stack[94] = Convert.ToInt32( ItemBank95Stack);
            items[95] = Convert.ToString(ItemBank96);
            stack[95] = Convert.ToInt32( ItemBank96Stack);
            items[96] = Convert.ToString(ItemBank97);
            stack[96] = Convert.ToInt32( ItemBank97Stack);
            items[97] = Convert.ToString(ItemBank98);
            stack[97] = Convert.ToInt32( ItemBank98Stack);
            items[98] = Convert.ToString(ItemBank99);
            stack[98] = Convert.ToInt32( ItemBank99Stack);
            items[99] = Convert.ToString(ItemBank100);
            stack[99] = Convert.ToInt32( ItemBank100Stack);

        }
        public void UpdateItemBankData(string[] items, int[] stack)
        {
            ItemBank1       = items[0];
            ItemBank1Stack  = stack[0].ToString();
            ItemBank2       = items[1];
            ItemBank2Stack  = stack[1].ToString();
            ItemBank3       = items[2];
            ItemBank3Stack  = stack[2].ToString();
            ItemBank4       = items[3];
            ItemBank4Stack  = stack[3].ToString();
            ItemBank5       = items[4];
            ItemBank5Stack  = stack[4].ToString();
            ItemBank6       = items[5];
            ItemBank6Stack  = stack[5].ToString();
            ItemBank7       = items[6];
            ItemBank7Stack  = stack[6].ToString();
            ItemBank8       = items[7];
            ItemBank8Stack  = stack[7].ToString();
            ItemBank9       = items[8];
            ItemBank9Stack  = stack[8].ToString();
            ItemBank10      = items[9];
            ItemBank10Stack = stack[9].ToString();

            ItemBank11       = items[10];
            ItemBank11Stack  = stack[10].ToString();
            ItemBank12       = items[11];
            ItemBank12Stack  = stack[11].ToString();
            ItemBank13       = items[12];
            ItemBank13Stack  = stack[12].ToString();
            ItemBank14       = items[13];
            ItemBank14Stack  = stack[13].ToString();
            ItemBank15       = items[14];
            ItemBank15Stack  = stack[14].ToString();
            ItemBank16       = items[15];
            ItemBank16Stack  = stack[15].ToString();
            ItemBank17       = items[16];
            ItemBank17Stack  = stack[16].ToString();
            ItemBank18       = items[17];
            ItemBank18Stack  = stack[17].ToString();
            ItemBank19       = items[18];
            ItemBank19Stack  = stack[18].ToString();
            ItemBank20       = items[19];
            ItemBank20Stack  = stack[19].ToString();

            ItemBank21       = items[20];
            ItemBank21Stack  = stack[20].ToString();
            ItemBank22       = items[21];
            ItemBank22Stack  = stack[21].ToString();
            ItemBank23       = items[22];
            ItemBank23Stack  = stack[22].ToString();
            ItemBank24       = items[23];
            ItemBank24Stack  = stack[23].ToString();
            ItemBank25       = items[24];
            ItemBank25Stack  = stack[24].ToString();
            ItemBank26       = items[25];
            ItemBank26Stack  = stack[25].ToString();
            ItemBank27       = items[26];
            ItemBank27Stack  = stack[26].ToString();
            ItemBank28       = items[27];
            ItemBank28Stack  = stack[27].ToString();
            ItemBank29       = items[28];
            ItemBank29Stack  = stack[28].ToString();
            ItemBank30       = items[29];
            ItemBank30Stack  = stack[29].ToString();

            ItemBank31       = items[30];
            ItemBank31Stack  = stack[30].ToString();
            ItemBank32       = items[31];
            ItemBank32Stack  = stack[31].ToString();
            ItemBank33       = items[32];
            ItemBank33Stack  = stack[32].ToString();
            ItemBank34       = items[33];
            ItemBank34Stack  = stack[33].ToString();
            ItemBank35       = items[34];
            ItemBank35Stack  = stack[34].ToString();
            ItemBank36       = items[35];
            ItemBank36Stack  = stack[35].ToString();
            ItemBank37       = items[36];
            ItemBank37Stack  = stack[36].ToString();
            ItemBank38       = items[37];
            ItemBank38Stack  = stack[37].ToString();
            ItemBank39       = items[38];
            ItemBank39Stack  = stack[38].ToString();
            ItemBank40       = items[39];
            ItemBank40Stack  = stack[39].ToString();

            ItemBank41       = items[40];
            ItemBank41Stack  = stack[40].ToString();
            ItemBank42       = items[41];
            ItemBank42Stack  = stack[41].ToString();
            ItemBank43       = items[42];
            ItemBank43Stack  = stack[42].ToString();
            ItemBank44       = items[43];
            ItemBank44Stack  = stack[43].ToString();
            ItemBank45       = items[44];
            ItemBank45Stack  = stack[44].ToString();
            ItemBank46       = items[45];
            ItemBank46Stack  = stack[45].ToString();
            ItemBank47       = items[46];
            ItemBank47Stack  = stack[46].ToString();
            ItemBank48       = items[47];
            ItemBank48Stack  = stack[47].ToString();
            ItemBank49       = items[48];
            ItemBank49Stack  = stack[48].ToString();
            ItemBank50       = items[49];
            ItemBank50Stack  = stack[49].ToString();

            ItemBank51       = items[50];
            ItemBank51Stack  = stack[50].ToString();
            ItemBank52       = items[51];
            ItemBank52Stack  = stack[51].ToString();
            ItemBank53       = items[52];
            ItemBank53Stack  = stack[52].ToString();
            ItemBank54       = items[53];
            ItemBank54Stack  = stack[53].ToString();
            ItemBank55       = items[54];
            ItemBank55Stack  = stack[54].ToString();
            ItemBank56       = items[55];
            ItemBank56Stack  = stack[55].ToString();
            ItemBank57       = items[56];
            ItemBank57Stack  = stack[56].ToString();
            ItemBank58       = items[57];
            ItemBank58Stack  = stack[57].ToString();
            ItemBank59       = items[58];
            ItemBank59Stack  = stack[58].ToString();
            ItemBank60       = items[59];
            ItemBank60Stack  = stack[59].ToString();        

            ItemBank61       = items[60];
            ItemBank61Stack  = stack[60].ToString();
            ItemBank62       = items[61];
            ItemBank62Stack  = stack[61].ToString();
            ItemBank63       = items[62];
            ItemBank63Stack  = stack[62].ToString();
            ItemBank64       = items[63];
            ItemBank64Stack  = stack[63].ToString();
            ItemBank65       = items[64];
            ItemBank65Stack  = stack[64].ToString();
            ItemBank66       = items[65];
            ItemBank66Stack  = stack[65].ToString();
            ItemBank67       = items[66];
            ItemBank67Stack  = stack[66].ToString();
            ItemBank68       = items[67];
            ItemBank68Stack  = stack[67].ToString();
            ItemBank69       = items[68];
            ItemBank69Stack  = stack[68].ToString();
            ItemBank70       = items[69];
            ItemBank70Stack  = stack[69].ToString();

            ItemBank71       = items[70];
            ItemBank71Stack  = stack[70].ToString();
            ItemBank72       = items[71];
            ItemBank72Stack  = stack[71].ToString();
            ItemBank73       = items[72];
            ItemBank73Stack  = stack[72].ToString();
            ItemBank74       = items[73];
            ItemBank74Stack  = stack[73].ToString();
            ItemBank75       = items[74];
            ItemBank75Stack  = stack[74].ToString();
            ItemBank76       = items[75];
            ItemBank76Stack  = stack[75].ToString();
            ItemBank77       = items[76];
            ItemBank77Stack  = stack[76].ToString();
            ItemBank78       = items[77];
            ItemBank78Stack  = stack[77].ToString();
            ItemBank79       = items[78];
            ItemBank79Stack  = stack[78].ToString();
            ItemBank80       = items[79];
            ItemBank80Stack  = stack[79].ToString();

            ItemBank81       = items[80];
            ItemBank81Stack  = stack[80].ToString();
            ItemBank82       = items[81];
            ItemBank82Stack  = stack[81].ToString();
            ItemBank83       = items[82];
            ItemBank83Stack  = stack[82].ToString();
            ItemBank84       = items[83];
            ItemBank84Stack  = stack[83].ToString();
            ItemBank85       = items[84];
            ItemBank85Stack  = stack[84].ToString();
            ItemBank86       = items[85];
            ItemBank86Stack  = stack[85].ToString();
            ItemBank87       = items[86];
            ItemBank87Stack  = stack[86].ToString();
            ItemBank88       = items[87];
            ItemBank88Stack  = stack[87].ToString();
            ItemBank89       = items[88];
            ItemBank89Stack  = stack[88].ToString();
            ItemBank90       = items[89];
            ItemBank90Stack  = stack[89].ToString();

            ItemBank91       = items[90];
            ItemBank91Stack  = stack[90].ToString();
            ItemBank92       = items[91];
            ItemBank92Stack  = stack[91].ToString();
            ItemBank93       = items[92];
            ItemBank93Stack  = stack[92].ToString();
            ItemBank94       = items[93];
            ItemBank94Stack  = stack[93].ToString();
            ItemBank95       = items[94];
            ItemBank95Stack  = stack[94].ToString();
            ItemBank96       = items[95];
            ItemBank96Stack  = stack[95].ToString();
            ItemBank97       = items[96];
            ItemBank97Stack  = stack[96].ToString();
            ItemBank98       = items[97];
            ItemBank98Stack  = stack[97].ToString();
            ItemBank99       = items[98];
            ItemBank99Stack  = stack[98].ToString();
            ItemBank100      = items[99];
            ItemBank100Stack = stack[99].ToString();
        }
    }
}
