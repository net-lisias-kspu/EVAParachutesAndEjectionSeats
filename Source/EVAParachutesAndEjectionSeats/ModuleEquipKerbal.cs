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

#if false
namespace VanguardTechnologies
{
    public class ModuleKrEquipKerbal : PartModule
    {
        [KSPField]
        public int range = 1;

        [KSPField]
        public string guiName = "Equip";

        [KSPField(guiActive = true, guiName = "Available", isPersistant = true)]
        public float count = 10;

        public ConfigNode moduleNode = null;

        public override void OnStart(PartModule.StartState state)
        {
            Events["EquipNearbyKerbal"].unfocusedRange = range;
            if (count < 0)
                Fields["count"].guiActive = false;
            Events["EquipNearbyKerbal"].guiName = guiName;
        }

        [KSPEvent(guiActive = true, externalToEVAOnly = true, guiActiveUnfocused = true, unfocusedRange = 1)]
        void EquipNearbyKerbal()
        {
            if (!FlightGlobals.ActiveVessel.isEVA || FlightGlobals.ActiveVessel.rootPart.Modules.Contains(moduleNode.GetValue("name")) || count == 0)
                return;
            else
            {
                FlightGlobals.ActiveVessel.rootPart.AddModule(moduleNode);
                FlightGlobals.ActiveVessel.rootPart.Modules[moduleNode.GetValue("name")].OnStart(PartModule.StartState.Flying);
                if (count > 0) count--;
            }
        }

        public override void OnLoad(ConfigNode node)
        {
            if (node.HasNode("MODULE"))
                moduleNode = node.GetNode("MODULE");
        }
    }
}
#endif