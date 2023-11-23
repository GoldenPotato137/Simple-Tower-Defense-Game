using Contract;
using UnityEngine;

namespace Manager
{
    public abstract class ManagerBase : Object, IUpdate
    {
        public abstract void Update();
        
        public abstract void Stop();
    }
}