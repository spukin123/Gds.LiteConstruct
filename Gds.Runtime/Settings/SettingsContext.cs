using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.Runtime.Settings
{
	public sealed class SettingsContext : ISettingsContext
	{
		private ISettingsSerializer serializer;
		
		public SettingsContext(ISettingsSerializer serializer)
		{
			this.serializer = serializer;
		}

		public SettingsContext(string workFolder)
		{
			this.serializer = new SettingsSerializer(workFolder);
		}

		private Dictionary<Type, object> settings = new Dictionary<Type, object>();

		public T GetSettingsOriginal<T>() where T : class, ICloneable, new()
		{
			T result;
			if (settings.ContainsKey(typeof(T)))
			{
				result = (T)settings[typeof(T)];
			}
			else
			{
				result = serializer.Load<T>();
				if (result == null)
					result = Activator.CreateInstance<T>();
				settings.Add(typeof(T), result);
			}
			return result;
		}

		public T GetSettingsCopy<T>() where T : class, ICloneable, new()
		{
			T result;
			if (settings.ContainsKey(typeof(T)))
			{
				result = (T)settings[typeof(T)];
			}
			else
			{
				result = serializer.Load<T>();
				if (result == null)
					result = Activator.CreateInstance<T>();
				settings.Add(typeof(T), result);
			}
			return (T)result.Clone();
		}

		public void SetSettings<T>(T data) where T : class, ICloneable, new()
		{
			serializer.Save<T>(data);
			settings[typeof(T)] = data;
			InvokeSubscribers<T>(data);
		}

		public void ClearSettings<T>() where T : class, ICloneable, new()
		{
			settings.Remove(typeof(T));
			serializer.Clear<T>();
			InvokeSubscribers<T>(Activator.CreateInstance<T>());
		}

		private Dictionary<Type, List<Delegate>> subscribers = new Dictionary<Type, List<Delegate>>();

		public void SubscribeToSettingsUpdate<T>(SettingsChangedEventHandler<T> handler) where T : class, ICloneable, new()
		{
			if (!subscribers.ContainsKey(typeof(T)))
				subscribers[typeof(T)] = new List<Delegate>();
			subscribers[typeof(T)].Add(handler);
		}

		public void UnsubscribeFromSettingsUpdate<T>(SettingsChangedEventHandler<T> handler) where T : class, ICloneable, new()
		{
			if (subscribers.ContainsKey(typeof(T)))
			{
				List<Delegate> handlers = subscribers[typeof(T)];
				handlers.Remove(handler);
			}
		}

		private void InvokeSubscribers<T>(T data) where T : class, ICloneable, new()
		{
			if (subscribers.ContainsKey(typeof(T)))
			{
				List<Delegate> handlers = subscribers[typeof(T)];
				foreach (Delegate handler in handlers)
				{
					((SettingsChangedEventHandler<T>)handler)(data);
				}
			}
		}
	}
}
