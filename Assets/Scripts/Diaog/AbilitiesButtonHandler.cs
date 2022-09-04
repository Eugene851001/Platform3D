using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Diaog
{
    class AbilitiesButtonHandler
    {
        private AbilityNode _buttonNode;
        private Action<AbilityNode> _handler;

        public AbilitiesButtonHandler(
            AbilityNode buttonNode, 
            Action<AbilityNode> handler)
        {
            _buttonNode = buttonNode;
            _handler = handler;
        }

        public void Handle()
        {
            _handler?.Invoke(_buttonNode);
        }
    }
}
