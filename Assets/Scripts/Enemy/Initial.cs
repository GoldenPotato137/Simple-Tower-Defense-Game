using UnityEngine;

namespace Enemy
{
    public class Initial : Enemy
    {
        protected override void DestroyOperate()
        {
            Debug.Log("Hi,I am a initial which reached your home!");
        }
    }
}