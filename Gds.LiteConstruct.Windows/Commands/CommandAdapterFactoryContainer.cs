using System;
using System.Collections.Generic;
using System.Text;
using Gds.LiteConstruct.Windows.Commands.Adapters;

namespace Gds.LiteConstruct.Windows.Commands
{
    public static class CommandAdapterFactoryContainer
    {
        private static Dictionary<Type, ICommandAdapterFactory> factories = new Dictionary<Type, ICommandAdapterFactory>();

        static CommandAdapterFactoryContainer()
        {
            factories.Add(typeof(System.Windows.Forms.ToolStripMenuItem), new ToolStripMenuItemAdapterFactory());
            factories.Add(typeof(System.Windows.Forms.Button), new ButtonAdapterFactory());
			factories.Add(typeof(System.Windows.Forms.ToolStripDropDownButton), new ToolStripDropDownButtonAdapterFactory());
            factories.Add(typeof(System.Windows.Forms.ToolStripButton), new CheckedToolStripButtonAdapterFactory());
        }

        public static ICommandAdapter CreateAdapter(object invoker)
        {
            ICommandAdapterFactory factory;
            if (!factories.TryGetValue(invoker.GetType(), out factory))
            {
                throw new ApplicationException(
                    string.Format(Gds.LiteConstruct.Windows.Properties.Resources.AdapterForType0HasNotBeenRegistered, invoker.GetType().ToString()));
            }

            ICommandAdapter toReturn = factory.Create();
            toReturn.SetInvoker(invoker);

            return toReturn;
        }

        public static void Register(Type invokerType, ICommandAdapterFactory factory)
        {
            if (factories.ContainsKey(invokerType))
            {
                throw new ArgumentException(
                    string.Format(Gds.LiteConstruct.Windows.Properties.Resources.AdapterForType0HasBeenAlreadyRegistered, invokerType.ToString()));
            }

            factories.Add(invokerType, factory);
        }

        public static void Clear()
        {
            factories.Clear();
        }
    }
}
