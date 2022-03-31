using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.LiteConstruct.Core.Controllers
{
	public delegate void ModelCreatedEventHandler();
	public delegate void ControllerActivatedEventHandler();
    public delegate void PrimitiveAddedEventHandler();
    public delegate void RenderModeControlUpdatedEventHandler(RenderModeControlState state);
    public delegate void NotifyEventHandler();
    public delegate void StateEventHandler(bool state);
}
