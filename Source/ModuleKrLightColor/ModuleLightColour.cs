/*
	This file is part of EVA Parachutes & Ejection Seats /L Unleashed
		© 2021-2022 Lisias T : http://lisias.net <support@lisias.net>
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
using UnityEngine;

namespace VanguardTechnologies
{
    public class ModuleKrLightColor : PartModule
    {
        [KSPField(isPersistant = false)]
        public string lightName, emissiveName;

        private List<Light> lights;
        private List<Renderer> emissives;
        private ColorPickerWindow _win;
        private ColorPickerWindow win { get { return _win ?? (_win = ColorPickerWindow.CreateWindow("Light Colour", new Color(1,1,1,1))); } }

        public override void OnStart(PartModule.StartState state)
        {
            lights = part.FindModelComponents<Light>(lightName).ToList();
            Log.detail("[{0}] Lights found: {1}", GetType().Name, lights.Count);

            emissives = part.FindModelComponents<Renderer>(emissiveName).ToList();
            Log.detail("[{0}] Emissives found: {1}", GetType().Name, emissives.Count);
        }

        public override void OnLoad(ConfigNode node)
        {
            try
            {
                ConfigNode colourNode = node.GetNode("COLOUR") ?? node.GetNode("COLOR");
                if (colourNode == null) return;
                win.color = new Color(float.Parse(colourNode.GetValue("r")), float.Parse(colourNode.GetValue("g")), float.Parse(colourNode.GetValue("b")));
            }
            catch (Exception e)
            {
                win.color = new Color(1, 1, 1, 1);
                Log.error("[{0}] FAILED TO LOAD COLOUR", GetType().Name);
                Log.ex(this,e);
            }
        }

        public override void OnSave(ConfigNode node)
        {
            ConfigNode colourNode = new ConfigNode("COLOR");
            colourNode.AddValue("r", win.color.r);
            colourNode.AddValue("g", win.color.g);
            colourNode.AddValue("b", win.color.b);
            node.AddNode(colourNode);
        }

        public void Update()
        {
            if (HighLogic.LoadedSceneIsEditor)
            {
                if (!win.IsVisible() && GameSettings.HEADLIGHT_TOGGLE.GetKey())
                {
                    RaycastHit r;
                    Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out r);
                    if (Part.FromGO(r.transform.gameObject) == part)
                        win.Show();
                }
            }
            lights.ForEach(l => l.color = win.color);
            Color emissiveClr = new Color(win.color.r, win.color.g, win.color.b);
            emissives.ForEach(e => e.material.SetColor("_EmissiveColor", emissiveClr));

        }

        [KSPEvent(guiActiveEditor = true, guiActive = true, guiName = "Show Colour Picker", guiActiveUnfocused = true, externalToEVAOnly = true, unfocusedRange = 5)]
        public void ShowWindow() { win.Show(); }

        public void OnDestroy() { Destroy(_win); }
    }
}
