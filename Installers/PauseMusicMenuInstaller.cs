using PauseMusic.Managers;
using PauseMusic.Services;
using PauseMusic.UI;
using Zenject;
using SiraUtil;

namespace PauseMusic.Installers
{
	internal class PauseMusicMenuInstaller : Installer
	{
		public override void InstallBindings()
		{
			Container.Bind<UI.SettingsFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
			Container.Bind<SettingsView>().FromNewComponentAsViewController().AsSingle().NonLazy();
		}
	}
}