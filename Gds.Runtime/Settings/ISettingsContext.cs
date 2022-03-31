using System;

namespace Gds.Runtime.Settings
{
	public interface ISettingsContext
	{
		T GetSettingsCopy<T>() where T : class, ICloneable, new();
		T GetSettingsOriginal<T>() where T : class, ICloneable, new();
		void SetSettings<T>(T data) where T : class, ICloneable, new();

		void ClearSettings<T>() where T : class, ICloneable, new();
		//void ClearAllSettings();

		void SubscribeToSettingsUpdate<T>(SettingsChangedEventHandler<T> handler) where T : class, ICloneable, new();
		void UnsubscribeFromSettingsUpdate<T>(SettingsChangedEventHandler<T> handler) where T : class, ICloneable, new();
	}

	public delegate void SettingsChangedEventHandler<T>(T data);
}
