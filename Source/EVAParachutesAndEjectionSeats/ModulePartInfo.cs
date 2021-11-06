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

namespace VanguardTechnologies
{
    public class ModuleKrPartInfo : PartModule
    {
        bool alreadyPrintedInternal = false;
        public override void OnStart(PartModule.StartState state)
        {
            Log.Info("--- PART INFO ---");

            Log.Info("fxgroups:");

            foreach (FXGroup f in part.fxGroups)
                Log.Info(f.name);

            Log.Info("Attach nodes:");

            foreach (AttachNode n in part.attachNodes)
                Log.Info(n.id);
            Log.Info("--- MODEL INFO ---");

            Log.Info("Animations:");
            foreach (Animation a in part.FindModelAnimators())
            {

                Log.Info("* " + a.name);

                foreach (AnimationState s in a)
                    Log.Info("** " + s.name);
            }

            Log.Info("Transforms:");

            printTransforms(part.transform);


            Log.Info("--- END OF MODEL INFO ---");
        }

        public override void OnUpdate()
        {
            if (part.internalModel != null && !alreadyPrintedInternal)
            {
                Log.Info("--- INTERNAL INFO ---");
                Log.Info("Internal transforms:");

                printTransforms(part.internalModel.transform);
                alreadyPrintedInternal = true;
                Log.Info("--- END OF INTERNAL INFO ---");
            }
        }
        public static void printTransforms(Transform t, string prefix = "")
        {
            Log.Info(prefix + t.name);
            prefix += "*";
            for (int i = 0; i < t.childCount; i++)
                printTransforms(t.GetChild(i), prefix);
        }
    }
}
