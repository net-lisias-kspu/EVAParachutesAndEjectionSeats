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
using UnityEngine;

namespace VanguardTechnologies
{
    public class ModuleKrPartInfo : PartModule
    {
        bool alreadyPrintedInternal = false;
        public override void OnStart(PartModule.StartState state)
        {
            Log.detail("--- PART INFO ---");

            Log.detail("fxgroups:");

            foreach (FXGroup f in part.fxGroups)
                Log.detail(f.name);

            Log.detail("Attach nodes:");

            foreach (AttachNode n in part.attachNodes)
                Log.detail(n.id);
            Log.detail("--- MODEL INFO ---");

            Log.detail("Animations:");
            foreach (Animation a in part.FindModelAnimators())
            {

                Log.detail("* " + a.name);

                foreach (AnimationState s in a)
                    Log.detail("** " + s.name);
            }

            Log.detail("Transforms:");

            printTransforms(part.transform);


            Log.detail("--- END OF MODEL INFO ---");
        }

        public override void OnUpdate()
        {
            if (part.internalModel != null && !alreadyPrintedInternal)
            {
                Log.detail("--- INTERNAL INFO ---");
                Log.detail("Internal transforms:");

                printTransforms(part.internalModel.transform);
                alreadyPrintedInternal = true;
                Log.detail("--- END OF INTERNAL INFO ---");
            }
        }
        public static void printTransforms(Transform t, string prefix = "")
        {
            Log.detail("{0}{1}", prefix, t.name);
            prefix += "*";
            for (int i = 0; i < t.childCount; i++)
                printTransforms(t.GetChild(i), prefix);
        }
    }
}
