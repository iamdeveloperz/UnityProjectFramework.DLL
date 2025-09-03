
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityProjectFramework.Core.UpdateLoop
{
    internal sealed class UpdateLoopRunner : MonoBehaviour
    {
        #region Fields
        
        private const string OBJECT_NAME = "[UpdateLoop.Runner] (Not Editable)";

        public static UpdateLoopRunner Instance { get; private set; }

        private readonly IteratorList<IUpdatable> _updatables = new IteratorList<IUpdatable>();
        private readonly IteratorList<IFixedUpdatable> _fixedUpdatables = new IteratorList<IFixedUpdatable>();
        private readonly IteratorList<ILateUpdatable> _lateUpdatables = new IteratorList<ILateUpdatable>();
        
        public bool HasRegistered =>
            _updatables.Count > 0 ||
            _fixedUpdatables.Count > 0 ||
            _lateUpdatables.Count > 0;

        #endregion
        
        internal static void CreateInstance()
        {
            var gameObject = new GameObject(OBJECT_NAME)
            {
                hideFlags = HideFlags.NotEditable | HideFlags.HideInInspector | HideFlags.DontSave
            };
            
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                gameObject.hideFlags = HideFlags.HideAndDontSave;
            }
            else
#endif
            {
                DontDestroyOnLoad(gameObject);
            }
            
            Instance = gameObject.AddComponent<UpdateLoopRunner>();
        }
        
        internal void Register(IManagedUpdatable managedUpdatable)
        {
            switch (managedUpdatable)
            {
                case IUpdatable updatable:
                    _updatables.Add(updatable);
                    break;
                case IFixedUpdatable fixedUpdatable:
                    _fixedUpdatables.Add(fixedUpdatable);
                    break;
                case ILateUpdatable lateUpdatable:
                    _lateUpdatables.Add(lateUpdatable);
                    break;
            }

            enabled = HasRegistered;
        }
        
        internal void Unregister(IManagedUpdatable managedUpdatable)
        {
            switch (managedUpdatable)
            {
                case IUpdatable updatable:
                    _updatables.Remove(updatable);
                    break;
                case IFixedUpdatable fixedUpdatable:
                    _fixedUpdatables.Remove(fixedUpdatable);
                    break;
                case ILateUpdatable lateUpdatable:
                    _lateUpdatables.Remove(lateUpdatable);
                    break;
            }

            enabled = HasRegistered;
        }
        
        internal void Clear()
        {
            _updatables.Clear();
            _fixedUpdatables.Clear();
            _lateUpdatables.Clear();
        }
        
        #region Updates
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Update()
        {
            foreach (var updatable in _updatables)
            {
                try
                {
                    updatable.OnUpdate();
                }
                catch (Exception exception)
                {
                    Log.Ex(exception);
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void FixedUpdate()
        {
            foreach (var updatable in _fixedUpdatables)
            {
                try
                {
                    updatable.OnFixedUpdate();
                }
                catch (Exception exception)
                {
                    Log.Ex(exception);
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void LateUpdate()
        {
            foreach (var updatable in _lateUpdatables)
            {
                try
                {
                    updatable.OnLateUpdate();
                }
                catch (Exception exception)
                {
                    Log.Ex(exception);
                }
            }
        }

        #endregion
    }
}