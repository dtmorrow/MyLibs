using System.Runtime.CompilerServices;

namespace MyLibs
{
    /// <summary>
    /// Methods to get information about a line of code, such as the file, method name, and line number.
    /// </summary>
    public static class DebugUtilities
    {
        /// <summary>
        /// Get the full path of the source file where this method is called.
        /// </summary>
        /// <param name="filePath">Override for file path. Leave empty to get file path automatically.</param>
        /// <returns>A <see cref="string"/> containing the full path of the source file where this method is called.</returns>
        public static string GetCallerFilePath([CallerFilePath] string filePath = "") => filePath;

        /// <summary>
        /// Get the file name of the source file where this method is called.
        /// </summary>
        /// <param name="fileName">Override for file name. Leave empty to get file name automatically.</param>
        /// <returns>A <see cref="string"/> containing the file name of the source file where this method is called.</returns>
        public static string GetCallerFile([CallerFilePath] string fileName = "") => System.IO.Path.GetFileName(fileName);

        /// <summary>
        /// Get the line number of the source file where this method is called.
        /// </summary>
        /// <param name="lineNumber">Override for line number. Leave empty to get line number automatically.</param>
        /// <returns>A <see cref="string"/> containing the line number of the source file where this method is called.</returns>
        public static int GetCallerLineNumber([CallerLineNumber] int lineNumber = 0) => lineNumber;

        /// <summary>
        /// Get the name of the running method where this method is called.
        /// </summary>
        /// <param name="methodName">Override for method name. Leave empty to get method name automatically.</param>
        /// <returns>A <see cref="string"/> containing the name of the running method where this method is called.</returns>
        public static string GetCallerMethodName([CallerMemberName] string methodName = "") => methodName;

        // NOTE: This has not been implemented yet.
        //       Check progress here: https://github.com/dotnet/csharplang/issues/287
        /// <summary>
        /// Gets the expression given to this method at compile time, and the result of the expression at runtime.
        /// </summary>
        /// <typeparam name="T">The return type of the expression.</typeparam>
        /// <param name="value">The expression to be captured.</param>
        /// <param name="expression">Override for expression. Leave empty to get expression automatically.</param>
        /// <returns>A tuple containing the expression as a <see cref="string"/> and the result of the expression as the expression's return type.</returns>
        public static (string expression, T result) GetExpression<T>(T value, [CallerArgumentExpression("value")] string expression = "") => (expression, value);

        //public static string GetExpression(Action action, [CallerArgumentExpression("value")] string expression = "") => expression;
    }
}
