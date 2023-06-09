using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauseMusic.Configuration
{
	public class PluginConfig
	{
		public static PluginConfig Instance
		{
			get;
			set;
		}

		public virtual bool enabled 
		{ 
			get; 
			set; 
		} = true;

		public virtual float fadeSpeed
		{
			get;
			set;
		} = 3f;

		public virtual float audioVolume
		{
			get;
			set;
		} = 1f;
	}
}
