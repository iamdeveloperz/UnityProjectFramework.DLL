
using System.Collections.Generic;

namespace UnityProjectFramework.Core.UpdateLoop
{
    internal sealed class UpdateLoopRegistry
    {
        #region Fields & Constructor

        private readonly IteratorList<IUpdatable> _updatables;
        private readonly IteratorList<IFixedUpdatable> _fixedUpdatables;
        private readonly IteratorList<ILateUpdatable> _lateUpdatables;
        
        public IReadOnlyList<IUpdatable> Updatables => _updatables;
        public IReadOnlyList<IFixedUpdatable> FixedUpdatables => _fixedUpdatables;
        public IReadOnlyList<ILateUpdatable> LateUpdatables => _lateUpdatables;
        
        public bool HasRegistered =>
            _updatables.Count > 0 ||
            _fixedUpdatables.Count > 0 ||
            _lateUpdatables.Count > 0;

        internal UpdateLoopRegistry()
        {
            _updatables = new IteratorList<IUpdatable>();
            _fixedUpdatables = new IteratorList<IFixedUpdatable>();
            _lateUpdatables = new IteratorList<ILateUpdatable>();
        }
        
        #endregion

        internal bool Register(IManagedUpdatable managedUpdatable)
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

            return HasRegistered;
        }
        
        internal bool Unregister(IManagedUpdatable managedUpdatable)
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

            return HasRegistered;
        }

        internal void Clear()
        {
            _updatables.Clear();
            _fixedUpdatables.Clear();
            _lateUpdatables.Clear();
        }
    }
}