using System;
using System.Collections.Generic;
using System.Linq;

namespace SqlCommandBuilder
{
    class FunctionMapping
    {
        public string FunctionName { get; private set; }
        public Func<string, string, IEnumerable<object>, CommandExpression> FunctionMapper { get; private set; }

        public FunctionMapping(string functionName)
        {
            this.FunctionName = functionName;
            this.FunctionMapper = FunctionWithTarget;
        }

        public FunctionMapping(string functionName, Func<string, string, IEnumerable<object>, CommandExpression> functionMapper)
        {
            this.FunctionName = functionName;
            this.FunctionMapper = functionMapper;
        }

        private readonly static Func<string, string, IEnumerable<object>, CommandExpression> FunctionWithTarget =
                (functionName, targetName, arguments) => CommandExpression.FromFunction(
                new ExpressionFunction()
                {
                    FunctionName = SupportedFunctions[new ExpressionFunction.FunctionCall(functionName, 0)].FunctionName,
                    Arguments = new List<CommandExpression>() { CommandExpression.FromReference(targetName) },
                });

        private readonly static Func<string, string, IEnumerable<object>, CommandExpression> FunctionWithTargetAndArguments =
                (functionName, targetName, arguments) => CommandExpression.FromFunction(
                new ExpressionFunction()
                {
                    FunctionName = SupportedFunctions[new ExpressionFunction.FunctionCall(functionName, arguments.Count())].FunctionName,
                    Arguments = MergeArguments(CommandExpression.FromReference(targetName), arguments),
                });

        private readonly static Func<string, string, IEnumerable<object>, CommandExpression> FunctionWithArgumentsAndTarget =
                (functionName, targetName, arguments) => CommandExpression.FromFunction(
                new ExpressionFunction()
                {
                    FunctionName = SupportedFunctions[new ExpressionFunction.FunctionCall(functionName, arguments.Count())].FunctionName,
                    Arguments = MergeArguments(arguments, CommandExpression.FromReference(targetName)),
                });

        public static Dictionary<ExpressionFunction.FunctionCall, FunctionMapping> SupportedFunctions = new Dictionary<ExpressionFunction.FunctionCall, FunctionMapping>()
            {
                {new ExpressionFunction.FunctionCall("Length", 0), new FunctionMapping("LEN")},
                {new ExpressionFunction.FunctionCall("IndexOf", 1), new FunctionMapping("CHARINDEX", FunctionWithTargetAndArguments)},
                {new ExpressionFunction.FunctionCall("Replace", 2), new FunctionMapping("REPLACE", FunctionWithTargetAndArguments)},
                {new ExpressionFunction.FunctionCall("Substring", 1), new FunctionMapping("SUBSTRING", FunctionWithTargetAndArguments)},
                {new ExpressionFunction.FunctionCall("Substring", 2), new FunctionMapping("SUBSTRING", FunctionWithTargetAndArguments)},
                {new ExpressionFunction.FunctionCall("ToLower", 0), new FunctionMapping("LOWER")},
                {new ExpressionFunction.FunctionCall("ToUpper", 0), new FunctionMapping("UPPER")},
                {new ExpressionFunction.FunctionCall("Trim", 0), new FunctionMapping("TRIM")},
                {new ExpressionFunction.FunctionCall("Concat", 1), new FunctionMapping("CONCAT", FunctionWithTargetAndArguments)},
                {new ExpressionFunction.FunctionCall("Round", 0), new FunctionMapping("ROUND")},
                {new ExpressionFunction.FunctionCall("Floor", 0), new FunctionMapping("FLOOR")},
                {new ExpressionFunction.FunctionCall("Ceiling", 0), new FunctionMapping("CEILING")},
            };

        private static List<CommandExpression> MergeArguments(CommandExpression argument, IEnumerable<object> arguments)
        {
            var collection = new List<CommandExpression>();
            collection.Add(argument);
            collection.AddRange(arguments.Select(CommandExpression.FromValue));
            return collection;
        }

        private static List<CommandExpression> MergeArguments(IEnumerable<object> arguments, CommandExpression argument)
        {
            var collection = new List<CommandExpression>();
            collection.AddRange(arguments.Select(CommandExpression.FromValue));
            collection.Add(argument);
            return collection;
        }
    }
}