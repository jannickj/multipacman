using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine.Data
{
    public class DictionaryList<TKey,TValue>
    {

        private Dictionary<TKey, HashSet<TValue>> dic = new Dictionary<TKey, HashSet<TValue>>();

        public ICollection<TValue> this[TKey key]
        {
            get
            {
                HashSet<TValue> vals;
                if(dic.TryGetValue(key,out vals))
                    return vals.ToArray();

                return new TValue[0];
            }
        }

        public ICollection<TValue> Get(TKey key)
        {
            return this[key];
        }

        public bool Add(TKey key, TValue val)
        {
            HashSet<TValue> vals;
            if (!dic.TryGetValue(key, out vals))
            {
                vals = new HashSet<TValue>();
                dic[key] = vals;
            }
            return vals.Add(val);
        }

        public bool Remove(TKey key, TValue val)
        {
            HashSet<TValue> vals;
            if (dic.TryGetValue(key, out vals))
            {
                if (vals.Remove(val))
                {
                    if (vals.Count == 0)
                        dic.Remove(key);
                    return true;
                }
            }
            return false;
        }
    }
}
