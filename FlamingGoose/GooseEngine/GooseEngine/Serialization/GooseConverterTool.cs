using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine.Serialization
{
    public class GooseConversionTool<ToType>
    {
        public void AddConverter<FromType>(GooseSerializer<FromType> converter) where FromType : GooseObject
        {
            Type fromType = typeof(FromType);
           
        }

        public ToType Convert(GooseObject gobj)
        {
            throw new NotImplementedException();
        }




        private class InherienceNode
        {
            private Type main;
            private HashSet<InherienceNode> derivingChildren = new HashSet<InherienceNode>();

            public InherienceNode(Type main)
            {
                this.main = main;
            }

            public bool IsParentOf(InherienceNode node)
            {
                return IsSubclassOf(main,node.main);
            }

            public void AddChild(InherienceNode node)
            {
                bool foundfamily = false;

                foreach (InherienceNode child in derivingChildren.ToArray())
                {
                    if (child.IsParentOf(node))
                    {
                        child.AddChild(node);
                        foundfamily = true;
                    }
                    else if (node.IsParentOf(child))
                    {
                        derivingChildren.Remove(child);
                        derivingChildren.Add(node);
                        node.AddChild(child);
                        foundfamily = true;
                    }
                }

                if (foundfamily != true)
                    this.derivingChildren.Add(node);

            }

        }

        private class InherienceTree
        {
            private InherienceNode root;

            public void AddNode(InherienceNode node)
            {
                if (root == null)
                    root = node;
                else
                {
                    if (root.IsParentOf(node))
                        root.AddChild(node);
                    else
                    {
                        node.AddChild(root);
                        root = node;
                    }
                }

            }

        }

        private static bool IsSubclassOf(Type generic, Type toCheck) 
        {
                while (toCheck != null && toCheck != typeof(object)) 
                {
                    var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                    if (generic == cur) 
                    {
                        return true;
                    }
                toCheck = toCheck.BaseType;
                }
            return false;
        }

    }
}
