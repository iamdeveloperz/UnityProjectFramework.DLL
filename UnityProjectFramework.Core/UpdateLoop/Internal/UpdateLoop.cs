
namespace UnityProjectFramework.Core.UpdateLoop
{
    internal sealed class UpdateLoop
    {
        #region Fields & Constructor

        private static UpdateLoop _sInstance;

        internal static UpdateLoop Instance => (ApplicationUtility.IsQuit)
            ? _sInstance
            : _sInstance = Create();

        private readonly UpdateLoopRunner _runner;
        private readonly UpdateLoopRegistry _registry;

        internal UpdateLoop()
        {
            var factory = new UpdatableFactory();
            
            _registry = new UpdateLoopRegistry();
            _runner = factory.CreateRunner(_registry);
        }

        #endregion

        public void Register(IManagedUpdatable managedUpdatable)
        {
            var isEnabled = _registry.Register(managedUpdatable);
            _runner.enabled = isEnabled;
        }
        
        public void Unregister(IManagedUpdatable managedUpdatable)
        {
            var isEnabled = _registry.Unregister(managedUpdatable);
            _runner.enabled = isEnabled;
        }

        #region Create Instance

        private static UpdateLoop Create()
        {
            return new UpdateLoop();
        }

        #endregion
    }
}