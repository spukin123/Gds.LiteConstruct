using Gds.LiteConstruct.PrimitivesManagement.AxisBindings.BindingAxisControllerManagement;
using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.BusinessObjects.Axises;

namespace Gds.LiteConstruct.PrimitivesManagement.AxisBindings.BindingAxisControllerManagement
{
    internal interface IBindingAxisControllerPresenter : IDisposable
    {
        bool Interacted(int x, int y);

        void MakeActive();
        void MakeUnactive();
        void Hide();
        void Show();
        void Render();

        void EnableTransactionMode();
    }
}
