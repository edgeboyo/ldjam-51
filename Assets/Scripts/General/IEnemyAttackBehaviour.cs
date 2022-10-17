using System.Collections;
using UnityEngine;
using Enemy;
using General;
using Player;
namespace General
{
    public interface IEnemyAttackBehaviour
    {
        public EnemyStats Stats { get; }
        public abstract float GetAttackRange();
        public abstract void Attack();
    }
}