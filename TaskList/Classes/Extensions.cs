using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskList.Classes
{
        public static class Extension
        {
            public static void InvokeIfRequired(this ISynchronizeInvoke iObject, MethodInvoker iAction)
            {
                try
                {
                    if (iObject is Control)
                    {
                        if ((iObject as Control).IsDisposed || (iObject as Control).Disposing)
                            return;
                    }
                    if (iObject.InvokeRequired)
                    {
                        var args = new object[0];
                        if (iObject is Control)
                        {
                            if ((iObject as Control).IsDisposed || (iObject as Control).Disposing)
                                return;
                        }
                        iObject.Invoke(iAction, args);
                    }
                    else
                    {
                        iAction();
                    }
                }
                catch (Exception e)
                {
                    
                }
            }

            public static bool IsEqualTo<TKey, TValue>(
                this IDictionary<TKey, TValue> first,
                IDictionary<TKey, TValue> second,
                IEqualityComparer<TValue> valueComparer = null)
            {
                // Quick reference checks
                if (ReferenceEquals(first, second)) return true;
                if (first == null || second == null) return false;

                // Different number of entries → not equal
                if (first.Count != second.Count) return false;

                // Use the supplied comparer or the default one
                var comparer = valueComparer ?? EqualityComparer<TValue>.Default;

                // Compare each key/value pair
                foreach (var kvp in first)
                {
                    if (!second.TryGetValue(kvp.Key, out var secondValue))
                        return false; // key missing

                    if (!comparer.Equals(kvp.Value, secondValue))
                        return false; // value differs
                }

                return true;
            }
        }
}
