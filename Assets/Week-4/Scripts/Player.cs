using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Week4
{
    public class Player : MonoBehaviour
    {
        /*public int health
        {
            get { return _health; }
            private set { _health = value; }
        }

        private int _health = 10;*/

        /*public int health
        {
            get;
            private set;
        }

        private void Awake()
        {
            health = 10;
        }*/

 

        [SerializeField] private int health = 10;
        [SerializeField]
        //[Multiline(10)]
        private string playerName;


        public void Damage(int amt)
        {
            health -= amt;
        }

        private Enemy FindNewTarget()
        {
            //GameObject.FindGameObjectWithTag("Enemy")

            /*GameObject enemy = GameObject.Find("Enemy");
            enemy.GetComponent<Enemy>();*/


            Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
            int randomIndex = Random.Range(0, enemies.Length);
            return enemies[randomIndex];
        }

        public int GetHealth()
        {
            return health;
        }

        [ContextMenu("Attack")]
        void Attack()
        {
            Enemy target = FindNewTarget();
            Vector3 origin = transform.position;

            transform.DOMove(target.transform.position, 1f).OnComplete(() =>
            {

                target.Damage(10);

                transform.DOMove(origin, 0.25f);

            });

           
            
            

            //AudioManager.PlaySound(AudioManager.SoundType.ATTACK);
        }
    }
}
