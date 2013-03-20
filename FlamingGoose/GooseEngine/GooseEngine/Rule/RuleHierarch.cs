using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooseEngine.Rule
{
    public class RuleHierarch<P,T>
    {
        private LinkedList<KeyValuePair<P, TransformationRule<T>>> hiarch = new LinkedList<KeyValuePair<P,TransformationRule<T>>>();
        private Dictionary<P, TransformationRule<T>> lookup = new Dictionary<P, TransformationRule<T>>();

        public void AddLayer(P priority, TransformationRule<T> t)
        {
            hiarch.AddFirst(new KeyValuePair<P, TransformationRule<T>>(priority, t));
            lookup.Add(priority, t);
        }

        public TransformationRule<T> GetRule(P p)
        {
            return lookup[p];
        }

        public bool TryGetRule(P p, out TransformationRule<T> rule)
        {
            return lookup.TryGetValue(p,out rule);
        }
        

        public Conclusion Conclude(T t)
        {
            Conclusion c = null;
            
            object o = hiarch.FirstOrDefault(kp => !((c = kp.Value.Conclude(t)) is DontCareConclusion));
            if (o == null)
                return new DontCareConclusion();
            return c;
        }

    }
}
