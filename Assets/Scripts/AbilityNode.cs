using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Assets.Scripts
{
    public enum NodeType
    {
        Ability,
        Group,
    }


    public class AbilityNode
    {
        public AbilityNode Parent { get; set; }

        public AbilityNode[] Childs { get; set; }

        public NodeType NodeType { get; set; }

        public Ablilities Value { get; set; }

        public string Name { get; set; }

        public bool IsDeveloped { get; set; }

        public void InitParents()
        {
            TraversTree(this, InitParent); 

            void InitParent(AbilityNode node)
            {
                foreach (var child in node.Childs)
                {
                    child.Parent = node;
                }
            }
        }

        public static void TraversTree(AbilityNode node, Action<AbilityNode> handleNode)
        {
            handleNode(node);

            foreach (var child in node.Childs)
            {
                TraversTree(child, handleNode);
            }
        }
    }
}
