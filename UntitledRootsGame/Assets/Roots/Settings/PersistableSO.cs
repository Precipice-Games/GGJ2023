using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Roots.Settings
{
	public class PersistableSO : MonoBehaviour
	{
		private const string SUFFIX = "default";
		[Header("Meta")] public string persistantName;
		[Header("Scriptable Objects")] public List<ScriptableObject> objectsToPersist;

		//public Settings settings;

		protected void Awake()
		{
			if (!File.Exists(Application.persistentDataPath + $"/{persistantName}_{0}_{SUFFIX}.pso"))
				Save(SUFFIX);
		}
		protected void OnEnable() => Load("saved");
		//Screen.fullScreen = settings.control.isFullscreen;
		protected void OnDisable() => Save("saved");

		public void ClearData(string suffix)
		{
			for (int i = 0; i < objectsToPersist.Count; i++)
			{
				File.Delete(Application.persistentDataPath + $"/{persistantName}_{i}_{suffix}.pso");
			}
			Load("default");
		}

		/*TODO call the next two methods whenever is appropriate
	SAVE: Call whenever the audio sliders are updated
	LOAD: Call whenever the audio sliders are intially loaded?*/

		public void Save(string suffix)
		{
			for (int i = 0; i < objectsToPersist.Count; i++)
			{
				var bf = new BinaryFormatter();
				FileStream file = File.Create(Application.persistentDataPath + $"/{persistantName}_{i}_{suffix}.pso");
				string json = JsonUtility.ToJson(objectsToPersist[i], true);
				bf.Serialize(file, json);
				file.Close();
			}
		}

		public void Load(string suffix)
		{
			for (int i = 0; i < objectsToPersist.Count; i++)
			{
				if (File.Exists(Application.persistentDataPath + $"/{persistantName}_{i}_{suffix}.pso"))
				{
					var bf = new BinaryFormatter();
					FileStream file = File.Open(
						Application.persistentDataPath + $"/{persistantName}_{i}_{suffix}.pso",
						FileMode.Open);
					//reassign scriptable objects to match json
					JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), objectsToPersist[i]);
					file.Close();
				}
				else
				{
					Load("default");
				}
			}
		}
	}
}
