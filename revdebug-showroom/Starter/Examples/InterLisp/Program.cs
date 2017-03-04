using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using InterLispApp.Classes;
using Starter.Examples.InterLisp.Classes;

namespace Starter.Examples.InterLisp
{
    public class InterLisp
    {
        public string Execute(string fileName)
        {
            var calcResult = new StringBuilder();

            using (var streamReader = new StreamReader(fileName))
            {
                var code = streamReader.ReadToEnd();
                
                while (code != null)
                {
                    if (string.IsNullOrEmpty(code)) { break;}

                    var tokenizer = new Tokenizer(code);
                    var scopesList = tokenizer.Tokenize();

                    var index = 0;
                    while (index < scopesList.Count)
                    {
                        var scope = (IList<TokenizedEntry>)scopesList[index];
                        IList<TokenizedEntry> tokenizedList = scope.ToList();

                        var interpreter = new Interpreter(tokenizedList);
                        var interpreted = interpreter.PrepareSeTree();
                        var interpretedCode = interpreted.ToString();

                        calcResult.Append(index + 1 + ". ");
                        calcResult.AppendLine(interpreted.ToString());
                        calcResult.Append("Result: ");
                        
                        try
                        {
                            var evaluated = Evaluator.Evaluate(interpreted);
                            calcResult.AppendLine(Converter.Convert(evaluated, toUpper: false));
                        }
                        catch (Exception ex)
                        {
                            calcResult.AppendLine(ex.Message);
                        }
                        calcResult.AppendLine();

                        index += 1;
                    }
                    code = streamReader.ReadLine();
                }
            }
            return calcResult.ToString();
        }
    }
}
