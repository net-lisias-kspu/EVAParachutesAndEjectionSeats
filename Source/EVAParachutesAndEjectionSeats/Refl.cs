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
using System.Reflection;

namespace VanguardTechnologies
{
    public static class Refl
    {
        public static FieldInfo GetField(object obj, int fieldNum)
        {
            int c = 0;
            foreach (FieldInfo FI in obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (c == fieldNum)
                    return FI;
                c++;
            }
            throw new Exception("No such field: " + obj.GetType() + "#" + fieldNum.ToString());
        }
        public static object GetValue(object obj, int fieldNum)
        {
            return GetField(obj, fieldNum).GetValue(obj);
        }
        public static void SetValue(object obj, int fieldNum, object value)
        {
            GetField(obj, fieldNum).SetValue(obj, value);
        }

		public static FieldInfo GetField(object obj, string name) {
			var f = obj.GetType().GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			if(f == null) throw new Exception("No such field: " + obj.GetType() + "#" + name);
			return f;
		}
		public static object GetValue(object obj, string name) {
			return GetField(obj, name).GetValue(obj);
		}
		public static void SetValue(object obj, string name, object value) {
			GetField(obj, name).SetValue(obj, value);
		}

        public static MethodInfo GetMethod(object obj, int methodnum)
        {
            MethodInfo[] m = obj.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            int c = 0;
            foreach (MethodInfo FI in m)
            {
                if (c == methodnum)
                    return FI;
                c++;
            }

            throw new Exception("No such method: " + obj.GetType() + "#" + methodnum);
        }
        public static object Invoke(object obj, int methodnum, params object[] args)
        {
            return GetMethod(obj, methodnum).Invoke(obj, args);
        }

		public static MethodInfo GetMethod(object obj, string name) {
			var m = obj.GetType().GetMethod(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			if(m == null) throw new Exception("No such method: " + obj.GetType() + "#" + name);
			return m;
		}
		public static object Invoke(object obj, string name, params object[] args) {
			return GetMethod(obj, name).Invoke(obj, args);
		}
    }
}