using IPA;
using IPA.Config;
using IPA.Config.Stores;
using PauseMusic.Configuration;
using PauseMusic.Installers;
using SiraUtil.Zenject;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;

namespace PauseMusic
{
	[Plugin(RuntimeOptions.SingleStartInit)]
	public class Plugin
	{
		internal static Plugin Instance { get; private set; }
		internal static IPALogger Log { get; private set; }

		[Init]
		public Plugin(IPALogger logger, Config conf, Zenjector zenjector)
		{
			Instance = this;
			Log = logger;
			PluginConfig.Instance = GeneratedStore.Generated<PluginConfig>(conf, true);

			zenjector.Install<PauseMusicGameInstaller>(Location.StandardPlayer);
		}
	}
}
