using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterLispApp.Classes
{
    public interface ISExpression
    {
        object Eval();
    }

    public class KeyWord : ISExpression
    {
        private readonly string _name;
        
        public string Name
        {
            get { return _name; }
        }

        public KeyWord(string name)
        {
            _name = name;
        }

        public override string ToString()
        {
            return _name;
        }

        public object Eval()
        {
            if (_name == "nil")
                return null;

            if (Evaluator.DataStack.Count > 0)
            {
                IDictionary<string, object> scopeData = Evaluator.DataStack.Peek();

                if (scopeData != null && scopeData.ContainsKey(_name))
                {
                    return scopeData[_name];
                }
            }
            
            if (Evaluator.MemDictionary.ContainsKey(_name))
            {
                return Evaluator.MemDictionary[_name];
            }

            if (Operations.GetOperations(_name) != null)
            {
                return Operations.GetOperations(_name);
            }

            return _name;
        }

        public int CompareTo(object obj)
        {
            return _name.CompareTo(obj.ToString());
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var word = obj as string;
            if (word != null)
                return _name.Equals(word);

            return false;
        }

        public override int GetHashCode()
        {
            return _name.GetHashCode();
        }
    }
}
