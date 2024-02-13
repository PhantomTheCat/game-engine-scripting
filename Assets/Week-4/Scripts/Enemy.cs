using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Week4;

namespace Week4
{
    public class Enemy : MonoBehaviour
    {
        //Properties
        public int health = 10;
        public int attackDamage = 3;
        [SerializeField] private Player target;




        //Methods
        public void DamageEnemy(int amount)
        {
            health -= amount;
        }

        [ContextMenu("Attack")]
        private void Attack()
        {
            target.DamagePlayer(attackDamage);
        }
    }
}
