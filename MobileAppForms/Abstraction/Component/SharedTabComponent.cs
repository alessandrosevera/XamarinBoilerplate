using System;
using System.Threading.Tasks;
using VueSharp;
using VueSharp.Abstraction;

namespace MobileAppForms.Abstraction
{
    public abstract class SharedTabComponent<TState> : TabComponent<TState>
    {
        public new Presenter Presenter => PassedPresenter;

        public Presenter PassedPresenter { get; private set; }

        public void InsertPresenter(Presenter presenter)
        {
            PassedPresenter = presenter;
        }

        protected override Presenter CreatePresenter()
        {
            return PassedPresenter;
        }
    }
}
