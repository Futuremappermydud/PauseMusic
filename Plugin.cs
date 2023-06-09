using IPA;
using IPA.Config;
using IPA.Config.Stores;
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
		public Plugin(IPALogger logger, Zenjector zenjector)
		{
			Instance = this;
			Plugin.Log = logger;

			zenjector.Install<PauseMusicGameInstaller>(Location.StandardPlayer);
		}
	}
}
