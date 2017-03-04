using System;
using System.Collections.Generic;
using InterLispApp.Classes;
using Starter.Examples.InterLisp.Classes;

namespace InterLispApp.Classes
{
    public class Interpreter
    {
        private readonly IList<TokenizedEntry> _tokenizedEntries;
        private int _index = 0;

        public Interpreter(IList<TokenizedEntry> tokenizedEntries)
        {
            _tokenizedEntries = tokenizedEntries;
        }

        public object PrepareSeTree()
        {
            if (_tokenizedEntries == null || _tokenizedEntries.Count == 0)
            {
                return null;
            }

            return ProcessNextEntry();
        }

        private object ProcessNextEntry()
        {
            var tokenizedEntry = GetNextEntry();
            if (tokenizedEntry == null)
            {
                return null;
            }

            if (tokenizedEntry.Type == EntryType.Separator && tokenizedEntry.Value.Equals("("))
            {
                return ProcesList();
            }

            if (tokenizedEntry.Type == EntryType.Number)
                return tokenizedEntry.Value;

            if (tokenizedEntry.Type == EntryType.String)
                return tokenizedEntry.Value;

            if (tokenizedEntry.Type == EntryType.Name)
            {
                if (tokenizedEntry.Value.Equals("'"))
                {
                    return new SeList(new KeyWord("quote"), new SeList(ProcessNextEntry()));
                }

                return new KeyWord((string) tokenizedEntry.Value);
            }

            return null;
        }

        private SeList ProcesList()
        {
            if (CheckNextEntry(EntryType.Separator, ")"))
            {
                return null;
            }

            object left = ProcessNextEntry();
            object right;

            if (CheckNextEntry(EntryType.Name, "."))
            {
                right = ProcessNextEntry();
                SpyNextEntry(EntryType.Separator, ")");
            }
            else
            {
                right = ProcesList();
            }

            var seList = new SeList(left, right);
            return seList;
        }

        private TokenizedEntry GetNextEntry()
        {
            var index = _index;

            if (index < _tokenizedEntries.Count)
            {
                _index += 1;
                return _tokenizedEntries[index];
            }

            return null;
        }

        private bool CheckNextEntry(EntryType entryType, object valueToCheck)
        {
            var index = _index;

            if (index >= _tokenizedEntries.Count) return false;

            var tokenizedEntry = _tokenizedEntries[index];

            if (tokenizedEntry == null) return false;

            if (tokenizedEntry.Type == entryType && tokenizedEntry.Value.Equals(valueToCheck))
            {
                _index += 1;
                return true;
            }
            return false;
        }


        private void SpyNextEntry(EntryType entryType, object expected)
        {
            var tokenizedEntry = GetNextEntry();

            if (tokenizedEntry == null)
                throw new Exception(string.Format("Expected '{0}'", expected));

            if (tokenizedEntry.Type != entryType || !tokenizedEntry.Value.Equals(expected))
                throw new Exception(string.Format("Expected '{0}'", expected));
        }
    }
}
