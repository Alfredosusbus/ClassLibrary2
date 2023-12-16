using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace ClassLibrary2.Tests
{
    [TestClass]
    public class ExpressionInterpreterTests
    {
        [TestMethod]
        public void TestExpressionGenerationAndInterpretation()
        {
            // Arrange
            IExpression expression = new OrExpression(
                new AndExpression(new TerminalExpression("A"), new TerminalExpression("B")),
                new AndExpression(new TerminalExpression("C"), new NotExpression(new TerminalExpression("D")))
            );

            Context context = new Context();

            // Act
            var stopwatch = Stopwatch.StartNew();
            Console.WriteLine("Generated Expression: " + expression.Interpret(context));
            stopwatch.Stop();

            // Assert
            Assert.IsTrue(stopwatch.ElapsedMilliseconds < 2, "Test took more than 2 ms");
        }

        [TestMethod]
        public void TestExpressionInterpretationPerformance()
        {
            // Arrange
            IExpression expression = new OrExpression(
                new AndExpression(new TerminalExpression("A"), new TerminalExpression("B")),
                new AndExpression(new TerminalExpression("C"), new NotExpression(new TerminalExpression("D")))
            );

            Context context = new Context();
            context.Assign("A", true);
            context.Assign("B", false);
            context.Assign("C", true);
            context.Assign("D", false);

            // Act
            var stopwatch = Stopwatch.StartNew();
            bool result = expression.Interpret(context);
            Console.WriteLine("Interpreted Result: " + result);
            stopwatch.Stop();

            // Assert
            Assert.IsTrue(stopwatch.ElapsedMilliseconds < 2, "Test took more than 2 ms");
        }




        [TestMethod]
        public void TestAnotherExpressionInterpretationPerformance()
        {
            // Arrange
            Context context = new Context();
            context.Assign("A", true);
            context.Assign("B", true);
            context.Assign("C", false);
            context.Assign("D", false);

            IExpression expression = new AndExpression(
                new OrExpression(new TerminalExpression("A"), new TerminalExpression("B")),
                new NotExpression(new AndExpression(new TerminalExpression("C"), new TerminalExpression("D")))
            );

            // Act
            var stopwatch = Stopwatch.StartNew();
            bool result = expression.Interpret(context);
            stopwatch.Stop();

            // Assert
            Assert.IsTrue(stopwatch.ElapsedMilliseconds < 20, $"Test took more than 20 ms. Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
        }

        [TestMethod]
        public void TestAllVariablesTrueInterpretationPerformance()
        {
            // Arrange
            Context context = new Context();
            context.Assign("A", true);
            context.Assign("B", true);
            context.Assign("C", true);
            context.Assign("D", true);

            IExpression expression = new AndExpression(
                new OrExpression(new TerminalExpression("A"), new TerminalExpression("B")),
                new OrExpression(new TerminalExpression("C"), new TerminalExpression("D"))
            );

            // Act
            var stopwatch = Stopwatch.StartNew();
            bool result = expression.Interpret(context);
            stopwatch.Stop();

            // Assert
            Assert.IsTrue(stopwatch.ElapsedMilliseconds < 20, $"Test took more than 20 ms. Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
            Assert.IsTrue(result, "Expression should evaluate to true when all variables are true");
        }

        [TestMethod]
        public void TestAllVariablesFalseInterpretationPerformance()
        {
            // Arrange
            Context context = new Context();
            context.Assign("A", false);
            context.Assign("B", false);
            context.Assign("C", false);
            context.Assign("D", false);

            IExpression expression = new AndExpression(
                new OrExpression(new TerminalExpression("A"), new TerminalExpression("B")),
                new OrExpression(new TerminalExpression("C"), new TerminalExpression("D"))
            );

            // Act
            var stopwatch = Stopwatch.StartNew();
            bool result = expression.Interpret(context);
            stopwatch.Stop();

            // Assert
            Assert.IsTrue(stopwatch.ElapsedMilliseconds < 20, $"Test took more than 20 ms. Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
            Assert.IsFalse(result, "Expression should evaluate to false when all variables are false");
        }

        [TestMethod]
        public void TestMixedVariablesInterpretationPerformance()
        {
            // Arrange
            Context context = new Context();
            context.Assign("A", true);
            context.Assign("B", false);
            context.Assign("C", true);
            context.Assign("D", false);

            IExpression expression = new OrExpression(
                new AndExpression(new TerminalExpression("A"), new NotExpression(new TerminalExpression("B"))),
                new AndExpression(new TerminalExpression("C"), new TerminalExpression("D"))
            );

            // Act
            var stopwatch = Stopwatch.StartNew();
            bool result = expression.Interpret(context);
            stopwatch.Stop();

            // Assert
            Assert.IsTrue(stopwatch.ElapsedMilliseconds < 20, $"Test took more than 20 ms. Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
            Assert.IsTrue(result, "Expression should evaluate to true for the given mixed variables");
        }

        [TestMethod]
        public void TestNestedExpressionsInterpretationPerformance()
        {
            // Arrange
            Context context = new Context();
            context.Assign("A", true);
            context.Assign("B", false);
            context.Assign("C", true);
            context.Assign("D", false);

            IExpression expression = new AndExpression(
                new OrExpression(
                    new AndExpression(new TerminalExpression("A"), new NotExpression(new TerminalExpression("B"))),
                    new TerminalExpression("C")
                ),
                new NotExpression(new TerminalExpression("D"))
            );

            // Act
            var stopwatch = Stopwatch.StartNew();
            bool result = expression.Interpret(context);
            stopwatch.Stop();

            // Assert
            Assert.IsTrue(stopwatch.ElapsedMilliseconds < 20, $"Test took more than 20 ms. Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
            Assert.IsTrue(result, "Expression should evaluate to true for the given nested expressions");
        }

        [TestMethod]
        public void TestComplexExpressionInterpretationPerformance()
        {
            // Arrange
            Context context = new Context();
            context.Assign("A", true);
            context.Assign("B", false);
            context.Assign("C", true);
            context.Assign("D", false);

            IExpression expression = new OrExpression(
                new AndExpression(
                    new OrExpression(new TerminalExpression("A"), new TerminalExpression("B")),
                    new NotExpression(new TerminalExpression("C"))
                ),
                new AndExpression(
                    new OrExpression(new TerminalExpression("B"), new NotExpression(new TerminalExpression("D"))),
                    new TerminalExpression("A")
                )
            );

            // Act
            var stopwatch = Stopwatch.StartNew();
            bool result = expression.Interpret(context);
            stopwatch.Stop();

            // Assert
            Assert.IsTrue(stopwatch.ElapsedMilliseconds < 20, $"Test took more than 20 ms. Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
            Assert.IsTrue(result, "Expression should evaluate to true for the given complex expression");
        }

        
           

            [TestMethod]
            public void Interpret_BothExpressionsFalse_ReturnsFalse()
            {
                // Arrange
                var context = new Context();
                var expression1 = new TerminalExpression("variable1");
                var expression2 = new TerminalExpression("variable2");
                context.Assign("variable1", false);
                context.Assign("variable2", false);
                var andExpression = new AndExpression(expression1, expression2);

                // Act
                var result = andExpression.Interpret(context);

                // Assert
                Assert.IsFalse(result);
            }
        

        [TestClass]
        public class OrExpressionTests
        {
            // ... (previous tests)

            [TestMethod]
            public void Interpret_BothExpressionsTrue_ReturnsTrue()
            {
                // Arrange
                var context = new Context();
                var expression1 = new TerminalExpression("variable1");
                var expression2 = new TerminalExpression("variable2");
                context.Assign("variable1", true);
                context.Assign("variable2", true);
                var orExpression = new OrExpression(expression1, expression2);

                // Act
                var result = orExpression.Interpret(context);

                // Assert
                Assert.IsTrue(result);
            }
        }


        [TestMethod]
        public void Interpret_NestedNotExpression_ReturnsInvertedValue()
        {
            // Arrange
            var context = new Context();
            var expression = new TerminalExpression("variable1");
            context.Assign("variable1", true);
            var nestedNotExpression = new NotExpression(expression);
            var notExpression = new NotExpression(nestedNotExpression);

            // Act
            var result = notExpression.Interpret(context);

            // Assert
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void Interpret_ExpressionTrue_ReturnsFalse()
        {
            // Arrange
            var context = new Context();
            var expression = new TerminalExpression("variable1");
            context.Assign("variable1", true);
            var notExpression = new NotExpression(expression);

            // Act
            var result = notExpression.Interpret(context);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Interpret_ExpressionFalse_ReturnsTrue()
        {
            // Arrange
            var context = new Context();
            var expression = new TerminalExpression("variable1");
            context.Assign("variable1", false);
            var notExpression = new NotExpression(expression);

            // Act
            var result = notExpression.Interpret(context);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Interpret_EitherExpressionTrue_ReturnsTrue()
        {
            // Arrange
            var context = new Context();
            var expression1 = new TerminalExpression("variable1");
            var expression2 = new TerminalExpression("variable2");
            context.Assign("variable1", true);
            context.Assign("variable2", false);
            var orExpression = new OrExpression(expression1, expression2);

            // Act
            var result = orExpression.Interpret(context);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Interpret_BothExpressionsTrue_ReturnsTrue()
        {
            // Arrange
            var context = new Context();
            var expression1 = new TerminalExpression("variable1");
            var expression2 = new TerminalExpression("variable2");
            context.Assign("variable1", true);
            context.Assign("variable2", true);
            var andExpression = new AndExpression(expression1, expression2);

            // Act
            var result = andExpression.Interpret(context);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Interpret_OneExpressionFalse_ReturnsFalse()
        {
            // Arrange
            var context = new Context();
            var expression1 = new TerminalExpression("variable1");
            var expression2 = new TerminalExpression("variable2");
            context.Assign("variable1", true);
            context.Assign("variable2", false);
            var andExpression = new AndExpression(expression1, expression2);

            // Act
            var result = andExpression.Interpret(context);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Interpret_VariableExistsInContext_ReturnsContextValue()
        {
            // Arrange
            var context = new Context();
            context.Assign("existingVariable", true);
            var terminalExpression = new TerminalExpression("existingVariable");

            // Act
            var result = terminalExpression.Interpret(context);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Interpret_VariableDoesNotExistInContext_ReturnsFalse()
        {
            // Arrange
            var context = new Context();
            var terminalExpression = new TerminalExpression("nonexistentVariable");

            // Act
            var result = terminalExpression.Interpret(context);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Lookup_VariableExists_ReturnsTrue()
        {
            // Arrange
            var context = new Context();
            context.Assign("variable1", true);

            // Act
            var result = context.Lookup("variable1");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Lookup_VariableDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var context = new Context();

            // Act
            var result = context.Lookup("nonexistentVariable");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Assign_VariableIsAssigned()
        {
            // Arrange
            var context = new Context();

            // Act
            context.Assign("newVariable", true);

            // Assert
            Assert.IsTrue(context.Lookup("newVariable"));
        }


    }
}

