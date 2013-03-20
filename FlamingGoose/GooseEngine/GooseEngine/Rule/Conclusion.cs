using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooseEngine.Rule
{
    public class Conclusion
    {
        private object tag;

        public object Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        public Conclusion()
        {

        }
        public Conclusion(object tag)
        {
            this.tag = tag;
        }

        public override string ToString()
        {
            return tag.ToString();
        }

    }
}
