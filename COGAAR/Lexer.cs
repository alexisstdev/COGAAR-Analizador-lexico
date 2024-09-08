using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace COGAAR
{
    public class Lexer
    {
        private static readonly List<(string, string)> patterns = new List<(string, string)>
    {
        (@"[\+\-]?[0-9]+\.[0-9]+", "NUMDB"),
        (@"[\+\-]?[0-9]+", "NUMINT"),
        (@"\bcase\b", "CASE"),
        (@"\bswitch\b", "SWTCH"),
        (@"\bbreak\b", "BRK"),
        (@"\btry\b", "TRY"),
        (@"\binput\b", "INP"),
        (@"\boutput\b", "OUT"),
        (@"\bclear\b", "CLEAR"),
        (@"\bint\b", "TPINT"),
        (@"\bstring\b", "TPSTR"),
        (@"\bdouble\b", "TPDBL"),
        (@"\bcatch\b", "CTCH"),
        (@"\bif\b", "IF"),
        (@"\belse\b", "ELSE"),
        (@"\belseif\b", "ELIF"),
        (@"\bfor\b", "FOR"),
        (@"\bwhile\b", "WHI"),
        (@"\bdo\b", "DO"),
        (@"\bcontinue\b", "CNTN"),
        (@"\breturn\b", "RTRN"),
        (@"\bfunction\b", "FCTN"),
        (@"\b[_][a-zA-Z_][a-zA-Z0-9_]*\b", "IDEN"),
        (@"[;]", "CH;"),
        (@"[,]", "CH,"),
        (@"[.]", "CH."),
        (@"[(]", "CH("),
        (@"[)]", "CH)"),
        (@"[{]", "CH{"),
        (@"[}]", "CH}"),
        (@"[=]", "ASSGN"),
        (@"[+]", "AOP+"),
        (@"[-]", "AOP-"),
        (@"[*]", "AOP*"),
        (@"[<]", "ROP<"),
        (@"[>]", "ROP>"),
        (@"[!]", "LOP!"),
        (@"[&]", "LOP&"),
        (@"[|]", "LOP|"),
        (@"[:]", "CH:"),
        (@"==", "ROP=="),
        (@"!=", "ROP!="),
        (@"<=", "ROP<="),
        (@"\n", "BRKLN"),
        (@"//.*", "COMM"),
    };

        private static readonly Dictionary<string, string> errorMessages = new Dictionary<string, string>
    {
        { "INVALID_IDEN", "Identificador no válido" },
        { "INVALID_KEYWORD", "Palabra reservada no válida" },
        { "INVALID_NUMINT", "Constante numérica entera no válida" },
        { "INVALID_NUMDB", "Constante numérica flotante no válida" },
        { "INVALID_NUMEXP", "Constante numérica con exponente no válida" },
        { "INVALID_OPERATOR", "Operadores aritméticos no válidos" },
        { "INVALID_STRING", "Cadena no válida" },
        { "INVALID_COMMENT", "Comentario no válido" },
        { "SYNTAX_ERROR", "Error de sintaxis" }
    };

        private static readonly HashSet<string> validKeywords = new HashSet<string>
    {
        "CASE", "SWTCH", "BRK", "TRY", "INP", "OUT", "CLEAR",
        "TPINT", "TPSTR", "TPDBL", "CTCH", "IF", "ELSE", "ELIF",
        "FOR", "WHI", "DO", "CNTN", "RTRN", "FCTN"
    };

        private string text;
        private List<Dictionary<string, object>> tokens;
        private List<Dictionary<string, object>> errors;
        private Dictionary<string, Dictionary<string, object>> identifierValues;
        private int identifierCounter;
        private Dictionary<string, int> identifierMap;

        public Lexer(string text)
        {
            this.text = text;
            this.tokens = new List<Dictionary<string, object>>();
            this.errors = new List<Dictionary<string, object>>();
            this.identifierValues = new Dictionary<string, Dictionary<string, object>>();
            this.identifierCounter = 1;
            this.identifierMap = new Dictionary<string, int>();
        }

        public void Tokenize()
        {
            int lineNumber = 1;
            string lastTokenType = null;
            foreach (var line in text.Split('\n'))
            {
                var tokensLine = new List<Dictionary<string, object>>();
                int lastEnd = 0;
                foreach (Match match in Regex.Matches(line, string.Join("|", patterns.ConvertAll(p => $"({p.Item1})"))))
                {
                    int start = match.Index;
                    int end = match.Index + match.Length;
                    if (start > lastEnd)
                    {
                        string unrecognizedText = line.Substring(lastEnd, start - lastEnd).Trim();
                        if (!string.IsNullOrEmpty(unrecognizedText))
                        {
                            errors.Add(new Dictionary<string, object>
                        {
                            { "line", lineNumber },
                            { "type", "ERROR" },
                            { "message", $"Token o identificador inesperado: {unrecognizedText}" }
                        });
                        }
                    }
                    lastEnd = end;
                    for (int i = 0; i < match.Groups.Count - 1; i++)
                    {
                        if (match.Groups[i + 1].Success)
                        {
                            string tokenType = patterns[i].Item2;
                            string group = match.Groups[i + 1].Value;
                            if (tokenType == "IDEN")
                            {
                                if (!identifierMap.ContainsKey(group))
                                {
                                    identifierMap[group] = identifierCounter++;
                                }
                                tokenType = $"IDEN{identifierMap[group]}";
                            }
                            tokensLine.Add(new Dictionary<string, object>
                        {
                            { "type", tokenType },
                            { "value", group },
                            { "line", lineNumber }
                        });
                            if (tokenType.StartsWith("TP"))
                            {
                                lastTokenType = group;
                            }
                            break;
                        }
                    }
                }

                if (lastEnd < line.Length)
                {
                    string unrecognizedText = line.Substring(lastEnd).Trim();
                    if (!string.IsNullOrEmpty(unrecognizedText))
                    {
                        errors.Add(new Dictionary<string, object>
                    {
                        { "line", lineNumber },
                        { "type", "SYNTAX_ERROR" },
                        { "message", $"Texto no reconocido: {unrecognizedText}" }
                    });
                    }
                }

                for (int i = 0; i < tokensLine.Count; i++)
                {
                    var token = tokensLine[i];
                    if (token["type"].ToString().StartsWith("IDEN"))
                    {
                        if (lastTokenType != null)
                        {
                            identifierValues[token["value"].ToString()] = new Dictionary<string, object>
                        {
                            { "type", lastTokenType },
                            { "value", null }
                        };
                            lastTokenType = null;
                        }
                        if (i + 2 < tokensLine.Count && tokensLine[i + 1]["type"].ToString() == "ASSGN")
                        {
                            var valueToken = tokensLine[i + 2];
                            if (new[] { "NUMINT", "STR", "IDEN", "NUMDB" }.Contains(valueToken["type"].ToString()))
                            {
                                if (identifierValues.ContainsKey(token["value"].ToString()))
                                {
                                    identifierValues[token["value"].ToString()]["value"] = valueToken["value"];
                                }
                                else
                                {
                                    identifierValues[token["value"].ToString()] = new Dictionary<string, object>
                                {
                                    { "type", null },
                                    { "value", valueToken["value"] }
                                };
                                }
                            }
                        }
                    }
                }

                tokens.AddRange(tokensLine);
                lineNumber++;
            }
        }

        public List<Dictionary<string, object>> GetTokens()
        {
            return tokens;
        }

        public List<Dictionary<string, object>> GetIdentifiersInfo()
        {
            var lastIdentifiers = new Dictionary<string, Dictionary<string, object>>();
            foreach (var token in tokens)
            {
                if (token["type"].ToString().StartsWith("IDEN") && identifierValues.ContainsKey(token["value"].ToString()))
                {
                    var identifierInfo = identifierValues[token["value"].ToString()];
                    identifierInfo["line"] = token["line"];
                    lastIdentifiers[token["value"].ToString()] = new Dictionary<string, object>
                {
                    { "line", identifierInfo["line"] },
                    { "type", identifierInfo["type"] },
                    { "name", token["value"] },
                    { "value", identifierInfo["value"] }
                };
                }
            }
            return new List<Dictionary<string, object>>(lastIdentifiers.Values);
        }

        public List<Dictionary<string, object>> DetectErrors()
        {
            foreach (var token in tokens)
            {
                if (token["type"].ToString().StartsWith("IDEN") && !token["value"].ToString().StartsWith("_"))
                {
                    errors.Add(new Dictionary<string, object>
                {
                    { "line", token["line"] },
                    { "type", "INVALID_IDEN" },
                    { "message", errorMessages["INVALID_IDEN"] }
                });
                }
                else if (!validKeywords.Contains(token["type"].ToString()) && !token["type"].ToString().StartsWith("IDEN"))
                {
                    if (Regex.IsMatch(token["value"].ToString(), @"^[a-zA-Z_]\w*$") && !validKeywords.Contains(token["value"].ToString()))
                    {
                        errors.Add(new Dictionary<string, object>
                    {
                        { "line", token["line"] },
                        { "type", "INVALID_KEYWORD" },
                        { "message", errorMessages["INVALID_KEYWORD"] }
                    });
                    }
                }
                else if (token["type"].ToString() == "NUMINT" && Regex.IsMatch(token["value"].ToString(), @"[a-zA-Z]"))
                {
                    errors.Add(new Dictionary<string, object>
                {
                    { "line", token["line"] },
                    { "type", "INVALID_NUMINT" },
                    { "message", errorMessages["INVALID_NUMINT"] }
                });
                }
                else if (token["type"].ToString() == "NUMDB" && Regex.IsMatch(token["value"].ToString(), @"[a-zA-Z]"))
                {
                    errors.Add(new Dictionary<string, object>
                {
                    { "line", token["line"] },
                    { "type", "INVALID_NUMDB" },
                    { "message", errorMessages["INVALID_NUMDB"] }
                });
                }
                else if (token["type"].ToString().StartsWith("AOP") && Regex.IsMatch(token["value"].ToString(), @"[^\+\-\*/]"))
                {
                    errors.Add(new Dictionary<string, object>
                {
                    { "line", token["line"] },
                    { "type", "INVALID_OPERATOR" },
                    { "message", errorMessages["INVALID_OPERATOR"] }
                });
                }
                else if (token["type"].ToString() == "STR" && Regex.IsMatch(token["value"].ToString(), @"\b(case|switch|break|try|input|output|clear|int|string|double|catch|if|else|elseif|for|while|do|continue|return|function)\b"))
                {
                    errors.Add(new Dictionary<string, object>
                {
                    { "line", token["line"] },
                    { "type", "INVALID_STRING" },
                    { "message", errorMessages["INVALID_STRING"] }
                });
                }
                else if (token["type"].ToString() == "COMM" && !Regex.IsMatch(token["value"].ToString(), @"//.*"))
                {
                    errors.Add(new Dictionary<string, object>
                {
                    { "line", token["line"] },
                    { "type", "INVALID_COMMENT" },
                    { "message", errorMessages["INVALID_COMMENT"] }
                });
                }
            }
            return errors;
        }
    }
}