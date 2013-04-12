using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine.Exceptions;

namespace GooseEngine.Conversion
{
    public class GooseConversionTool<ToType>
    {
        private InherienceTree tree = new InherienceTree();

        public GooseConversionTool()
        {
            InherienceNode n = new InherienceNode(typeof(object), new NoConverter());
        }

        public void AddConverter<FromType>(GooseConverter<FromType,ToType> converter) where FromType : GooseObject
        {
            Type fromType = typeof(FromType);
            converter.ConversionTool = this;
            InherienceNode node = new InherienceNode(fromType, converter);
            tree.AddNode(node);           
        }

        public ToType Convert(GooseObject gobj)
        {
            InherienceNode leaf = this.tree.FindYoungestParent(gobj.GetType());
            return (ToType)leaf.Convert(gobj);
        }


        private class InherienceNode
        {
            private Type main;
            private HashSet<InherienceNode> derivingChildren = new HashSet<InherienceNode>();
            private GooseConverter converter;

            public InherienceNode(Type main, GooseConverter converter)
            {
                this.main = main;
                this.converter = converter;
            }

            public bool IsParentOf(InherienceNode node)
            {
                return IsParentOf(node.main);
            }

            public bool IsParentOf(Type type)
            {
                return IsSubclassOf(main, type);
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
                        break;
                    }
                    else if (node.IsParentOf(child))
                    {
                        derivingChildren.Remove(child);
                        derivingChildren.Add(node);
                        node.AddChild(child);
                        foundfamily = true;
                        break;
                    }
                }

                if (foundfamily != true)
                    this.derivingChildren.Add(node);

            }


            public  InherienceNode FindParent(Type type)
            {
                foreach (InherienceNode child in this.derivingChildren)
                {
                    if (child.IsParentOf(type))
                        return child.FindParent(type);
                }
                return this;
            }

            public object Convert(GooseObject gobj)
            {
                return converter.BeginUnsafeConversion(gobj);
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


            public InherienceNode FindYoungestParent(Type type)
            {
                return root.FindParent(type);
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


        private class NoConverter : GooseConverter
        {

            internal override object BeginUnsafeConversion(GooseObject gobj)
            {
                throw new UnconvertableException(gobj);
            }
        }
    }
}
