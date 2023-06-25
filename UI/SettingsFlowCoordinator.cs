using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;
using HMUI;
using UnityEngine;
using Zenject;

namespace PauseMusic.UI
{
	public class SettingsFlowCoordinator : FlowCoordinator
	{
		internal FlowCoordinator parentFlowCoordinator = null;
		private SettingsView _settingsViewController = null;

		[Inject]
		public void Construct(
			MainFlowCoordinator mainFlowCoordinator,
			SettingsView settingsViewController)
		{
			parentFlowCoordinator = mainFlowCoordinator;
			_settingsViewController = settingsViewController;
		}

		private void Start()
		{
			MenuButtons.instance.RegisterButton(
				new MenuButton("Pause Music", onClick: () =>
					{
						parentFlowCoordinator.PresentFlowCoordinator(this);
					}
				));
		}

		protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
		{
			if (firstActivation)
			{
				SetTitle("Pause Music");
				showBackButton = true;
			}
			if (addedToHierarchy)
			{
				ProvideInitialViewControllers(_settingsViewController, null, null);
			}
		}

		protected override void BackButtonWasPressed(ViewController topViewController)
		{
			parentFlowCoordinator.DismissFlowCoordinator(this);
		}
	}
}
