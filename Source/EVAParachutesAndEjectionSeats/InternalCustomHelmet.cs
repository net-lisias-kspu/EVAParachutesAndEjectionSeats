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
using UnityEngine;

namespace VanguardTechnologies
{
    class InternalKrCustomHelmet : InternalModule
    {
        [KSPField]
        public string modelPath = "Squad/Parts/Electrical/RTG/model" /*haha*/, modelName = "customHelmet", transformPath = "kbIVA@idle/globalMove01/joints01/bn_spA01/bn_spB01/bn_spc01/bn_spD01/be_spE01/bn_neck01";
        [KSPField]
        public UnityEngine.Vector3 localPosition = UnityEngine.Vector3.zero, scale = UnityEngine.Vector3.one, localRotation = UnityEngine.Vector3.zero;
        [KSPField]
        public bool debugTransformPath = false;


        public override void OnUpdate()
        {
            foreach (Kerbal k in internalModel.transform.GetComponentsInChildren<Kerbal>())
                if (!k.transform.Find(transformPath + "/" + modelName))
                {
                    GameObject helmet = GameDatabase.Instance.GetModel(modelPath);
                    helmet.transform.name = modelName;
                   // UIPartActionController.SetLayerRecursive(helmet.transform, 16);
                    helmet.SetActive(true);
                    helmet.transform.parent = k.transform.Find(transformPath);
                    if (debugTransformPath)
                        CheckTheF___ingTransforms(k.transform, transformPath);
                    Log.dbg("{0}", helmet.transform.parent);
                    helmet.transform.localPosition = localPosition;
                    helmet.transform.localScale = scale;
                    helmet.transform.localRotation = Quaternion.Euler(localRotation);
                }
        }

        void CheckTheF___ingTransforms(Transform t, string path)
        {
            foreach (Transform c in t)
            {
                Log.dbg("{0}", c.name);
                if (path.StartsWith(c.name))
                {
                    CheckTheF___ingTransforms(c, path.Remove(0, path.IndexOf('/') + 1));
                    return;
                }
            }
            Log.dbg("---END OF TRANSFORM CHECK---");
        }
    }
}
