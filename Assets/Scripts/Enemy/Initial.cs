using UnityEngine;

namespace Enemy
{
    public class Initial : Enemy
    {
        protected override void ReachTargetOperate()
        {
            Debug.Log("Hi,I am a initial which reached your home!");
        }

        protected override void KilledOperate()
        {
            // throw new System.NotImplementedException();
        }

        protected override void DamagedOperate(int damaged)
        {
            Debug.Log("HP:"+hp);
            // throw new System.NotImplementedException();
        }
    }
}