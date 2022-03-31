using System;

namespace Gds.Runtime.Settings
{
	public interface ISettingsSerializer
	{
		T Load<T>() where T : class;
		void Save<T>(T data) where T : class;
		void Clear<T>() where T : class;
	}
}
