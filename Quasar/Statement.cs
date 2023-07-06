namespace Quasar
{
    abstract class Statement
    {
        public abstract Expression execute();

        public class Assignment : Statement 
        {
            public string Name;

            public Statement Value;

            public Environment Env;

            public Assignment(string name, Statement value, ref Environment env)
            {
                Name = name;
                Value = value;
                Env = env;
            }

            public override Expression execute()
            {
                Expression.Literal value = Value.execute() as Expression.Literal;
                Env.Define(Name, value.Value);
                return new Expression.Literal(value.Value);
            }
        }

        public class ExpressionStmt : Statement 
        {
            public Expression expression;

            public ExpressionStmt(Expression expression) 
            { 
                this.expression = expression;
            }

            public override Expression execute()
            {
                return expression.evaluate();
            }
        }

        public class Print : Statement
        {
            public ExpressionStmt expression;

            public Print(ExpressionStmt expression)
            {
                this.expression = expression;
            }

            public override Expression execute()
            {
                Expression.Literal literal = expression.execute() as Expression.Literal;
                Console.WriteLine(literal.Value);
                return literal;
            }
        }
    }
}
