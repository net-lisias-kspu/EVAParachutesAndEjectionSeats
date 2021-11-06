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
using UnityEngine;

#if false
namespace VanguardTechnologies
{
    class ModuleKrSpawnHeight : PartModule
    {
        [KSPField]
        public float height = 0, setOrbit = 0;

        public void OnPutToGround(PartHeightQuery q)
        {
            Log.Info(q.lowestPoint.ToString());
            q.lowestPoint = -height;
            Log.Info(q.lowestPoint.ToString());
        }

        public override void OnStart(PartModule.StartState state)
        {
            if ((state & StartState.PreLaunch) != StartState.PreLaunch) return;
            if (setOrbit > 0)
                Invoke("SetOrbit", 1);
        }

        private void SetOrbit()
        {
            if (vessel.HoldPhysics)
            {
                Invoke("SetOrbit", 1);
                Log.Info("fail");
                return;
            }
            vessel.Landed = false;
            vessel.GoOnRails();
            double v = Math.Sqrt(vessel.mainBody.gravParameter / (vessel.mainBody.Radius + vessel.orbit.altitude));
            vessel.orbit.vel = Vector3.Cross(vessel.orbit.pos, new Vector3(0, 0, -1)).normalized * (float)v;
            vessel.GoOffRails();
            Log.Info("success");
        }
    }
}
#endif