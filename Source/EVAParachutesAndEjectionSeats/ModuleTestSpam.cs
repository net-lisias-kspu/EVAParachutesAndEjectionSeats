/*
	This file is part of EVA Parachutes & Ejection Seats /L Unleashed
		© 2021 Lisias T : http://lisias.net <support@lisias.net>
		© 2016-2021 LinxGuruGamer
		© 2013-2015 Kreuzung

	EVA Parachutes & Ejection Seats /L is double licensed, as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt
		* GPL 2.0 : https://www.gnu.org/licenses/gpl-2.0.txt

	And you are allowed to choose the License that better suit your needs.

	EVA Parachutes & Ejection Seats /L Unleashed is distributed in the
	hope that it will be useful, but WITHOUT ANY WARRANTY; without even
	the implied warranty of	MERCHANTABILITY or FITNESS FOR A PARTICULAR
	PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with EVA Parachutes & Ejection Seats /L Unleashed.
	If not, see <https://ksp.lisias.net/SKL-1_0.txt>.

	You should have received a copy of the GNU General Public License 2.0
	along with EVA Parachutes & Ejection Seats /L Unleashed.
	If not, see <https://www.gnu.org/licenses/>.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Shows when which override of PartModule is called
/// </summary>
namespace VanguardTechnologies
{
    class ModuleKrTestSpam : PartModule
    {
        public override void OnActive()
        {
            Log.Info("OnActive");
        }
        public override void OnAwake()
        {
            Log.Info("OnAwake");
        }
        public override void OnInactive()
        {
            Log.Info("OnInactive");
        }
        public override void OnLoad(ConfigNode node)
        {
            Log.Info("OnLoad");
        }
        public override void OnFixedUpdate()
        {
            Log.Info("OnFixedUpdate");
        }
        public override void OnUpdate()
        {
            Log.Info("OnUpdate");
        }
        public override void OnSave(ConfigNode node)
        {
            Log.Info("OnSave");
        }
        public override void OnStart(StartState state)
        {
            Log.Info("OnStart:" + state);
        }
    }
}