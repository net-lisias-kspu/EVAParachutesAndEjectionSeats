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
using System.Reflection;
using KSP.IO;

namespace VanguardTechnologies
{
    [Serializable]
    public class PartManipulation : IConfigNode
    {
        string name, module;
        bool floatInterpolation = false;
        FieldInfo target;
        public Dictionary<float, string> values = new Dictionary<float, string>();
        UnityEngine.AnimationCurve curve = new UnityEngine.AnimationCurve();
        char[] delimiters = { ',', ';', '\t', ':', '=' };

        public void Load(string fileName)
        {
            TextReader r = TextReader.CreateForType<PartManipulation>(fileName);
            string s;
            string[] split;
            while (!r.EndOfStream)
            {
                s = r.ReadLine();
                split = s.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                switch (split[0].ToLowerInvariant().Trim())
                {
                    case "name":
                        name = split[1];
                        break;
                    case "module":
                        module = split[1];
                        break;
                    case "floatinterpolation":
                        floatInterpolation = true;
                        break;
                    case "value":
                        values.Add(Convert.ToSingle(split[1]), split[2].Trim());
                        break;
                    default:
                        UnityEngine.Debug.Log("Invalid or comment line in PartManipulation: " + s);
                        break;
                }
            }
            if(floatInterpolation)
                foreach(float key in values.Keys)
                    curve.AddKey(key, float.Parse(values[key]));
        }

        public void Load(ConfigNode node)
        {
            UnityEngine.Debug.Log("PartManipulation loading");
            name = node.GetValue("name");
            module = node.GetValue("module");
            floatInterpolation = bool.Parse(node.GetValue("floatInterpolation"));
            foreach (string s in node.GetValues("value"))
            {
                string[] split = s.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                values.Add(System.Convert.ToSingle(split[0]), split[1]);
                UnityEngine.Debug.Log(values[Convert.ToSingle(split[0])]);
            }
            UnityEngine.Debug.Log("PartManipulation loaded");
        }

        public void Save(ConfigNode node)
        {
            UnityEngine.Debug.Log("PartManipulation saving");
            node.AddValue("name", name);
            node.AddValue("module", module);
            node.AddValue("floatInterpolation", floatInterpolation);
            foreach (float key in values.Keys)
                node.AddValue("value", key.ToString() + ":" + values[key]);
            UnityEngine.Debug.Log("PartManipulation saved");
        }

        public void ApplyToPart(Part p, float state)
        {
            //UnityEngine.Debug.Log("ApplyToPart");
            if (target == null)
            {
                if (module != null)
                    target = p.Modules[module].Fields[name].FieldInfo;
                else
                    target = p.GetType().GetField(name);
            }
            if (floatInterpolation)
            {
                //UnityEngine.Debug.Log("using interpolation");
                if (module == null)
                    target.SetValue(p, curve.Evaluate(state));
                else
                    target.SetValue(p.Modules[module], curve.Evaluate(state));
                return;
            }
            UnityEngine.Debug.Log("searching closest");
            float closest = float.PositiveInfinity;
            foreach (float k in values.Keys)
                if (Math.Abs(state - k) < Math.Abs(state - closest))
                    closest = k;
            UnityEngine.Debug.Log("applying");
            if (module == null)
                target.SetValue(p, System.Convert.ChangeType(values[closest], target.FieldType));
            else
                target.SetValue(p.Modules[module], System.Convert.ChangeType(values[closest], target.FieldType));
        }
    }
}