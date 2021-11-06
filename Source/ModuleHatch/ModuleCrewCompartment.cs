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
using System.Collections.Generic;
using System.Linq;

namespace VanguardTechnologies
{
    public class ModuleKrCrewCompartment : PartModule
    {
        public bool HasFreeSeat
        {
            get
            {
                return part.CrewCapacity > part.protoModuleCrew.Count;
            }
        }

        [KSPField(guiActive = true, guiName = "Crew count")]
        public float crewCount_GUI;
        [KSPField(guiActive = true, guiName = "Crew capacity")]
        public float crewCapacity_GUI;
        [KSPField(guiActive = true, guiName = "Crew names")]
        public string crewNames_GUI;

        [KSPEvent(guiActive = true, guiName = "Start EVA")]
        public void StartEVA()
        {
            ModuleKrHatch h = ModuleKrHatch.GetHatches(vessel).FirstOrDefault(x => x.isActiveHatch);
            if(part.protoModuleCrew.Count == 0)
                ScreenMessages.PostScreenMessage("Crew compartment is empty", 3, ScreenMessageStyle.UPPER_CENTER);
            else if (h == null)
                ScreenMessages.PostScreenMessage("No hatch is activated", 3, ScreenMessageStyle.UPPER_CENTER);
            else
            {
                ProtoCrewMember m = part.protoModuleCrew[0];
                part.RemoveCrewmember(m);
                h.part.AddCrewmember(m);
                FlightEVA.fetch.spawnEVA(m, h.part, h.part.airlock);
            }
        }

        public override void OnUpdate()
        {
            crewCount_GUI = part.protoModuleCrew.Count;
            crewNames_GUI = string.Join(", ", part.protoModuleCrew.Select(p => p.name.Split(' ')[0]).ToArray());
        }
        public override void OnStart(PartModule.StartState state)
        {
            crewCapacity_GUI = part.CrewCapacity;
        }

        public static List<ModuleKrCrewCompartment> GetCompartments(Vessel v)
        {
            List<ModuleKrCrewCompartment> cList = new List<ModuleKrCrewCompartment>();
            foreach (Part p in v.parts)
                foreach (PartModule pm in p.Modules)
                    if (pm is ModuleKrCrewCompartment)
                        cList.Add((ModuleKrCrewCompartment)pm);
            return cList;
        }
    }
}
