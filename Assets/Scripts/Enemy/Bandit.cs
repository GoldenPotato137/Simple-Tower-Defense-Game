namespace Enemy
{
    public class Bandit : Enemy
    {
        protected override void ReachTargetOperate()
        {
            ;
        }

        protected override void KilledOperate()
        {
            throw new System.NotImplementedException();
        }

        protected override void DamagedOperate(int damaged)
        {
            throw new System.NotImplementedException();
        }
    }
}