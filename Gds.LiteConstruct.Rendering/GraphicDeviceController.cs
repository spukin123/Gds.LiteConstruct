using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.Direct3D;
using System.Threading;
using System.Windows.Forms;
using System.Timers;
using Gds.Runtime;
using Gds.Runtime.Settings;
using System.Drawing;

namespace Gds.LiteConstruct.Rendering
{
    public class GraphicDeviceController
    {
        private Device device = null;
        private Thread renderThread = null;

        private RenderModeBase renderMode = null;

        public RenderModeBase CurentRenderMode
        {
            get { return renderMode; }
        }

        private bool pause = true;

        private bool Pause
        {
            get { return pause; }
            set { pause = value; }
        }

        public bool IsPaused
        {
            get { return pause; }
        }

        private int previousFps = 0;
        private int currentFps = 0;
        private System.Timers.Timer resetFpsTimer = null;

        public int FPS
        {
            get { return previousFps; }
        }

        public CameraBase Camera
        {
            get { return renderMode.Camera; }
        }

		private Color backColor;


        public GraphicDeviceController(Control control)
        {
			InitializeSettings();
            InitializeGraphics(control);
			
            control.SizeChanged += OnControlResize;
            device.DeviceLost += this.LostDevice;

            Form form = control.FindForm();
            form.SizeChanged += OnFormSizeChanged;

            renderThread = new Thread(Render);
            renderThread.Priority = ThreadPriority.Normal;
            renderThread.Start();
            renderThread.IsBackground = false;

            resetFpsTimer = new System.Timers.Timer(1000);
            resetFpsTimer.Elapsed += new System.Timers.ElapsedEventHandler(ResetFps);
            resetFpsTimer.Start();
        }

        private void InitializeGraphics(Control control)
        {
            PresentParameters presentParams = new PresentParameters();
            presentParams.Windowed = true;
            presentParams.SwapEffect = SwapEffect.Discard;
            presentParams.EnableAutoDepthStencil = true;
            presentParams.AutoDepthStencilFormat = DepthFormat.D24X8;
            presentParams.BackBufferCount = 1;
            presentParams.BackBufferFormat = Format.A8R8G8B8;
            
            device = new Device(0, DeviceType.Hardware, control, CreateFlags.SoftwareVertexProcessing, presentParams);
        }

		private void InitializeSettings()
		{
			ISettingsContext settingsContext = Gds.Runtime.AppContext.Get<ISettingsContext>();
			SceneSettings settings = settingsContext.GetSettingsCopy<SceneSettings>();
			ApplySettings(settings);
			settingsContext.SubscribeToSettingsUpdate<SceneSettings>(ApplySettings);
		}

		private void ApplySettings(SceneSettings settings)
		{
			backColor = settings.BackgroundColor;
		}

        public void SetRenderMode(RenderModeBase newRenderMode)
        {
            lock (device)
            {
                if (renderMode != null)
                    device.DeviceReset -= new EventHandler(renderMode.RestoreDeviceObjects);
                // TODO: Clear previous render mode
                this.renderMode = newRenderMode;
                newRenderMode.Device = device;
                device.DeviceReset += new EventHandler(newRenderMode.RestoreDeviceObjects);
                device.Disposing += new EventHandler(newRenderMode.DeleteDeviceObjects);
                
                newRenderMode.InitializeDeviceObjects();
                newRenderMode.RestoreDeviceObjects(device, null);
                Pause = false;
            }
        }

        private void OnControlResize(object sender, EventArgs e)
        {
            Pause = false;
        }

        private void OnFormSizeChanged(object sender, EventArgs e)
        {
            if ((sender as Form).WindowState == FormWindowState.Minimized)
                Pause = true;
            else
                Pause = false;
        }

        public void StopRendering()
        {
            if (renderThread.ThreadState == ThreadState.Running)
            {
                lock (device)
                {
                    renderThread.Abort();
                }
                renderThread.Join();
            }
        }

        private void Render()
        {
            while (true)
            {
                if (device == null) continue;
                if (Pause == true)
                {
                    Thread.Sleep(70);
                    continue;
                }
                lock (device)
                {
                    device.Clear(ClearFlags.ZBuffer | ClearFlags.Target, backColor, 1.0f, 0);
                    device.BeginScene();

                    if (renderMode != null)
                        renderMode.DoRender();

                    try
                    {
                        device.EndScene();
                        device.Present();
                    }
                    catch { }
                }
                currentFps++;
            }
        }

        private void LostDevice(object sender, EventArgs e)
        {
            lock (device)
            {
                pause = true;
            }
        }

        private void ResetFps(object sender, ElapsedEventArgs e)
        {
            previousFps = currentFps;
            currentFps = 0;
        }
    }
}
