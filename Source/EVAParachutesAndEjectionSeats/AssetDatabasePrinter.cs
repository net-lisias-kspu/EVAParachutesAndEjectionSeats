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
using GUI = KSPe.UI.GUI;
using GUILayout = KSPe.UI.GUILayout;

namespace VanguardTechnologies
{
    [KSPAddon(KSPAddon.Startup.Settings, false)]
    class AssetDatabasePrinter : MonoBehaviour
    {
        public void OnGUI()
        {
            if (GUILayout.Button("Vanguard Technologies Asset Database Printer - save asset list to kspdir/assetlist.log"))
            {
                ConfigNode topNode = new ConfigNode("ASSETS");
                AssetBase assetBase = (AssetBase)UnityEngine.Object.FindObjectOfType(typeof(AssetBase));

                ConfigNode guiSkinNode = new ConfigNode("GUISKINS");
                foreach (GUISkin s in assetBase.guiSkins)
                    guiSkinNode.AddValue("objectName", s.name);

                ConfigNode prefabNode = new ConfigNode("PREFABS");
                if (assetBase != null)
                    foreach (GameObject o in assetBase.prefabs)
                     prefabNode.AddValue("objectName", o.name);

                ConfigNode textureNode = new ConfigNode("TEXTURES");
                if (assetBase != null)
                    foreach (Texture2D t in assetBase.textures)
                        textureNode.AddValue("objectName", t.name);

                ConfigNode unityResource = new ConfigNode("UNITYRESOURCES");
                int nameless = 0, unass = 0, newGameObject = 0;
                foreach (UnityEngine.Object o in UnityEngine.Resources.FindObjectsOfTypeAll(typeof(UnityEngine.Object)))
                    switch (o.name)
                    {
                        case "": nameless++; break;
                        case "Unass": unass++; break;
                        case "New Game Object": newGameObject++; break;
                        default:
                            unityResource.AddValue("objectName", o.name);
                            break;
                    }
                unityResource.AddValue("nameless", nameless);
                unityResource.AddValue("Unass", unass);
                unityResource.AddValue("NewGameObject", newGameObject);

                topNode.AddNode(guiSkinNode);
                topNode.AddNode(prefabNode);
                topNode.AddNode(textureNode);
                topNode.AddNode(unityResource);
                topNode.Save(KSPUtil.ApplicationRootPath + "/assetlist.log");
            }
        }
    }
}
