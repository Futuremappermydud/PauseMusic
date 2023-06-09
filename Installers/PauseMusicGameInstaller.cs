using PauseMusic.Managers;
using PauseMusic.Services;
using Zenject;
using SiraUtil;

namespace PauseMusic.Installers
{
	internal class PauseMusicGameInstaller : Installer
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<AudioLoaderService>().AsSingle().NonLazy();
			Container.BindInterfacesAndSelfTo<PauseAudioManager>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
		}
	}
}
