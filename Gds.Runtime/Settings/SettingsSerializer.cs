using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Gds.Runtime.Settings
{
	public class SettingsSerializer : ISettingsSerializer
	{
		private string workFolder;

		public SettingsSerializer(string workFolder)
		{
			Guard.ArgumentNotNullOrEmptyString(workFolder, "workFolder");
			Guard.CheckArgumentValid<string>(Path.IsPathRooted, workFolder, "workFolder");
			
			this.workFolder = workFolder;
			if (!Directory.Exists(workFolder))
				Directory.CreateDirectory(workFolder);
		}

		private object locker = new object();

		public void Save<T>(T data) where T : class
		{
			Guard.ArgumentNotNull(data, "data");
			string name = GetName<T>();

			lock (locker)
			{
				using (FileStream stream = new FileStream(GetFilePath(name), FileMode.Create))
				{
					BinaryFormatter formatter = new BinaryFormatter();
					formatter.Serialize(stream, data);
				}
			}
		}

		public T Load<T>() where T : class
		{
			string name = GetName<T>();
			string filePath = GetFilePath(name);
			if (!File.Exists(filePath))
				return null;

			lock (locker)
			{
				using (FileStream stream = new FileStream(filePath, FileMode.Open))
				{
					BinaryFormatter formatter = new BinaryFormatter();
					return (T)formatter.Deserialize(stream);
				}
			}
		}

		public void Clear<T>() where T : class
		{
			string filePath = GetFilePath(GetName<T>());
			if (File.Exists(filePath))
				File.Delete(filePath);
		}

		private string GetName<T>()
		{
			Type type = typeof(T);
			object[] attrs = type.GetCustomAttributes(typeof(SettingsAttribute), false);
			if (attrs.Length > 0)
			{
				SettingsAttribute attr = (SettingsAttribute)attrs[0];
				return attr.Name;
			}
			else
			{
				return type.Name;
			}
		}

		private string GetFilePath(string name)
		{
			return Path.Combine(workFolder, name);
		}
	}
}
