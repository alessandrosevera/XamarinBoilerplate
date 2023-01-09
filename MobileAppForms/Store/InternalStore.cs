using System;
using System.Linq;
using VuexSharp;

namespace MobileAppForms.Store
{
    public class InternalStore : Store<InternalState>
    {
        #region auto-properties

        //modules
        public AppContextModule AppContextModule { get; private set; }
        public ProfileModule ProfileModule { get; private set; }

        private MutationFactory MutationFactory { get; }
        private ActionFactory ActionFactory { get; }

        #endregion

        #region ctor(s)

        public InternalStore(MutationFactory mutationFactory, ActionFactory actionFactory)
        {
            MutationFactory = mutationFactory;
            ActionFactory = actionFactory;

            State = new InternalState(this);

        }

        #endregion

        #region abstract methods implementation

        public override void InitializeStore()
        {
            BindModules();
            BootstrapModules();
        }

        protected override void BindModules()
        {
            AppContextModule = new AppContextModule(this, MutationFactory, ActionFactory);
            ProfileModule = new ProfileModule(this, MutationFactory, ActionFactory);

            Modules = new VuexSharp.Core.Module[] { AppContextModule, ProfileModule };

        }

        public override void InitState()
        {
            if (!(Modules is null) && Modules.Any())
            {
                foreach (var module in Modules)
                {
                    module.CreateState();
                }
            }
            State = new InternalState(this);
        }

        #endregion
    }
}
