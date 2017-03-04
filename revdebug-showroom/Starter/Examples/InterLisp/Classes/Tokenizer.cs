using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InterLispApp.Classes
{
    public enum EntryType
    {
        Name,
        Number,
        String,
        Separator
    }

    public class Tokenizer
    {
        private readonly StringReader _reader;
        private const string Separators = "()";
        private const string MathOperators = "+-*/<>=";

        private readonly Hashtable _scopes = new Hashtable();
        private int _scopeCounter = 0;

        public Tokenizer(string text)
        {
            _reader = new StringReader(text);
        }

        //public IList<TokenizedEntry> Tokenize()
        public Hashtable Tokenize()
        {
            int level = 0;
            IList<TokenizedEntry> tokenizedList = new List<TokenizedEntry>();

            while (_reader.Peek() != -1)
            {
                //skip white chars
                while (Char.IsWhiteSpace((char) _reader.Peek()) | _reader.Peek() == '\n' | _reader.Peek() == '\t')
                {
                    _reader.Read();
                }

                if (_reader.Peek() == -1)
                    return _scopes; // tokenizedList;

                var c = (char) _reader.Peek();

                //separators
                if (Separators.IndexOf(c) >= 0)
                {
                    if (c == '(')
                        level += 1;
                    else
                        level -= 1;

                    tokenizedList.Add(new TokenizedEntry()
                    {
                        Value = c.ToString(CultureInfo.InvariantCulture),
                        Level = level,
                        Type = EntryType.Separator
                    });

                    if (level == 0)
                    {
                        _scopes.Add(_scopeCounter, tokenizedList);
                        _scopeCounter += 1;
                        tokenizedList= new List<TokenizedEntry>();
                    }
                    
                    _reader.Read();
                }

                //comment
                else if (c == ';')
                {
                    while (_reader.Peek() != '\n')
                    {
                        _reader.Read();
                    }
                }

                else if (c == '-')
                {
                    var text = new StringBuilder();
                    _reader.Read();
                    c = (char) _reader.Peek();
                    //negative sign
                    if (char.IsDigit(c))
                    {
                        var tokenizedNumber = this.ParseNumber(c);

                        if (tokenizedNumber.Value is long)
                        {
                            tokenizedNumber.Value = -(long) tokenizedNumber.Value;
                        }
                        if (tokenizedNumber.Value is double)
                        {
                            tokenizedNumber.Value = -(double) tokenizedNumber.Value;
                        }
                        tokenizedList.Add(tokenizedNumber);

                        //_reader.Read();
                    }
                    else
                    {
                        //minus operator
                        text.Append("-");

                        while (!char.IsWhiteSpace(c) && MathOperators.IndexOf(c) < 0)
                        {
                            text.Append((char)_reader.Read());
                            c = (char)_reader.Peek();
                        }

                        if (text.Length > 0)
                        {
                            tokenizedList.Add(new TokenizedEntry()
                            {
                                Value = text.ToString(),
                                Level = level,
                                Type = EntryType.Name
                            });

                            _reader.Read();
                        }
                    }
                }

                //names
                else if (char.IsLetter(c) | c == '_' | MathOperators.IndexOf(c) >= 0)
                {
                    var text = new StringBuilder();

                    while (MathOperators.IndexOf(c) >= 0)
                    {
                        text.Append((char)_reader.Read());
                        c = (char)_reader.Peek();
                        //text.Append(c);
                        //c = (char)_reader.Read();
                    }
                        
                    while (!char.IsWhiteSpace(c) && MathOperators.IndexOf(c) < 0 && Separators.IndexOf(c) < 0)
                    {
                        text.Append((char) _reader.Read());
                        c = (char) _reader.Peek();
                    }

                    if (text.Length > 0)
                    {
                        tokenizedList.Add(new TokenizedEntry()
                        {
                            Value = text.ToString(),
                            Level = level,
                            Type = EntryType.Name
                        });
                        //_reader.Read();
                    }
                }

                //string
                else if (c == '\"')
                {
                    var text = new StringBuilder();
                    _reader.Read();
                    c = (char) _reader.Peek();
                    while (c != '\"')
                    {
                        text.Append((char) _reader.Read());
                        c = (char) _reader.Peek();
                    }
                    _reader.Read();

                    tokenizedList.Add(new TokenizedEntry()
                    {
                        Value = text.ToString(),
                        Level = level,
                        Type = EntryType.String
                    });
                }

                //quote
                else if (c == '\'')
                {
                    tokenizedList.Add(new TokenizedEntry() {Value = "'", Level = level, Type = EntryType.Name});
                    _reader.Read();
                }

                //dot
                else if (c == '.')
                {
                    tokenizedList.Add(new TokenizedEntry() {Value = ".", Level = level, Type = EntryType.Name});
                    _reader.Read();
                }

                //number
                else if (char.IsDigit(c))
                {
                    var tokenizedNumber = this.ParseNumber(c);
                    tokenizedList.Add(tokenizedNumber);
                }
            }
            return _scopes;
        }

        private TokenizedEntry ParseNumber(char startChar)
        {
            var isDouble = false;
            var text = new StringBuilder();

            var c = startChar;
            while (char.IsDigit(c))
            {
                text.Append((char)_reader.Read());
                c = (char)_reader.Peek();
            }

            if (c == '.')
            {
                isDouble = true;
                text.Append((char)_reader.Read());
                c = (char)_reader.Peek();
                
                //decimal part
                while (char.IsDigit(c))
                {
                    text.Append((char)_reader.Read());
                    c = (char)_reader.Peek();
                }
            }
            
            if (isDouble)
            {
                return new TokenizedEntry
                {
                    Value = Convert.ToDouble(text.ToString(), CultureInfo.InvariantCulture),
                    Level = 1,
                    Type = EntryType.Number
                };
            }

            return new TokenizedEntry
            {
                Value = Convert.ToInt64(text.ToString()),
                Level = 1,
                Type = EntryType.Number
            };
        }
    }
}
