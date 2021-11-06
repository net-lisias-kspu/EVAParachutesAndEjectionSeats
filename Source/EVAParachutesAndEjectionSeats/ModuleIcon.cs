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

namespace VanguardTechnologies
{
    public class ModuleKrIcon : PartModule
    {
        [KSPField]
        public string icon = "icons.png", grouping = "none";

        [KSPField] //Using float because int didn't work in .16, maybe it does in .17 though... fixing this up in .19.1 lol
        public int x = 0, y = 0;

        public override void OnStart(PartModule.StartState state)
        {
            part.stackIcon.SetIcon(icon, x, y);
            switch (grouping.ToLowerInvariant())
            {
                case "none":
                    part.stackIconGrouping = StackIconGrouping.NONE;
                    break;
                case "same_module":
                    part.stackIconGrouping = StackIconGrouping.SAME_MODULE;
                    break;
                case "same_hierarchy":
                    part.stackIconGrouping = StackIconGrouping.SAME_HIERARCHY;
                    break;
                case "same_type":
                    part.stackIconGrouping = StackIconGrouping.SAME_TYPE;
                    break;
                case "sym_counterparts":
                    part.stackIconGrouping = StackIconGrouping.SYM_COUNTERPARTS;
                    break;
            }
        }
        void Update()
        {
            if (HighLogic.LoadedSceneIsEditor)
                part.stackIcon.SetIcon(icon, x, y);
        }
    }
}
