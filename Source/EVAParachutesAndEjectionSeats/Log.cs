/*
	This file is part of EVA Parachutes & Ejection Seats /L Unleashed
	© 2021-2022 LisiasT : http://lisias.net <support@lisias.net>

	THIS FILE is licensed to you under:

	* WTFPL - http://www.wtfpl.net
		* Everyone is permitted to copy and distribute verbatim or modified
			copies of this license document, and changing it is allowed as long
			as the name is changed.

	THIS FILE is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

*/
using System.Diagnostics;
using KSPe.Util.Log;

namespace VanguardTechnologies
{
	public static class Log
	{
		private static readonly Logger log = Logger.CreateForType<Startup> ();

		public static void force (string msg, params object [] @params)
		{
			log.force (msg, @params);
		}

		public static void info (string msg, params object [] @params)
		{
			log.info (msg, @params);
		}

		public static void warn (string msg, params object [] @params)
		{
			log.warn (msg, @params);
		}

		public static void detail (string msg, params object [] @params)
		{
			log.detail (msg, @params);
		}

		public static void trace (string msg, params object [] @params)
		{
			log.trace (msg, @params);
		}

		public static void error (string msg, params object [] @params)
		{
			log.error (msg, @params);
		}

		public static void ex (object offended, System.Exception e)
		{
			log.error(offended, e);
		}

		[ConditionalAttribute ("DEBUG")]
		public static void dbg (string msg, params object [] @params)
		{
			log.trace (msg, @params);
		}
	}
}
