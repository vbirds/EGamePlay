using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEditor;
using EGamePlay.Combat;

namespace EGamePlay
{
    public class ExecuteClipData : ScriptableObject
    {
        public float TotalTime { get; set; }
        public float StartTime;
        public float EndTime;

        [ShowInInspector]
        [DelayedProperty]
        [PropertyOrder(-1)]
        public string Name
        {
            get { return name; }
            set { name = value; /*AssetDatabase.ForceReserializeAssets(new string[] { AssetDatabase.GetAssetPath(this) }); AssetDatabase.SaveAssets(); AssetDatabase.Refresh();*/ }
        }

        public ExecuteClipType ExecuteClipType;

        [Space(10)]
        [ShowIf("ExecuteClipType", ExecuteClipType.ActionEvent)]
        public ActionEventData ActionEventData;

        [Space(10)]
        [ShowIf("ExecuteClipType", ExecuteClipType.CollisionExecute)]
        public CollisionExecuteData CollisionExecuteData;

        [Space(10)]
        [ShowIf("ExecuteClipType", ExecuteClipType.Animation)]
        public AnimationData AnimationData;

        [Space(10)]
        [ShowIf("ExecuteClipType", ExecuteClipType.Audio)]
        public AudioData AudioData;

        [Space(10)]
        [ShowIf("ExecuteClipType", ExecuteClipType.ParticleEffect)]
        public ParticleEffectData ParticleEffectData;

        public float Duration { get => (EndTime - StartTime); }

        public ExecuteClipData GetClipTime()
        {
            return this;
        }
    }

    public enum ExecuteClipType
    {
        CollisionExecute = 0,
        ActionEvent = 1,
        Animation = 2,
        Audio = 3,
        ParticleEffect = 4,
    }

    [LabelText("Ŀ�괫������")]
    public enum ExecutionTargetInputType
    {
        [LabelText("None")]
        None = 0,
        [LabelText("����Ŀ��ʵ��")]
        Target = 1,
        [LabelText("����Ŀ���")]
        Point = 2,
    }

    [LabelText("�¼�����")]
    public enum FireEventType
    {
        [LabelText("��������Ч��")]
        AssignEffect = 0,
        [LabelText("������ִ����")]
        TriggerNewExecution = 1,
    }

    [Serializable]
    public class ActionEventData
    {
        public FireEventType ActionEventType;
        [ShowIf("ActionEventType", FireEventType.AssignEffect)]
        public EffectApplyType EffectApply;
        [ShowIf("ActionEventType", FireEventType.TriggerNewExecution)]
        [LabelText("��ִ����")]
        public string NewExecution;
    }

    //[LabelText("ִ������")]
    //public enum ExecuteType
    //{
    //    [LabelText("�׳��¼�")]
    //    FireOutEvent = 0,
    //    [LabelText("����ִ��")]
    //    ContinuousExecute = 1,
    //}

    [LabelText("��ײ��ִ������")]
    public enum CollisionExecuteType
    {
        [LabelText("����ִ��")]
        OutOfHand = 0,
        [LabelText("ִ��ִ��")]
        InHand = 1,
    }

    [Serializable]
    public class CollisionExecuteData
    {
        public CollisionExecuteType ExecuteType;
        public ActionEventData ActionData;

        [Space(10)]
        public CollisionShape Shape;
        [ShowIf("Shape", CollisionShape.Sphere), LabelText("�뾶")]
        public float Radius;

        [ShowIf("Shape", CollisionShape.Box)]
        public Vector3 Center;
        [ShowIf("Shape", CollisionShape.Box)]
        public Vector3 Size;

        [Space(10)]
        public CollisionMoveType MoveType;
        [DelayedProperty]
        public GameObject ObjAsset;

        [ShowIf("ShowSpeed")]
        public float Speed = 1;
        public bool ShowSpeed { get => MoveType != CollisionMoveType.SelectedPosition && MoveType != CollisionMoveType.SelectedDirection; }
        public bool ShowPoints { get => MoveType == CollisionMoveType.PathFly || MoveType == CollisionMoveType.SelectedDirectionPathFly; }
        [ShowIf("ShowPoints")]
        public List<CtrlPoint> Points;

        public List<CtrlPoint> GetCtrlPoints()
        {
            var list = new List<CtrlPoint>();
            foreach (var item in Points)
            {
                var newPoint = new CtrlPoint();
                newPoint.position = item.position;
                newPoint.type = item.type;
                newPoint.InTangent = item.InTangent;
                newPoint.OutTangent = item.OutTangent;
                list.Add(newPoint);
            }
            return list;
        }
    }

    [Serializable]
    public class ParticleEffectData
    {
        public GameObject ParticleEffect;
    }

    [Serializable]
    public class AnimationData
    {
        public AnimationClip AnimationClip;
    }

    [Serializable]
    public class AudioData
    {
        public AudioClip AudioClip;
    }
}