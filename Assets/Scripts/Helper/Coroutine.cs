/*
 * @Author: iwiniwin
 * @Date: 2021-06-04 19:33:53
 * @Description: 自定义协程
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helper
{
    public sealed class CoroutineManager
    {
        private static CoroutineManager _instance;
        public const int TickToSecond = 10000000;
        private long _previousTicks;
        
        private readonly Dictionary<string, List<Coroutine>> _coroutineDict = new();

        public static Coroutine Start(IEnumerator routine, object target = null)
        {
            return InternalStart(new Coroutine(routine, target));
        }
        
        private static Coroutine InternalStart(Coroutine coroutine)
        {
            CreateInstanceIfRequired();
            _instance.StartCoroutine(coroutine);
            return coroutine;
        }

        private static void CreateInstanceIfRequired()
        {
            _instance ??= new CoroutineManager
            {
                _previousTicks = DateTime.Now.Ticks
            };
        }
        
        public static void Stop(Coroutine routine)
        {
            _instance?.StopCoroutine(routine);
        }
        
        public static void StopAll()
        {
            if(_instance == null) return;
            _instance.StopAllCoroutines();
        }

        private void StopCoroutine(Coroutine routine)
        {
            if(_coroutineDict.ContainsKey(routine.Key))
            {
                _coroutineDict.Remove(routine.Key);
            }
        }

        private void StopAllCoroutines(string targetKey)
        {
            List<string> keys = new List<string>(_coroutineDict.Keys);
            foreach (string key in keys)
            {
                List<Coroutine> coroutines = _coroutineDict[key];
                for (int i = coroutines.Count - 1; i >= 0; i--)
                {
                    if(coroutines[i].TargetKey == targetKey)
                    {
                        coroutines.RemoveAt(i);
                    }
                }
                if(_coroutineDict[key].Count == 0)
                {
                    _coroutineDict.Remove(key);
                }
            }
        }

        private void StopAllCoroutines()
        {
            _coroutineDict.Clear();
        }

        public static void Update()
        {
            CreateInstanceIfRequired();
            float deltaTime = (float)(DateTime.Now.Ticks - _instance._previousTicks) / TickToSecond;
            if(deltaTime > 0)  // 避免一帧内触发多次
            {
                _instance.OnUpdate(deltaTime);
                _instance._previousTicks = DateTime.Now.Ticks;
            }
        }

        private void StartCoroutine(Coroutine coroutine)
        {
            MoveNext(coroutine);
            if (!_coroutineDict.ContainsKey(coroutine.Key))
            {
                _coroutineDict.Add(coroutine.Key, new List<Coroutine>());
            }
            _coroutineDict[coroutine.Key].Add(coroutine);
        }

        private void OnUpdate(float dt)
        {
            if (_coroutineDict.Count == 0) return;
            List<string> keys = new List<string>(_coroutineDict.Keys);
            foreach (string key in keys)
            {
                List<Coroutine> coroutines = _coroutineDict[key];
                for (int i = coroutines.Count - 1; i >= 0; i--)
                {
                    Coroutine coroutine = coroutines[i];
                    if(!coroutine.CurrentYield.IsDone(dt))
                    {
                        continue;
                    }
                    if(!MoveNext(coroutine))
                    {
                        coroutines.RemoveAt(i);
                        coroutine.CurrentYield = null;
                        coroutine.Finished = true;
                    }
                }
                if(_coroutineDict[key].Count == 0)
                {
                    _coroutineDict.Remove(key);
                }
            }
        }

        private static bool MoveNext(Coroutine coroutine)
        {
            if (!coroutine.Routine.MoveNext())
            {
                return false;
            }
            object current = coroutine.Routine.Current;
            if (current == null)
            {
                coroutine.CurrentYield = new YieldDefault();
            }
            else if(current is Coroutine)
            {
                coroutine.CurrentYield = new YieldNestedCoroutine(){
                    coroutine = (Coroutine)current
                };
            }
            else if(current is IEnumerator)
            {
                Coroutine nestedCoroutine = Start((IEnumerator)current);
                coroutine.CurrentYield = new YieldNestedCoroutine(){
                    coroutine = nestedCoroutine
                };
            }
            else if(current is ICoroutineYield)
            {
                coroutine.CurrentYield = (ICoroutineYield)current;
            }
            else
            {
                coroutine.CurrentYield = new YieldDefault();
            }
            return true;
        }
    }

    public sealed class Coroutine : YieldInstruction
    {
        private string m_Key = "";
        private string m_TargetKey = "";

        public string Key => m_Key;
        public string TargetKey => m_TargetKey;

        private IEnumerator m_Routine;
        internal IEnumerator Routine => m_Routine;

        internal bool Finished {get; set;}

        internal ICoroutineYield CurrentYield = new YieldDefault();

        internal Coroutine(IEnumerator routine, string methodName, object target)
        {
            this.m_TargetKey = target.GetHashCode().ToString();
            this.m_Key = this.m_TargetKey + methodName;
            this.m_Routine = routine;
        }

        internal Coroutine(IEnumerator routine, object target)
        {
            if (target != null)
            {
                this.m_TargetKey = target.GetHashCode().ToString();
            }
            if(routine != null)
            {
                this.m_Key = this.m_TargetKey + routine.GetHashCode().ToString();
            }
            this.m_Routine = routine;
        }
    }

    public interface ICoroutineYield
    {
        bool IsDone(float dt);
    }

    public class YieldDefault : ICoroutineYield
    {
        public bool IsDone(float dt)
        {
            return true;
        }
    }

    public class YieldNestedCoroutine : ICoroutineYield
    {
        public Coroutine coroutine;
        public bool IsDone(float dt)
        {
            return coroutine.Finished;
        }
    }

    public class YieldWaitForSeconds : ICoroutineYield
    {
        private float timeLeft = 0;
        public YieldWaitForSeconds(float timeLeft)
        {
            this.timeLeft = timeLeft;
        }
        public bool IsDone(float dt)
        {
            timeLeft -= dt;
            return timeLeft <= 0;
        }
    }
}