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
    public class ModuleKrHatch : PartModule
    {
        [KSPField(isPersistant = true, guiActive = true, guiActiveEditor = true, guiName = "Hatch active")]
        public bool isActiveHatch = false;

        [KSPEvent(guiActive = true, guiName = "Activate hatch")]
        public void ActivateHatch()
        {
            foreach (ModuleKrHatch h in GetHatches(vessel))
                h.isActiveHatch = false;
            this.isActiveHatch = true;
        }

        [KSPEvent(guiActive = true, guiName = "Start EVA")]
        public void StartEVA()
        {
            ModuleKrCrewCompartment c = ModuleKrCrewCompartment.GetCompartments(vessel).FirstOrDefault(x => x.part.protoModuleCrew.Count>0);
            if (c == null)
                ScreenMessages.PostScreenMessage("No crew compartment with crew found", 3, ScreenMessageStyle.UPPER_CENTER);
            else
            {
                ProtoCrewMember m = c.part.protoModuleCrew[0];
                c.part.RemoveCrewmember(m);
                part.AddCrewmember(m);
                FlightEVA.fetch.spawnEVA(m, part, part.airlock);
            }
        }

        public override void OnUpdate()
        {
            if (part.protoModuleCrew.Count > 0)
            {
                List<ModuleKrCrewCompartment> clist = ModuleKrCrewCompartment.GetCompartments(vessel);
                foreach (ModuleKrCrewCompartment c in clist)
                    if (c.HasFreeSeat)
                    {
                        ProtoCrewMember cmember = part.protoModuleCrew[0];
                        part.RemoveCrewmember(cmember);
                        c.part.AddCrewmember(cmember);
                        vessel.SpawnCrew();
                        break;
                    }
            }

            if (vessel.isActiveVessel)
                part.CrewCapacity = 1;
            else
                part.CrewCapacity = ModuleKrCrewCompartment.GetCompartments(vessel).Any(c => c.HasFreeSeat) ? 1 : 0;

        }

        public override void OnStart(PartModule.StartState state)
        {
            part.CrewCapacity = 0; //WhyTF does this not remove crew spaces in the editor?
        }

        public static List<ModuleKrHatch> GetHatches(Vessel v)
        {
            List<ModuleKrHatch> hList = new List<ModuleKrHatch>();
            foreach (Part p in v.parts)
                foreach (PartModule pm in p.Modules)
                    if (pm is ModuleKrHatch)
                        hList.Add((ModuleKrHatch)pm);
            return hList;
        }
    }
}
#endif